#region U S A G E S

using RzR.Infrastructure.DependencyResolver;
using RzR.Infrastructure.DependencyResolver.Abstractions;
using RzR.Infrastructure.DependencyResolver.Models;

#endregion

namespace InvokeDependency.Tests
{
    public static class CoreClass
    {
        public static IInvokeDependencyContainer ServicesContainer
            => new InvokeDependencyContainer(new DependencyResolverConfig(true));
    }
}