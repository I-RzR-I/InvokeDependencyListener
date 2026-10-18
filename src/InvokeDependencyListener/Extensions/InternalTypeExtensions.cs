// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependencyListener
//  Author           : RzR
//  Created On       : 2025-09-02 18:26
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-02 18:50
// ***********************************************************************
//  <copyright file="InternalTypeExtensions.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using RzR.Extensions.Domain.Collections;
using RzR.Extensions.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace RzR.Infrastructure.DependencyResolver.Extensions
{
    /// <summary>
    ///     An internal type extensions.
    /// </summary>
    internal static class InternalTypeExtensions
    {
        /// <summary>
        ///     Gets the available types in this collection.
        /// </summary>
        /// <param name="sourceAssemblyTypes">The sourceAssemblyTypes to act on.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the available types in this
        ///     collection.
        /// </returns>
        internal static IEnumerable<Type> GetAvailableTypes(this Type[] sourceAssemblyTypes, Type serviceType)
            => sourceAssemblyTypes.Where(x => x.IsAvailableForInstance(serviceType)).NotNull();

        /// <summary>
        ///     A Type extension method that query if 'sourceType' is available for instance.
        /// </summary>
        /// <param name="sourceType">The sourceType to act on.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        ///     True if available for instance, false if not.
        /// </returns>
        internal static bool IsAvailableForInstance(this Type sourceType, Type serviceType)
            => sourceType.IsPublic.IsTrue() 
               && sourceType.IsInterface.IsFalse() 
               && sourceType.IsAbstract.IsFalse() 
               && serviceType.IsAssignableFrom(sourceType);

        /// <summary>
        ///     A Type extension method that query if 'sourceType' is available for instance.
        /// </summary>
        /// <param name="sourceType">The sourceType to act on.</param>
        /// <returns>
        ///     True if available for instance, false if not.
        /// </returns>
        internal static bool IsAvailableForInstance(this Type sourceType)
            => sourceType.IsPublic.IsTrue() 
               && sourceType.IsInterface.IsFalse() 
               && sourceType.IsAbstract.IsFalse();
    }
}