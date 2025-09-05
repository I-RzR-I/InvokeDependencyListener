// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests.Net45
//  Author           : RzR
//  Created On       : 2025-08-10 19:29
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-10 19:29
// ***********************************************************************
//  <copyright file="IServiceOne.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using System;

namespace InvokeDependency.Tests.Net45.Abstractions
{
    public interface IServiceOne
    {
        int GetIntValue();

        Guid GetTransientOperationId();

        Guid GetScopedOperationId();
        
        Guid GetSingletonOperationId();
    }
}