// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests
//  Author           : RzR
//  Created On       : 2025-09-03 20:18
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-03 20:20
// ***********************************************************************
//  <copyright file="ServiceOne.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using InvokeDependency.Tests.Abstractions;
using System;

#endregion

namespace InvokeDependency.Tests.Services
{
    public class ServiceOne : IServiceOne
    {
        private readonly Guid _scopedId;
        private readonly Guid _singletonId;
        private readonly Guid _transientId;

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