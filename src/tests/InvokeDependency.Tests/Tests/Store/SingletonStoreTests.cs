// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests
//  Author           : RzR
//  Created On       : 2025-09-03 20:18
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-09-03 20:20
// ***********************************************************************
//  <copyright file="SingletonStoreTests.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using InvokeDependency.Tests.Services;
using InvokeDependencyListener.Store;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

#endregion

namespace InvokeDependency.Tests.Tests.Store
{
    [TestClass]
    public class SingletonStoreTests
    {
        [TestMethod]
        public void Singleton_001_Check_Store()
        {
            var store = new ServiceStore();

            var isPersisted = store.IsPersistedSingletonServiceStore();
            var allService = store.GetSingletonServicesFromLocalStore();

            Assert.IsFalse(isPersisted);
            Assert.AreEqual(0, allService.Count());
        }

        [TestMethod]
        public void Singleton_002_AddSingletonServiceToLocalStore_Test()
        {
            var store = new ServiceStore();
            AddService(ref store);

            var isPersisted = store.IsPersistedSingletonServiceStore();
            var allService = store.GetSingletonServicesFromLocalStore();

            Assert.IsTrue(isPersisted);
            Assert.AreEqual(2, allService.Count());
        }

        [TestMethod]
        public void Singleton_003_GetSingletonServiceFromLocalStore_Test()
        {
            var store = new ServiceStore();
            AddService(ref store);

            var service = store.GetSingletonServiceFromLocalStore(typeof(ServiceOne));
            Assert.IsNotNull(service);

            var service2 = store.GetSingletonServiceFromLocalStore(typeof(ServiceTwo));
            Assert.IsNotNull(service2);
        }

        [TestMethod]
        public void Singleton_004_CleanSingletonServiceStore_Test()
        {
            var store = new ServiceStore();
            AddService(ref store);

            store.CleanSingletonServiceStore();

            var isPersisted = store.IsPersistedSingletonServiceStore();
            var allService = store.GetSingletonServicesFromLocalStore();

            Assert.IsFalse(isPersisted);
            Assert.AreEqual(0, allService.Count());
        }

        [TestMethod]
        public void Singleton_005_Dispose_Test()
        {
            var store = new ServiceStore();
            AddService(ref store);

            store.Dispose();

            var isPersisted = store.IsPersistedSingletonServiceStore();
            var allService = store.GetSingletonServicesFromLocalStore();

            Assert.IsFalse(isPersisted);
            Assert.AreEqual(0, allService.Count());
        }

        private static void AddService(ref ServiceStore store)
        {
            store.AddSingletonServiceToLocalStore<ServiceOne>(new ServiceOne());
            store.AddSingletonServiceToLocalStore<ServiceTwo>(new ServiceTwo());
        }
    }
}