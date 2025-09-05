#region U S A G E S

using InvokeDependencyListener;
using InvokeDependencyListener.Abstractions;
using InvokeDependencyListener.Models;

#endregion

namespace InvokeDependency.Tests
{
    public static class CoreClass
    {
        public static IInvokeDependencyContainer ServicesContainer
            => new InvokeDependencyContainer(new DependencyResolverConfig(true));
    }
}