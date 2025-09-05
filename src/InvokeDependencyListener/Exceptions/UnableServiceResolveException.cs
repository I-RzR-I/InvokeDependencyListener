// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependencyListener
//  Author           : RzR
//  Created On       : 2025-09-01 22:30
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-01 23:06
// ***********************************************************************
//  <copyright file="UnableServiceResolveException.cs" company="RzR SOFT & TECH">
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
    ///     Exception for signalling unable service resolve errors.
    /// </summary>
    /// <seealso cref="T:Exception"/>
    /// =================================================================================================
    public class UnableServiceResolveException : Exception
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnableServiceResolveException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// =================================================================================================
        public UnableServiceResolveException(string message) : base(message) { }
    }
}