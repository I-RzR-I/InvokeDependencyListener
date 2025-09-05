// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests
//  Author           : RzR
//  Created On       : 2025-09-05 12:13
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-05 12:14
// ***********************************************************************
//  <copyright file="AutoResolveWithDiTests.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using InvokeDependency.Tests.Abstractions;
using InvokeDependency.Tests.Services;
using InvokeDependencyListener;
using InvokeDependencyListener.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InvokeDependency.Tests.Tests
{
    [TestClass]
    public class AutoResolveWithDiTests
    {
        private IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider;
        //private InvokeDependencyContainer _invokeDependencyContainer;

        [TestInitialize]
        public void Init()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddScoped<IServiceOne, ServiceOne>();
            _serviceCollection.AddScoped<IServiceTwo, ServiceTwo>();
            _serviceCollection.AddScoped<IService, Service>();
            _serviceProvider = _serviceCollection.BuildServiceProvider();

            //_invokeDependencyContainer = new InvokeDependencyContainer(new DependencyResolverConfig(true));
        }

        [TestMethod]
        public void Get_RegisterService_FromServiceProvider_Test()
        {
            var service1 = _serviceProvider.GetService<IService>();

            Assert.IsNotNull(service1);
            Assert.AreNotEqual(Guid.Empty, service1.GetScopedServiceId());

            //var service2 = _invokeDependencyContainer.GetService<IService>();
            var service2 = CoreClass.ServicesContainer.GetService<IService>();

            Assert.IsNotNull(service2);
            Assert.AreNotEqual(Guid.Empty, service2.GetScopedServiceId());
        }
    }
}