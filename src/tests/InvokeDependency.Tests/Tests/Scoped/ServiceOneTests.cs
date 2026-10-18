// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests
//  Author           : RzR
//  Created On       : 2025-09-03 20:18
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-03 20:20
// ***********************************************************************
//  <copyright file="ServiceOneTests.cs" company="RzR SOFT & TECH">
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
    public class ServiceOneTests
    {
        private static IInvokeDependencyContainer _container;

        [TestInitialize]
        public void Init() => _container = new InvokeDependencyContainer();

        [TestMethod]
        public void Invoke_Service_Test()
        {
            _container.RegisterService<IServiceOne, ServiceOne>();
            var service = _container.GetService<IServiceOne>();

            Assert.IsNotNull(service);

            var serviceResult = service.GetIntValue();
            Assert.IsNotNull(serviceResult);
            Assert.AreEqual(1, serviceResult);
        }

        [TestMethod]
        public void Invoke_Scoped_Service_Test()
        {
            _container.RegisterService<IServiceOne, ServiceOne>();

            var service1 = _container.GetService<IServiceOne>();
            var service2 = _container.GetService<IServiceOne>();

            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);

            var id1 = service1.GetScopedOperationId();
            var id2 = service2.GetScopedOperationId();

            Assert.AreEqual(id1, id2);
        }
    }
}