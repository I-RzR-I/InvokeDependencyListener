// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependencyListener
//  Author           : RzR
//  Created On       : 2025-08-07 19:53
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-01 18:17
// ***********************************************************************
//  <copyright file="ServiceStore.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using DomainCommonExtensions.CommonExtensions;
using DomainCommonExtensions.DataTypeExtensions;
using InvokeDependencyListener.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#endregion

namespace InvokeDependencyListener.Store
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A service store.
    /// </summary>
    /// =================================================================================================
    internal sealed class ServiceStore
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the compiled factories.
        /// </summary>
        /// =================================================================================================
        private readonly ConcurrentDictionary<Type, Func<object[], object>> _compiledFactories;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) stack of resolutions.
        /// </summary>
        /// =================================================================================================
        private readonly ThreadLocal<HashSet<Type>> _resolutionStack;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the scoped instances.
        /// </summary>
        /// =================================================================================================
        private readonly ThreadLocal<Dictionary<Type, object>> _scopedInstances;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the services.
        /// </summary>
        /// =================================================================================================
        private readonly ConcurrentDictionary<Type, InvokeDependencyServiceDescriptor> _services;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the singleton instances.
        /// </summary>
        /// =================================================================================================
        private readonly ConcurrentDictionary<Type, object> _singletonInstances;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     True if is disposed, false if not.
        /// </summary>
        /// =================================================================================================
        private bool _isDisposed;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     True if is resolution stack disposed, false if not.
        /// </summary>
        /// =================================================================================================
        private bool _isResolutionStackDisposed;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     True if is scoped disposed, false if not.
        /// </summary>
        /// =================================================================================================
        private bool _isScopedDisposed;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     True if is singleton disposed, false if not.
        /// </summary>
        /// =================================================================================================
        private bool _isSingletonDisposed;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Prevents a default instance of the <see cref="ServiceStore" /> class from being
        ///     created.
        /// </summary>
        /// =================================================================================================
        internal ServiceStore()
        {
            _compiledFactories = new ConcurrentDictionary<Type, Func<object[], object>>();
            _resolutionStack = new ThreadLocal<HashSet<Type>>(() => new HashSet<Type>());
            _scopedInstances = new ThreadLocal<Dictionary<Type, object>>(() => new Dictionary<Type, object>());
            _services = new ConcurrentDictionary<Type, InvokeDependencyServiceDescriptor>();
            _singletonInstances = new ConcurrentDictionary<Type, object>();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        /// </summary>
        /// =================================================================================================
        internal void Dispose()
        {
            if (_isDisposed.IsTrue())
                return;

            CleanSingletonServiceStore();
            CleanServiceStore();

            CleanCompiledFactoriesStore();

            CleanScopedStore();
            CleanResolutionStackStore();

            _isDisposed = true;
        }

        #region SERVICES

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Adds a service to local store.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="serviceDescriptor">Information describing the service.</param>
        /// =================================================================================================
        internal void AddServiceToLocalStore<T>(InvokeDependencyServiceDescriptor serviceDescriptor) 
            => _services[typeof(T)] = serviceDescriptor;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets service from local store.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <returns>
        ///     The service from local store.
        /// </returns>
        /// =================================================================================================
        internal object GetServiceFromLocalStore<T>()
        {
            if (_services.TryGetValue(typeof(T), out var serviceDescriptor).IsFalse())
                return null;

            return serviceDescriptor;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets service descriptor from local store.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        ///     The service descriptor from local store.
        /// </returns>
        /// =================================================================================================
        internal InvokeDependencyServiceDescriptor GetServiceDescriptorFromLocalStore(Type serviceType)
        {
            if (_services.TryGetValue(serviceType, out var serviceDescriptor).IsFalse())
                return null;

            return serviceDescriptor;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Clean service store.
        /// </summary>
        /// =================================================================================================
        internal void CleanServiceStore()
        {
            try
            {
                _services.Clear();
            }
            catch
            {
                /* ignored */
            }
        }

        #endregion

        #region SINGLETON

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Adds a singleton service to local store.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="instance">The instance.</param>
        /// =================================================================================================
        internal void AddSingletonServiceToLocalStore<T>(object instance)
            => _singletonInstances[typeof(T)] = instance;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets singleton service from local store.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <returns>
        ///     The singleton service from local store.
        /// </returns>
        /// =================================================================================================
        internal object GetSingletonServiceFromLocalStore<T>()
        {
            if (_singletonInstances.TryGetValue(typeof(T), out var serviceInstance).IsFalse())
                return null;

            return serviceInstance;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets singleton service from local store.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        ///     The singleton service from local store.
        /// </returns>
        /// =================================================================================================
        internal object GetSingletonServiceFromLocalStore(Type serviceType)
        {
            if (_singletonInstances.TryGetValue(serviceType, out var serviceInstance).IsFalse())
                return null;

            return serviceInstance;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets the singleton services from local stores in this collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the singleton services from local
        ///     stores in this collection.
        /// </returns>
        /// =================================================================================================
        internal IEnumerable<object> GetSingletonServicesFromLocalStore()
            => _singletonInstances.Values;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Clean singleton service store.
        /// </summary>
        /// =================================================================================================
        internal void CleanSingletonServiceStore()
        {
            try
            {
                _singletonInstances.Clear();
            }
            catch
            {
                /* ignored */
            }
            finally { _isSingletonDisposed = true; }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Query if this object is persisted singleton service.
        /// </summary>
        /// <returns>
        ///     True if persisted singleton service, false if not.
        /// </returns>
        /// =================================================================================================
        internal bool IsPersistedSingletonServiceStore()
        {
            if (_singletonInstances.IsNull() || _isSingletonDisposed.IsTrue() || _isDisposed.IsTrue())
                return false;

            return _singletonInstances.Count.IsGreaterThanZero();
        }

        #endregion

        #region SCOPED

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Adds a scoped service to local store to 'serviceInstance'.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="serviceInstance">The service instance.</param>
        /// =================================================================================================
        internal void AddScopedServiceToLocalStore(Type serviceType, object serviceInstance)
            => _scopedInstances.Value[serviceType] = serviceInstance;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets scoped service from local store.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        ///     The scoped service from local store.
        /// </returns>
        /// =================================================================================================
        internal object GetScopedServiceFromLocalStore(Type serviceType)
        {
            if (_scopedInstances.Value.TryGetValue(serviceType, out var instance))
                return instance;

            return null;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Clean scoped store.
        /// </summary>
        /// =================================================================================================
        internal void CleanScopedStore()
        {
            try
            {
                _scopedInstances.Dispose();
            }
            catch
            {
                /* ignored */
            }
            finally { _isScopedDisposed = true; }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Query if this object is persisted scoped service store.
        /// </summary>
        /// <returns>
        ///     True if persisted scoped service store, false if not.
        /// </returns>
        /// =================================================================================================
        internal bool IsPersistedScopedServiceStore()
        {
            if (_scopedInstances.IsNull() || _isScopedDisposed.IsTrue() || _isDisposed.IsTrue())
                return false;

            return _scopedInstances.Value.Count.IsGreaterThanZero();
        }

        #endregion

        #region RESOLUTIONSTACK

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Adds a resolution stack to local store.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// =================================================================================================
        internal void AddResolutionStackToLocalStore(Type serviceType)
            => _resolutionStack?.Value.Add(serviceType);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Query if 'serviceType' is defined service resolution stack from local store.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        ///     True if defined service resolution stack from local store, false if not.
        /// </returns>
        /// =================================================================================================
        internal bool IsDefinedServiceResolutionStackFromLocalStore(Type serviceType)
        {
            if (_isResolutionStackDisposed.IsTrue())
                return false;

            return _resolutionStack.Value.Contains(serviceType);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets defined services name resolution stack from local store.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        ///     The defined services name resolution stack from local store.
        /// </returns>
        /// =================================================================================================
        internal string GetDefinedServicesNameResolutionStackFromLocalStore(Type serviceType)
        {
            return string.Join(" -> ", _resolutionStack.Value.Select(t => t.Name)) + " -> " + serviceType.Name;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Removes the resolution stack from store described by serviceType.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// =================================================================================================
        internal void RemoveResolutionStackFromStore(Type serviceType)
            => _resolutionStack.Value.Remove(serviceType);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Clean resolution stack store.
        /// </summary>
        /// =================================================================================================
        internal void CleanResolutionStackStore()
        {
            try
            {
                _resolutionStack.Dispose();
            }
            catch
            {
                /* ignored */
            }
            finally { _isResolutionStackDisposed = true; }
        }

        #endregion

        #region COMPILEDFACTORIES

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets compiled factories from local store.
        /// </summary>
        /// <returns>
        ///     The compiled factories from local store.
        /// </returns>
        /// =================================================================================================
        internal ConcurrentDictionary<Type, Func<object[], object>> GetCompiledFactoriesFromLocalStore()
            => _compiledFactories;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Clean compiled factories store.
        /// </summary>
        /// =================================================================================================
        internal void CleanCompiledFactoriesStore()
        {
            try
            {
                _compiledFactories.Clear();
            }
            catch
            {
                /* ignored */
            }
        }

        #endregion
    }
}