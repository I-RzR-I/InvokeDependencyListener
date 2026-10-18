// ***********************************************************************
//  Assembly         : RzR.Infrastructure.DependencyResolver
//  Author           : RzR
//  Created On       : 2025-08-07 15:48
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-07 17:32
// ***********************************************************************
//  <copyright file="InvokeDependencyServiceScope.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using RzR.Extensions.Domain.Primitives;
using RzR.Extensions.Domain.Validation;
using RzR.Infrastructure.DependencyResolver.Abstractions;
using System;
using System.Collections.Generic;

#endregion

namespace RzR.Infrastructure.DependencyResolver
{
    /// <summary>
    ///     An invoke dependency service scope.
    /// </summary>
    /// <seealso cref="T:RzR.Infrastructure.DependencyResolver.Abstractions.IInvokeDependencyServiceScope"/>
    public sealed class InvokeDependencyServiceScope : IInvokeDependencyServiceScope
    {
        /// <summary>
        ///     (Immutable) the dependency container.
        /// </summary>
        private readonly IInvokeDependencyContainer _container;

        /// <summary>
        ///     (Immutable) the scoped registered instances.
        /// </summary>
        private readonly Dictionary<Type, object> _scopedInstances = new Dictionary<Type, object>();

        /// <summary>
        ///     True if scoped service is disposed, otherwise False.
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvokeDependencyServiceScope"/> class.
        /// </summary>
        /// <param name="container">The dependency container.</param>
        internal InvokeDependencyServiceScope(IInvokeDependencyContainer container) 
            => _container = container;

        /// <inheritdoc/>
        public T GetService<T>() 
            => GetService(typeof(T)).To<T>();

        /// <inheritdoc/>
        public object GetService(Type serviceType)
        {
            DomainEnsure.IsNotNull(serviceType, nameof(serviceType));

            if (_scopedInstances.TryGetValue(serviceType, out var serviceInstance))
                return serviceInstance;
            
            serviceInstance = _container.GetService(serviceType);

            if (serviceInstance.IsNotNull())
                _scopedInstances[serviceType] = serviceInstance;

            return serviceInstance;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_disposed.IsTrue())
                return;

            foreach (var serviceInstance in _scopedInstances.Values)
            {
                if (serviceInstance is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            _scopedInstances.Clear();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            _disposed = true;
        }

        /// <inheritdoc />
        ~InvokeDependencyServiceScope() => Dispose();
    }
}