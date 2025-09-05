// ***********************************************************************
//  Assembly         : RzR.Shared.Services.InvokeDependency.Tests.Net45
//  Author           : RzR
//  Created On       : 2025-08-11 18:35
// 
//  Last Modified By : RzR
//  Last Modified On : 2025-08-11 18:35
// ***********************************************************************
//  <copyright file="ScopedStoreTests.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using InvokeDependencyListener.Store;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace InvokeDependency.Tests.Net45.Tests.Store
{
    [TestClass]
    public class ScopedStoreTests
    {
        [TestMethod]
        [DoNotParallelize]
        public void Scoped_001_Check_Store()
        {
            var store = new ServiceStore();

            var isPersisted = store.IsPersistedScopedServiceStore();

            Assert.IsFalse(isPersisted);
        }

        [TestMethod]
        [DoNotParallelize]
        public void Scoped_002_AddScopedServiceToLocalStore_Test()
        {
            var store = new ServiceStore();
            AddService(store: ref store);

            var isPersisted = store.IsPersistedScopedServiceStore();

            Assert.IsTrue(isPersisted);
        }

        [TestMethod]
        [DoNotParallelize]
        public void Scoped_003_GetScopedServiceFromLocalStore_Test()
        {
            var store = new ServiceStore();
            AddService(store: ref store);

            var service = store.GetScopedServiceFromLocalStore(typeof(Services.ServiceOne));
            Assert.IsNotNull(service);

            var service2 = store.GetScopedServiceFromLocalStore(typeof(Services.ServiceTwo));
            Assert.IsNotNull(service2);
        }

        [TestMethod]
        [DoNotParallelize]
        public void Scoped_004_CleanScopedStore_Test()
        {
            var store = new ServiceStore();
            AddService(store: ref store);

            store.CleanScopedStore();

            var isPersisted = store.IsPersistedScopedServiceStore();

            Assert.IsFalse(isPersisted);
        }

        [TestMethod]
        [DoNotParallelize]
        public void Scoped_005_Dispose_Test()
        {
            var store = new ServiceStore();

            store.Dispose();

            var isPersisted = store.IsPersistedScopedServiceStore();

            Assert.IsFalse(isPersisted);
        }

        private static void AddService(ref ServiceStore store)
        {
            store.AddScopedServiceToLocalStore(typeof(Services.ServiceOne), new Services.ServiceOne());
            store.AddScopedServiceToLocalStore(typeof(Services.ServiceTwo), new Services.ServiceTwo());
        }
    }
}