// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests.Net45
//  Author           : RzR
//  Created On       : 2025-08-10 19:31
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-10 19:31
// ***********************************************************************
//  <copyright file="ServiceTwo.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using InvokeDependency.Tests.Net45.Abstractions;

namespace InvokeDependency.Tests.Net45.Services
{
    public class ServiceTwo: IServiceTwo
    {
        /// <inheritdoc />
        public string GetStringValue() => "test";
    }
}