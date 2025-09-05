// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests.Net45
//  Author           : RzR
//  Created On       : 2025-08-10 19:31
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-10 19:31
// ***********************************************************************
//  <copyright file="Service.cs" company="RzR SOFT & TECH">
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
    public class Service: IService
    {
        private readonly IServiceOne _serviceOne;
        private readonly IServiceTwo _serviceTwo;

        public Service(IServiceOne serviceOne, IServiceTwo serviceTwo)
        {
            _serviceOne = serviceOne;
            _serviceTwo = serviceTwo;
        }

        /// <inheritdoc />
        public string GetValue()
        {
            var s1Result = _serviceOne.GetIntValue();
            var s2Result = _serviceTwo.GetStringValue();

            return $"{s2Result}_{s1Result}";
        }

        /// <inheritdoc />
        public Guid GetTransientServiceId() => _serviceOne.GetTransientOperationId();

        /// <inheritdoc />
        public Guid GetScopedServiceId() => _serviceOne.GetScopedOperationId();

        /// <inheritdoc />
        public Guid GetSingletonServiceId() => _serviceOne.GetSingletonOperationId();
    }
}