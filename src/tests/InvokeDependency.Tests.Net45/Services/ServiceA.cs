// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests.Net45
//  Author           : RzR
//  Created On       : 2025-09-02 20:28
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-02 20:28
// ***********************************************************************
//  <copyright file="ServiceA.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using InvokeDependency.Tests.Net45.Abstractions;
using System;

namespace InvokeDependency.Tests.Net45.Services
{
    internal class ServiceA: IServiceA
    {
        /// <inheritdoc />
        public Guid GetId() => Guid.NewGuid();
    }
}