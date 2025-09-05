// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests.Net45
//  Author           : RzR
//  Created On       : 2025-08-11 19:21
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-11 19:21
// ***********************************************************************
//  <copyright file="IService.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using System;

namespace InvokeDependency.Tests.Net45.Abstractions
{
    public interface IService
    {
        string GetValue();

        Guid GetTransientServiceId();

        Guid GetScopedServiceId();

        Guid GetSingletonServiceId();
    }
}