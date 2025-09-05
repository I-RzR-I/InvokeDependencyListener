// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependencyListener
//  Author           : RzR
//  Created On       : 2025-08-07 20:24
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-01 18:18
// ***********************************************************************
//  <copyright file="ServiceInstanceBuildHelper.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DomainCommonExtensions.DataTypeExtensions;
using InvokeDependencyListener.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#endregion

namespace InvokeDependencyListener.Helpers
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A service instance build helper.
    /// </summary>
    /// =================================================================================================
    internal class ServiceInstanceBuildHelper
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the instance.
        /// </summary>
        /// =================================================================================================
        internal static readonly ServiceInstanceBuildHelper Instance = new ServiceInstanceBuildHelper();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Prevents a default instance of the <see cref="ServiceInstanceBuildHelper"/> class from
        ///     being created.
        /// </summary>
        /// =================================================================================================
        private ServiceInstanceBuildHelper() { }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Creates an instance.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the requested operation is invalid.
        /// </exception>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <param name="compiledFactories">The compiled factories.</param>
        /// <param name="getServiceFunc">The get service function.</param>
        /// <returns>
        ///     The new instance.
        /// </returns>
        /// =================================================================================================
        internal object CreateInstance(
            Type implementationType,
            ConcurrentDictionary<Type, Func<object[], object>> compiledFactories,
            Func<Type, object> getServiceFunc)
        {
            object[] parameters;
            if (compiledFactories.TryGetValue(implementationType, out var compiledFactory))
            {
                parameters = GetConstructorParameters(implementationType, getServiceFunc);
                return compiledFactory(parameters);
            }

            // Fallback to reflection-based creation
            var constructors = implementationType.GetConstructors()
                .Where(c => c.IsPublic)
                .OrderByDescending(c => c.GetParameters().Length)
                .ToArray();

            if (constructors.Length.IsZero())
            {
                var message = DefaultMessagesHelper.FormatToDefault(
                    nameof(ServiceInstanceBuildHelper),
                    DefaultMessagesHelper.NoPublicCtorFound,
                    implementationType.Name);

                throw new NoPublicConstructorException(message);
            }

            var constructor = constructors[0];
            parameters = GetConstructorParameters(implementationType, getServiceFunc, constructor);

            return Activator.CreateInstance(implementationType, parameters);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Compile factory.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="constructor">The constructor.</param>
        /// <returns>
        ///     A function delegate that yields an object.
        /// </returns>
        /// =================================================================================================
        private Func<object[], object> CompileFactory(Type type, ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();
            var parameterExpressions = parameters.Select((p, i) =>
                Expression.Convert(
                    Expression.ArrayIndex(
                        Expression.Parameter(typeof(object[]), "args"),
                        Expression.Constant(i)),
                    p.ParameterType)).ToArray();

            var newExpression = Expression.New(constructor, parameterExpressions);
            var convertExpression = Expression.Convert(newExpression, typeof(object));

            return Expression.Lambda<Func<object[], object>>(
                convertExpression,
                Expression.Parameter(typeof(object[]), "args")).Compile();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets constructor parameters.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the requested operation is invalid.
        /// </exception>
        /// <param name="type">The type.</param>
        /// <param name="getServiceFunc">The get service function.</param>
        /// <param name="constructor">(Optional) The constructor.</param>
        /// <returns>
        ///     An array of object.
        /// </returns>
        /// =================================================================================================
        private object[] GetConstructorParameters(Type type, Func<Type, object> getServiceFunc, ConstructorInfo constructor = null)
        {
            constructor ??= type.GetConstructors()[0];
            var parameters = constructor.GetParameters();
            var args = new object[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameterType = parameters[i].ParameterType;
                args[i] = getServiceFunc(parameterType) ??
                          throw new InvalidOperationException(
                              DefaultMessagesHelper.FormatToDefault(
                                  nameof(ServiceInstanceBuildHelper),
                                  DefaultMessagesHelper.UnableServiceResolve,
                                  parameterType.Name, type.Name));
            }

            return args;
        }
    }
}