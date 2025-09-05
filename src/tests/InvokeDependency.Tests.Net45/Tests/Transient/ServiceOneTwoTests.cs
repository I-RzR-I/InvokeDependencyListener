// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests.Net45
//  Author           : RzR
//  Created On       : 2025-08-11 23:48
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-11 23:50
// ***********************************************************************
//  <copyright file="ServiceOneTwoTests.cs" company="RzR SOFT & TECH">
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

namespace InvokeDependency.Tests.Net45.Tests.Transient
{
    [TestClass]
    public class ServiceOneTwoTests
    {
        private static IInvokeDependencyContainer _container;

        [TestInitialize]
        public void Init()
        {
            _container = new InvokeDependencyContainer();

            _container.RegisterService<IServiceOne, ServiceOne>(InvokeDependencyServiceLifetime.Transient);
            _container.RegisterService<IServiceTwo, ServiceTwo>(InvokeDependencyServiceLifetime.Transient);
            _container.RegisterService<IService, Service>(InvokeDependencyServiceLifetime.Transient);
        }

        [TestMethod]
        public void Invoke_Service_Test()
        {
            var service = _container.GetService<IService>();
            Assert.IsNotNull(service);

            var serviceResult = service.GetValue();
            Assert.IsNotNull(serviceResult);
            Assert.AreEqual("test_1", serviceResult);
        }
    }
}