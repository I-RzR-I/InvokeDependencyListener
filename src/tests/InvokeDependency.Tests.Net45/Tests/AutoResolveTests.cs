// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests.Net45
//  Author           : RzR
//  Created On       : 2025-09-02 17:56
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-02 17:56
// ***********************************************************************
//  <copyright file="AutoResolveTests.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using InvokeDependency.Tests.Net45.Abstractions;
using InvokeDependency.Tests.Net45.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.Infrastructure.DependencyResolver;
using RzR.Infrastructure.DependencyResolver.Abstractions;
using RzR.Infrastructure.DependencyResolver.Models;
using System;

namespace InvokeDependency.Tests.Net45.Tests
{
    [TestClass]
    public class AutoResolveTests
    {
        private static IInvokeDependencyContainer _container;

        [TestInitialize]
        public void Init()
        {
            _container = new InvokeDependencyContainer(
                new DependencyResolverConfig(true));
        }

        [TestMethod]
        public void AutoResolve_GetService_Pass_Test()
        {
            var service = _container.GetService<IServiceX>();

            Assert.IsNotNull(service);

            var id = service.GetId();
            Assert.IsNotNull(id);
            Assert.AreNotEqual(Guid.Empty, id);
        }

        [TestMethod]
        public void AutoResolve_GetRequiredService_Pass_Test()
        {
            var service = _container.GetRequiredService<IServiceX>();

            Assert.IsNotNull(service);

            var id = service.GetId();
            Assert.IsNotNull(id);
            Assert.AreNotEqual(Guid.Empty, id);
        }

        [TestMethod]
        public void AutoResolve_GetService_Null_Interface_Pass_Test()
        {
            var service = _container.GetService<IServiceY>();

            Assert.IsNull(service);
        }

        [TestMethod]
        public void AutoResolve_GetService_Class_Pass_Test()
        {
            var service = _container.GetService<ServiceY>();

            Assert.IsNotNull(service);

            var id = service.GetId();
            Assert.IsNotNull(id);
            Assert.AreEqual(Guid.Empty, id);
        }

        [TestMethod]
        public void AutoResolve_GetService_Null_Interface_Internal_Class_Pass_Test()
        {
            var service = _container.GetService<IServiceA>();

            Assert.IsNull(service);
        }

        [TestMethod]
        public void AutoResolve_GetService_Null_Internal_Class_Pass_Test()
        {
            var service = _container.GetService<ServiceA>();

            Assert.IsNull(service);
        }
    }
}