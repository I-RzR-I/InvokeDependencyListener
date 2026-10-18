// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependencyListener
//  Author           : RzR
//  Created On       : 2025-08-07 15:53
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-01 23:06
// ***********************************************************************
//  <copyright file="CyclicDependencyException.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;

#endregion

namespace RzR.Infrastructure.DependencyResolver.Exceptions
{
    /// <summary>
    ///     Exception for signalling cyclic dependency errors.
    /// </summary>
    /// <seealso cref="T:Exception">
    ///     =================================================================================================
    /// </seealso>
    public class CyclicDependencyException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CyclicDependencyException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CyclicDependencyException(string message) : base(message) { }
    }
}