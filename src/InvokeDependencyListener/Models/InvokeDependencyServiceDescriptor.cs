// ***********************************************************************
//  Assembly         : RzR.Infrastructure.DependencyResolver
//  Author           : RzR
//  Created On       : 2025-08-07 15:41
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-07 17:31
// ***********************************************************************
//  <copyright file="InvokeDependencyServiceDescriptor.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using RzR.Extensions.Domain.Primitives;
using RzR.Infrastructure.DependencyResolver.Abstractions;
using RzR.Infrastructure.DependencyResolver.Enums;
using System;

#endregion

namespace RzR.Infrastructure.DependencyResolver.Models
{
    /// <summary>
    ///     An invoke dependency service descriptor.
    /// </summary>
    internal sealed class InvokeDependencyServiceDescriptor
    {
        /// <summary>
        ///     Gets or sets the type of the service.
        /// </summary>
        /// <value>
        ///     The type of the service.
        /// </value>
        public Type ServiceType { get; set; }

        /// <summary>
        ///     Gets or sets the type of the implementation.
        /// </summary>
        /// <value>
        ///     The type of the implementation.
        /// </value>
        public Type ImplementationType { get; set; }

        /// <summary>
        ///     Gets or sets the lifetime.
        /// </summary>
        /// <value>
        ///     The lifetime.
        /// </value>
        public InvokeDependencyServiceLifetime Lifetime { get; set; }

        /// <summary>
        ///     Gets or sets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public object Instance { get; set; }

        /// <summary>
        ///     Gets or sets the factory.
        /// </summary>
        /// <value>
        ///     A function delegate that yields an object.
        /// </value>
        public Func<IInvokeDependencyContainer, object> ServiceFactory { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this object is factory registration.
        /// </summary>
        /// <value>
        ///     True if this object is factory registration, false if not.
        /// </value>
        public bool IsFactoryRegistration => ServiceFactory.IsNotNull();
    }
}