// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests.Net45
//  Author           : RzR
//  Created On       : 2025-09-02 17:53
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-02 17:53
// ***********************************************************************
//  <copyright file="ServiceX.cs" company="RzR SOFT & TECH">
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
    public class ServiceX: IServiceX
    {
        /// <inheritdoc />
        public Guid GetId() => Guid.NewGuid();
    }
}