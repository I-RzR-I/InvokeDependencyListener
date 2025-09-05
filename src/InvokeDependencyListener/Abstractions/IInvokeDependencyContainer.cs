// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependencyListener
//  Author           : RzR
//  Created On       : 2025-08-07 15:41
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-07 17:31
// ***********************************************************************
//  <copyright file="IInvokeDependencyContainer.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using InvokeDependencyListener.Enums;
using System;

#endregion

namespace InvokeDependencyListener.Abstractions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Interface for invoke dependency container.
    /// </summary>
    /// =================================================================================================
    public interface IInvokeDependencyContainer : IDisposable
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Registers this object.
        /// </summary>
        /// <typeparam name="TImplementation">Type of the implementation.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="lifetime">
        ///     (Optional) The lifetime. Default value is <seealso cref="InvokeDependencyServiceLifetime.Scoped"/>.
        /// </param>
        /// =================================================================================================
        void RegisterService<TImplementation>(
            TImplementation instance, 
            InvokeDependencyServiceLifetime lifetime = InvokeDependencyServiceLifetime.Scoped)
            where TImplementation : class;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Registers this object.
        /// </summary>
        /// <typeparam name="TInterface">Type of the interface.</typeparam>
        /// <typeparam name="TImplementation">Type of the implementation.</typeparam>
        /// <param name="lifetime">
        ///     (Optional) The lifetime. Default value is <seealso cref="InvokeDependencyServiceLifetime.Scoped"/>.
        /// </param>
        /// =================================================================================================
        void RegisterService<TInterface, TImplementation>(
            InvokeDependencyServiceLifetime lifetime = InvokeDependencyServiceLifetime.Scoped)
            where TImplementation : class, TInterface;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Registers the factory.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="factory">The factory.</param>
        /// <param name="lifetime">
        ///     (Optional) The lifetime. Default value is <seealso cref="InvokeDependencyServiceLifetime.Scoped"/>.
        /// </param>
        /// =================================================================================================
        void RegisterFactory<T>(
            Func<IInvokeDependencyContainer, T> factory, 
            InvokeDependencyServiceLifetime lifetime = InvokeDependencyServiceLifetime.Scoped);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets a service.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <returns>
        ///     The service.
        /// </returns>
        /// =================================================================================================
        T GetService<T>();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets a service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        ///     The service.
        /// </returns>
        /// =================================================================================================
        object GetService(Type serviceType);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets required service.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <returns>
        ///     The required service.
        /// </returns>
        /// =================================================================================================
        T GetRequiredService<T>();

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets required service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        ///     The required service.
        /// </returns>
        /// =================================================================================================
        object GetRequiredService(Type serviceType);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Creates the scope.
        /// </summary>
        /// <returns>
        ///     The new scope.
        /// </returns>
        /// =================================================================================================
        IInvokeDependencyServiceScope CreateScope();
    }
}