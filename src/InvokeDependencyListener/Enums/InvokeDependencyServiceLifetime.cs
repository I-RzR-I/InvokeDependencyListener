// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependencyListener
//  Author           : RzR
//  Created On       : 2025-08-07 15:34
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-07 15:34
// ***********************************************************************
//  <copyright file="InvokeDependencyServiceLifetime.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace InvokeDependencyListener.Enums
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Values that represent service lifetimes.
    /// </summary>
    /// =================================================================================================
    public enum InvokeDependencyServiceLifetime
    {
        /// <summary>
        ///     An enum constant representing the transient option.
        /// </summary>
        Transient,

        /// <summary>
        ///     An enum constant representing the scoped option.
        /// </summary>
        Scoped,

        /// <summary>
        ///     An enum constant representing the singleton option.
        /// </summary>
        Singleton
    }
}