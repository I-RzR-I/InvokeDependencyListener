// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependencyListener
//  Author           : RzR
//  Created On       : 2025-09-02 15:58
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-02 15:59
// ***********************************************************************
//  <copyright file="DependencyResolverConfig.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace RzR.Infrastructure.DependencyResolver.Models
{
    /// <summary>
    ///     A dependency resolver configuration.
    /// </summary>
    public class DependencyResolverConfig
    {
        /// <summary>
        ///     Gets or sets a value indicating whether the resolve by interface.
        /// </summary>
        /// <value>
        ///     True if resolve be interface, false if not.
        /// </value>
        /// <example>
        ///     <code>
        ///         public interface IService { }
        ///         public class Service : IService { }
        ///         
        ///         This line is not written, forgot to register service;
        ///          /*_container.RegisterService&lt;IService, Service&gt;(InvokeDependencyServiceLifetime.Scoped);*/
        ///          
        ///          var serviceX = _container.GetService&lt;IService&gt;
        ///          // When <seealso cref="ResolveByInterface"/> is true, than `serviceX` is instance of the type of Service
        ///     </code>
        /// </example>
        /// <remarks>
        ///     It can take more execution time, as it scans the assembly and declared types.
        /// </remarks>
        public bool ResolveByInterface { get; private set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyResolverConfig"/> class.
        /// </summary>
        public DependencyResolverConfig() => ResolveByInterface = false;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyResolverConfig"/> class.
        /// </summary>
        /// <param name="resolveByInterface">
        ///     True if resolve by interface inheritance, false if not.
        /// </param>
        public DependencyResolverConfig(bool resolveByInterface) => ResolveByInterface = resolveByInterface;
    }
}