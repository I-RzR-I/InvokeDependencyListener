// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependencyListener
//  Author           : RzR
//  Created On       : 2025-09-01 22:54
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-01 23:06
// ***********************************************************************
//  <copyright file="RequiredServiceNotRegisteredException.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;

#endregion

namespace InvokeDependencyListener.Exceptions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Exception for signalling required service not registered errors.
    /// </summary>
    /// <seealso cref="T:Exception"/>
    /// =================================================================================================
    public class RequiredServiceNotRegisteredException : Exception
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="RequiredServiceNotRegisteredException"/>
        ///     class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// =================================================================================================
        public RequiredServiceNotRegisteredException(string message) : base(message)
        {
        }
    }
}