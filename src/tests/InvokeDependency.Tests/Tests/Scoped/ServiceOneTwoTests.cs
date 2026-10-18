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

#endregion

namespace InvokeDependency.Tests.Tests.Scoped
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
            _container.RegisterService<IServiceOne, ServiceOne>();
            _container.RegisterService<IServiceTwo, ServiceTwo>();
            _container.RegisterService<IService, Service>();

            var service = _container.GetService<IService>();
            Assert.IsNotNull(service);

            var serviceResult = service.GetValue();
            Assert.IsNotNull(serviceResult);
            Assert.AreEqual("test_1", serviceResult);
        }

        [TestMethod]
        public void Invoke_Same_Service_2_Test()
        {
            _container.RegisterService<IServiceOne, ServiceOne>();
            _container.RegisterService<IServiceTwo, ServiceTwo>();
            _container.RegisterService<IService, Service>();

            var baseId = _container.GetRequiredService<IService>().GetScopedServiceId();

            using (var scope1 = _container.CreateScope())
            {
                var service1 = scope1.GetService<IService>();
                var service2 = scope1.GetService<IService>();

                Assert.IsNotNull(service1);
                Assert.IsNotNull(service2);
                Assert.AreEqual(service1.GetScopedServiceId(), service2.GetScopedServiceId());
            }

            using (var scope2 = _container.CreateScope())
            {
                var service3 = scope2.GetService<IService>();
                var service4 = scope2.GetService<IService>();

                Assert.IsNotNull(service3);
                Assert.IsNotNull(service4);
                Assert.AreEqual(service3.GetScopedServiceId(), service4.GetScopedServiceId());
                Assert.AreNotEqual(service3.GetScopedServiceId(), baseId);
            }
        }
    }
}