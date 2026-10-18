// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests
//  Author           : RzR
//  Created On       : 2025-09-03 20:18
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-03 20:20
// ***********************************************************************
//  <copyright file="ServiceTwoTests.cs" company="RzR SOFT & TECH">
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
    public class ServiceTwoTests
    {
        private static IInvokeDependencyContainer _container;

        //[TestInitialize]
        public void Init()
        {
            _container = new InvokeDependencyContainer();

            _container.RegisterService<IServiceTwo, ServiceTwo>(InvokeDependencyServiceLifetime.Singleton);
        }

        //[TestMethod]
        public void Invoke_Service_Test()
        {
            var service = _container.GetService<IServiceTwo>();
            Assert.IsNotNull(service);

            var serviceResult = service.GetStringValue();
            Assert.IsNotNull(serviceResult);
            Assert.AreEqual("test", serviceResult);
        }
    }
}