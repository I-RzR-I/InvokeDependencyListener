// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests
//  Author           : RzR
//  Created On       : 2025-09-03 20:18
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-03 20:20
// ***********************************************************************
//  <copyright file="ServiceOneTwoTests.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using InvokeDependency.Tests.Abstractions;
using InvokeDependency.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.Infrastructure.DependencyResolver;
using RzR.Infrastructure.DependencyResolver.Abstractions;
using RzR.Infrastructure.DependencyResolver.Enums;

#endregion

namespace InvokeDependency.Tests.Tests.Singleton
{
    [TestClass]
    public class ServiceOneTwoTests
    {
        private static IInvokeDependencyContainer _container;

        [TestInitialize]
        public void Init() => _container = new InvokeDependencyContainer();

        [TestMethod]
        public void Invoke_Service_Test()
        {
            _container.RegisterService<IServiceOne, ServiceOne>(InvokeDependencyServiceLifetime.Singleton);
            _container.RegisterService<IServiceTwo, ServiceTwo>(InvokeDependencyServiceLifetime.Singleton);
            _container.RegisterService<IService, Service>(InvokeDependencyServiceLifetime.Singleton);

            var service = _container.GetService<IService>();
            Assert.IsNotNull(service);

            var serviceResult = service.GetValue();
            Assert.IsNotNull(serviceResult);
            Assert.AreEqual("test_1", serviceResult);
        }

        [TestMethod]
        public void Invoke_Same_Service_2_Test()
        {
            _container.RegisterService<IServiceOne, ServiceOne>(InvokeDependencyServiceLifetime.Singleton);
            _container.RegisterService<IServiceTwo, ServiceTwo>(InvokeDependencyServiceLifetime.Singleton);
            _container.RegisterService<IService, Service>(InvokeDependencyServiceLifetime.Singleton);


            var service1 = _container.GetService<IService>();
            var service2 = _container.GetService<IService>();

            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);
            Assert.AreEqual(service1.GetSingletonServiceId(), service2.GetSingletonServiceId());
        }
    }
}