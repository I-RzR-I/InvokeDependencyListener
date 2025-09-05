// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests.Net45
//  Author           : RzR
//  Created On       : 2025-08-10 19:31
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-10 19:31
// ***********************************************************************
//  <copyright file="ServiceOne.cs" company="RzR SOFT & TECH">
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
    public class ServiceOne: IServiceOne
    {
        private readonly Guid _scopedId;
        private readonly Guid _transientId;
        private readonly Guid _singletonId;

        public ServiceOne()
        {
            _scopedId = Guid.NewGuid();
            _transientId = Guid.NewGuid();
            _singletonId = Guid.NewGuid();
        }

        /// <inheritdoc />
        public int GetIntValue() => 1;

        /// <inheritdoc />
        public Guid GetTransientOperationId() => _transientId;

        /// <inheritdoc />
        public Guid GetScopedOperationId() => _scopedId;

        /// <inheritdoc />
        public Guid GetSingletonOperationId() => _singletonId;
    }
}