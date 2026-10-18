// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests.Net45
//  Author           : RzR
//  Created On       : 2025-08-11 19:40
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-11 23:50
// ***********************************************************************
//  <copyright file="ServiceOneTests.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using InvokeDependency.Tests.Net45.Abstractions;
using InvokeDependency.Tests.Net45.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.Infrastructure.DependencyResolver;
using RzR.Infrastructure.DependencyResolver.Abstractions;
using RzR.Infrastructure.DependencyResolver.Enums;

#endregion

namespace InvokeDependency.Tests.Net45.Tests.Transient
{
    [TestClass]
    public class ServiceOneTests
    {
        private static IInvokeDependencyContainer _container;

        [TestInitialize]
        public void Init()
        {
            _container = new InvokeDependencyContainer();

        }

        [TestMethod]
        public void Invoke_Service_Test()
        {
            _container.RegisterService<IServiceOne, ServiceOne>(InvokeDependencyServiceLifetime.Transient);

            var service = _container.GetService<IServiceOne>();

            Assert.IsNotNull(service);

            var serviceResult = service.GetIntValue();
            Assert.IsNotNull(serviceResult);
            Assert.AreEqual(1, serviceResult);
        }

        [TestMethod]
        public void Invoke_Same_Service_Test()
        {
            _container.RegisterService<IServiceOne, ServiceOne>(InvokeDependencyServiceLifetime.Transient);
            
            var service1 = _container.GetService<IServiceOne>();
            var service2 = _container.GetService<IServiceOne>();

            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);

            Assert.AreNotSame(service1, service2);

            Assert.AreNotEqual(service1.GetTransientOperationId(), service2.GetTransientOperationId());
        }

        [TestMethod]
        public void Invoke_Same_Service_2_Test()
        {
            var container = new InvokeDependencyContainer();
            _container.RegisterService<IServiceOne, ServiceOne>(InvokeDependencyServiceLifetime.Transient);
            _container.RegisterService<IServiceTwo, ServiceTwo>(InvokeDependencyServiceLifetime.Transient);
            _container.RegisterService<IService, Service>(InvokeDependencyServiceLifetime.Transient);

            var service1 = _container.GetService<IService>();
            var service2 = _container.GetService<IService>();

            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);

            Assert.AreNotSame(service1, service2);

            Assert.AreNotEqual(service1.GetTransientServiceId(), service2.GetTransientServiceId());
        }
    }
}