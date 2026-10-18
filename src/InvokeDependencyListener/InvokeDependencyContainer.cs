// ***********************************************************************
//  Assembly         : RzR.Infrastructure.DependencyResolver
//  Author           : RzR
//  Created On       : 2025-08-07 15:39
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-07 17:32
// ***********************************************************************
//  <copyright file="InvokeDependencyContainer.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using RzR.Extensions.Domain.Collections;
using RzR.Extensions.Domain.Primitives;
using RzR.Extensions.Domain.Reflection.TypeParam;
using RzR.Extensions.Domain.Validation;
using RzR.Infrastructure.DependencyResolver.Abstractions;
using RzR.Infrastructure.DependencyResolver.Enums;
using RzR.Infrastructure.DependencyResolver.Exceptions;
using RzR.Infrastructure.DependencyResolver.Extensions;
using RzR.Infrastructure.DependencyResolver.Helpers;
using RzR.Infrastructure.DependencyResolver.Models;
using RzR.Infrastructure.DependencyResolver.Store;
using System;
using System.Linq;

#endregion

namespace RzR.Infrastructure.DependencyResolver
{
    /// <summary>
    ///     Container for invoke dependencies.
    /// </summary>
    /// <seealso cref="T:RzR.Infrastructure.DependencyResolver.Abstractions.IInvokeDependencyContainer"/>
    public sealed class InvokeDependencyContainer : IInvokeDependencyContainer
    {
        /// <summary>
        ///     (Immutable) the dependency resolver configuration.
        /// </summary>
        private readonly DependencyResolverConfig _dependencyResolverConfig;

        /// <summary>
        ///     (Immutable) the services store.
        /// </summary>
        private readonly ServiceStore _store;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvokeDependencyContainer"/> class.
        /// </summary>
        /// <param name="dependencyResolverConfig">
        ///     (Optional)
        ///     (Immutable) the dependency resolver configuration.
        /// </param>
        public InvokeDependencyContainer(DependencyResolverConfig dependencyResolverConfig = null)
        {
            _dependencyResolverConfig = dependencyResolverConfig.IfIsNull(new DependencyResolverConfig());
            _store = new ServiceStore();
        }

        /// <summary>
        ///     True if disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     True if is scoped context, false if not.
        /// </summary>
        private bool _isScopedContext;

        /// <inheritdoc/>
        public void RegisterService<TImplementation>(
            TImplementation instance,
            InvokeDependencyServiceLifetime lifetime = InvokeDependencyServiceLifetime.Scoped)
            where TImplementation : class
        {
            DomainEnsure.IsNotNull(instance, nameof(instance));
            DomainEnsure.IsValidEnum<InvokeDependencyServiceLifetime>(lifetime, nameof(lifetime));

            InternalRegisterService(instance, lifetime);
        }

        /// <inheritdoc/>
        public void RegisterService<TInterface, TImplementation>(
            InvokeDependencyServiceLifetime lifetime = InvokeDependencyServiceLifetime.Scoped)
            where TImplementation : class, TInterface
        {
            DomainEnsure.IsValidEnum<InvokeDependencyServiceLifetime>(lifetime, nameof(lifetime));

            InternalRegisterService<TInterface, TImplementation>(lifetime);
        }

        /// <inheritdoc/>
        public void RegisterFactory<T>(
            Func<IInvokeDependencyContainer, T> factory,
            InvokeDependencyServiceLifetime lifetime = InvokeDependencyServiceLifetime.Scoped)
        {
            DomainEnsure.IsNotNull(factory, nameof(factory));
            DomainEnsure.IsValidEnum<InvokeDependencyServiceLifetime>(lifetime, nameof(lifetime));

            _store.AddServiceToLocalStore<T>(new InvokeDependencyServiceDescriptor
            {
                ServiceType = typeof(T),
                Lifetime = lifetime,
                ServiceFactory = container => factory(container)
            });
        }

        /// <inheritdoc/>
        public T GetService<T>() => (T)GetService(typeof(T));

        /// <inheritdoc/>
        public object GetService(Type serviceType) => VerifyAndGetServiceFunc(serviceType);

        /// <inheritdoc/>
        public T GetRequiredService<T>()
        {
            var service = GetService<T>();

            if (service.IsNull())
                throw new InvalidOperationException(
                    DefaultMessagesHelper.FormatToDefault(
                        nameof(InvokeDependencyContainer),
                        DefaultMessagesHelper.RequiredServiceNotRegistered,
                        typeof(T).Name));

            return service;
        }

        /// <inheritdoc/>
        public object GetRequiredService(Type serviceType)
        {
            DomainEnsure.IsNotNull(serviceType, nameof(serviceType));

            var service = GetService(serviceType);

            if (service.IsNull())
                throw new InvalidOperationException(
                    DefaultMessagesHelper.FormatToDefault(
                        nameof(InvokeDependencyContainer),
                        DefaultMessagesHelper.RequiredServiceNotRegistered,
                        serviceType.Name));

            return service;
        }

        /// <inheritdoc/>
        public IInvokeDependencyServiceScope CreateScope()
        {
            _isScopedContext = true;
            var scope = new InvokeDependencyServiceScope(this);

            return scope;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_disposed.IsTrue())
                return;

            // Dispose all singleton instances
            foreach (var instance in _store.GetSingletonServicesFromLocalStore())
                if (instance is IDisposable disposable)
                    disposable.Dispose();

            _store.CleanSingletonServiceStore();
            _store.CleanServiceStore();

            _store.CleanCompiledFactoriesStore();

            _store.CleanScopedStore();
            _store.CleanResolutionStackStore();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            _disposed = true;
        }

        /// <summary>
        ///     Registers the service described by lifetime.
        /// </summary>
        /// <typeparam name="TInterface">Type of the interface.</typeparam>
        /// <typeparam name="TImplementation">Type of the implementation.</typeparam>
        /// <param name="lifetime">The lifetime.</param>
        private void InternalRegisterService<TInterface, TImplementation>(
            InvokeDependencyServiceLifetime lifetime)
            where TImplementation : class, TInterface
        {
            DomainEnsure.IsValidEnum<InvokeDependencyServiceLifetime>(lifetime, nameof(lifetime));

            _store.AddServiceToLocalStore<TInterface>(
                new InvokeDependencyServiceDescriptor
                {
                    ServiceType = typeof(TInterface),
                    ImplementationType = typeof(TImplementation),
                    Lifetime = lifetime
                });

            if (lifetime.AreEquals(InvokeDependencyServiceLifetime.Singleton))
                _store.AddSingletonServiceToLocalStore<TInterface>(CreateInstance(typeof(TImplementation)));
        }

        /// <summary>
        ///     Registers the service described by lifetime.
        /// </summary>
        /// <typeparam name="TImplementation">Type of the implementation.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="lifetime">The lifetime.</param>
        private void InternalRegisterService<TImplementation>(
            TImplementation instance, InvokeDependencyServiceLifetime lifetime)
            where TImplementation : class
        {
            DomainEnsure.IsNotNull(instance, nameof(instance));
            DomainEnsure.IsValidEnum<InvokeDependencyServiceLifetime>(lifetime, nameof(lifetime));

            _store.AddServiceToLocalStore<TImplementation>(new InvokeDependencyServiceDescriptor
            {
                ServiceType = typeof(TImplementation),
                Lifetime = lifetime,
                Instance = instance
            });

            if (lifetime.AreEquals(InvokeDependencyServiceLifetime.Singleton))
                _store.AddSingletonServiceToLocalStore<TImplementation>(instance);
        }

        /// <summary>
        ///     Resolve service.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when one or more arguments are outside the required range.
        /// </exception>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns>
        ///     An object.
        /// </returns>
        private object ResolveService(InvokeDependencyServiceDescriptor descriptor)
        {
            DomainEnsure.IsNotNull(descriptor, nameof(descriptor));

            // Handle pre-registered instances
            if (descriptor.Instance.IsNotNull())
                return descriptor.Instance;

            // Handle factory registrations
            if (descriptor.IsFactoryRegistration)
                return descriptor.ServiceFactory(this);

            if (descriptor.Lifetime.AreEquals(InvokeDependencyServiceLifetime.Scoped) && _isScopedContext.IsTrue())
                return CreateInstance(descriptor.ImplementationType);

            var serviceImplementation = descriptor.Lifetime switch
            {
                InvokeDependencyServiceLifetime.Singleton => GetSingleton(descriptor),
                InvokeDependencyServiceLifetime.Scoped => GetScoped(descriptor),
                InvokeDependencyServiceLifetime.Transient => CreateInstance(descriptor.ImplementationType),
                _ => throw new ArgumentOutOfRangeException(
                    DefaultMessagesHelper.FormatToDefault(nameof(InvokeDependencyContainer),
                        DefaultMessagesHelper.NotAcceptedServiceLifeTime, descriptor.Lifetime))
            };

            return serviceImplementation;
        }

        /// <summary>
        ///     Gets a singleton.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns>
        ///     The singleton.
        /// </returns>
        private object GetSingleton(InvokeDependencyServiceDescriptor descriptor)
        {
            DomainEnsure.IsNotNull(descriptor, nameof(descriptor));

            var service = _store.GetSingletonServiceFromLocalStore(descriptor.ServiceType);

            service = service.IfIsNull(CreateInstance(descriptor.ImplementationType));

            return service;
        }

        /// <summary>
        ///     Gets a scoped.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns>
        ///     The scoped.
        /// </returns>
        private object GetScoped(InvokeDependencyServiceDescriptor descriptor)
        {
            DomainEnsure.IsNotNull(descriptor, nameof(descriptor));

            var scopedInstance = _store.GetScopedServiceFromLocalStore(descriptor.ServiceType);
            if (scopedInstance.IsNotNull())
                return scopedInstance;

            var instance = CreateInstance(descriptor.ImplementationType);
            _store.AddScopedServiceToLocalStore(descriptor.ServiceType, instance);

            return instance;
        }

        /// <summary>
        ///     Creates an instance.
        /// </summary>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns>
        ///     The new instance.
        /// </returns>
        private object CreateInstance(Type implementationType)
        {
            DomainEnsure.IsNotNull(implementationType, nameof(implementationType));

            return ServiceInstanceBuildHelper.Instance.CreateInstance(
                implementationType,
                _store.GetCompiledFactoriesFromLocalStore(),
                VerifyAndGetServiceFunc);
        }

        /// <summary>
        ///     Verify and get service function.
        /// </summary>
        /// <exception cref="CyclicDependencyException">
        ///     Thrown when a Cyclic Dependency error condition occurs.
        /// </exception>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        ///     An object.
        /// </returns>
        private object VerifyAndGetServiceFunc(Type serviceType)
        {
            DomainEnsure.IsNotNull(serviceType, nameof(serviceType));

            var serviceDescriptor = _store.GetServiceDescriptorFromLocalStore(serviceType);
            if (serviceDescriptor.IsNull())
            {
                if (_dependencyResolverConfig.ResolveByInterface.IsTrue())
                {
                    if (serviceType.IsInterface.IsTrue())
                    {
                        var types = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(x => x.GetTypes().GetAvailableTypes(serviceType))
                            .Select(x => x).NotNull();

                        var firstType = types.FirstOrDefault();

                        return firstType.IsNull()
                            ? null
                            : CreateInstance(firstType);
                    }
                    else
                    {
                        return serviceType.IsAvailableForInstance()
                            ? CreateInstance(serviceType)
                            : null;
                    }
                }
                else
                    return null;
            }

            // Circular dependency detection
            if (_store.IsDefinedServiceResolutionStackFromLocalStore(serviceType))
            {
                var message = DefaultMessagesHelper.FormatToDefault(
                    nameof(InvokeDependencyContainer),
                    DefaultMessagesHelper.CircularDependency,
                    _store.GetDefinedServicesNameResolutionStackFromLocalStore(serviceType));

                throw new CyclicDependencyException(message);
            }

            _store.AddResolutionStackToLocalStore(serviceType);

            try
            {
                return ResolveService(serviceDescriptor);
            }
            finally
            {
                _store.RemoveResolutionStackFromStore(serviceType);
            }
        }

        /// <inheritdoc/>
        ~InvokeDependencyContainer() => Dispose();
    }
}