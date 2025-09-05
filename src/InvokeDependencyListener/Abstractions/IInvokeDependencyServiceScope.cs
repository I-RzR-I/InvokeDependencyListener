// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependencyListener
//  Author           : RzR
//  Created On       : 2025-08-07 15:42
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-07 17:31
// ***********************************************************************
//  <copyright file="IInvokeDependencyServiceScope.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;

#endregion

namespace InvokeDependencyListener.Abstractions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Interface for invoke dependency service scope.
    /// </summary>
    /// =================================================================================================
    public interface IInvokeDependencyServiceScope : IDisposable
    {
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
    }
}