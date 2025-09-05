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
using InvokeDependencyListener;
using InvokeDependencyListener.Abstractions;
using InvokeDependencyListener.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace InvokeDependency.Tests.Net45.Tests.Singleton
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
            _container.RegisterService<IServiceOne, ServiceOne>(InvokeDependencyServiceLifetime.Singleton);
            var service = _container.GetService<IServiceOne>();
            Assert.IsNotNull(service);

            var serviceResult = service.GetIntValue();
            Assert.IsNotNull(serviceResult);
            Assert.AreEqual(1, serviceResult);
        }

        [TestMethod]
        public void Invoke_Scoped_Service_Test()
        {
            _container.RegisterService<IServiceOne, ServiceOne>(InvokeDependencyServiceLifetime.Singleton);

            var service1 = _container.GetService<IServiceOne>();
            var service2 = _container.GetService<IServiceOne>();

            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);
            Assert.AreSame(service1, service2);

            var id1 = service1.GetSingletonOperationId();
            var id2 = service2.GetSingletonOperationId();

            Assert.AreEqual(id1, id2);
        }
    }
}