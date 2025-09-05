// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependencyListener
//  Author           : RzR
//  Created On       : 2025-09-01 22:11
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-01 23:06
// ***********************************************************************
//  <copyright file="NoPublicConstructorException.cs" company="RzR SOFT & TECH">
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
    ///     Exception for signalling no public constructor errors.
    /// </summary>
    /// <seealso cref="T:Exception" />
    /// =================================================================================================
    public class NoPublicConstructorException : Exception
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="NoPublicConstructorException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// =================================================================================================
        public NoPublicConstructorException(string message) : base(message) { }
    }
}