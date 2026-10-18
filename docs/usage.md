So, how does it generally work, and how can you use the current implementation?

The `container` available methods. This is a member list of `IInvokeDependencyContainer` (namespace `RzR.Infrastructure.DependencyResolver.Abstractions`), not compilable code.
```text
void RegisterService<TImplementation>(TImplementation instance, InvokeDependencyServiceLifetime lifetime = InvokeDependencyServiceLifetime.Scoped) where TImplementation : class;
void RegisterService<TInterface, TImplementation>(InvokeDependencyServiceLifetime lifetime = InvokeDependencyServiceLifetime.Scoped) where TImplementation : class, TInterface;
void RegisterFactory<T>(Func<IInvokeDependencyContainer, T> factory, InvokeDependencyServiceLifetime lifetime = InvokeDependencyServiceLifetime.Scoped);
T GetService<T>();
object GetService(Type serviceType);
T GetRequiredService<T>();
object GetRequiredService(Type serviceType);
IInvokeDependencyServiceScope CreateScope();
```

The first step is to find a place where you can store the `container` data. It can be a class that can be accessed from anywhere in the solution, like a `Core` project.

```csharp
using RzR.Infrastructure.DependencyResolver;
using RzR.Infrastructure.DependencyResolver.Abstractions;
using RzR.Infrastructure.DependencyResolver.Models;

public static class CoreClass
{
    // One container instance for the whole application.
    public static IInvokeDependencyContainer ServicesContainer { get; }
        = new InvokeDependencyContainer(new DependencyResolverConfig(true));
}
```

The second step is to register services/repositories.

```csharp
// CoreClass, IService and Service are declared in the other samples on this page.
// IServiceOne/ServiceOne and IServiceTwo/ServiceTwo stand for your own services.
public static class CoreServiceExtensions
{
    public static void RegisterCoreServices()
    {
        CoreClass.ServicesContainer.RegisterService<IServiceOne, ServiceOne>();
        CoreClass.ServicesContainer.RegisterService<IServiceTwo, ServiceTwo>();
        CoreClass.ServicesContainer.RegisterService<IService, Service>();
    }
}
```

The next step consist in using the `injected` services
```csharp
using System;

// CoreClass and IService are declared in the other samples on this page.
public class Handle
{
    public Guid GetId()
    {
        var service = CoreClass.ServicesContainer.GetService<IService>();
        var id = service.GetServiceId();
		
        return id;
    }
}
```

> One more important thing: `ServicesContainer` was created with `new DependencyResolverConfig(true)`, which turns on resolve-by-interface. With this option the container can return a service even if you never registered it, as long as a public class implements the requested abstraction. It does not read registrations from `IServiceCollection`; the library does not use `IServiceCollection` or `IServiceProvider` at all.

How resolve-by-interface works:

- It applies only when the requested type is not registered and the container was created with `new DependencyResolverConfig(true)`. With `new DependencyResolverConfig()` or `null`, an unregistered type returns `null`.
- When you request an interface, the container scans the assemblies currently loaded in `AppDomain.CurrentDomain.GetAssemblies()` and uses the first public, top-level (not nested), non-abstract, non-interface type assignable to it. If several types qualify, which one wins is undefined.
- When you request a concrete class, the container creates it directly if it is public, top-level (not nested) and non-abstract.
- Constructor parameters are resolved through the same lookup, recursively.
- The resolved instance is not stored, so each call returns a new one. Constructor parameters that are registered keep their registered lifetime.
- If no type matches, `GetService` returns `null` and `GetRequiredService` throws `InvalidOperationException`. If a type matches but one of its constructor parameters cannot be resolved, both throw `InvalidOperationException`; if it has no public constructor, both throw `NoPublicConstructorException`.
- Scanning has a performance cost. Register services explicitly when you can.

> Security: with resolve-by-interface on, any loaded assembly, including a plugin or a transitive dependency, can supply the implementation of an unregistered interface. For security-sensitive abstractions, or in hosts that load plugins, register those services and their dependencies explicitly, or create the container with `new DependencyResolverConfig()` (the default, resolve-by-interface off).

The `IService` and `Service` types used on this page:

```csharp
using System;

public interface IService
{
    Guid GetServiceId();
}

public class Service : IService
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid GetServiceId() => _id;
}
```

The example below uses a separate container with no registrations. `IService` resolves only because `Service` is public and loaded in the AppDomain.

```csharp
using System;
using RzR.Infrastructure.DependencyResolver;
using RzR.Infrastructure.DependencyResolver.Abstractions;
using RzR.Infrastructure.DependencyResolver.Models;

// IService and Service are declared in the sample above.
public class CoreClassX
{
    // Resolve-by-interface is on and nothing is registered.
    private readonly IInvokeDependencyContainer _container =
        new InvokeDependencyContainer(new DependencyResolverConfig(true));

    public Guid GetServiceId()
    {
        // IService is not registered, so the container finds Service and creates a new instance.
        var service = _container.GetService<IService>();
        var id = service.GetServiceId();

        return id;
    }
}
```

## Migrating from InvokeDependencyListener

The package `InvokeDependencyListener` is now published as [`RzR.Infrastructure.DependencyResolver`](https://www.nuget.org/packages/RzR.Infrastructure.DependencyResolver). Type names, the public API and the runtime behaviour are unchanged. For most projects only the package reference and the `using` directives change; steps 3 and 4 cover the exceptions. The old `InvokeDependencyListener` package is still available on nuget.org. NuGet does not redirect the old id, so switch manually.

### 1. Replace the package

Remove the old package and install the new one.

```powershell
Uninstall-Package InvokeDependencyListener
Install-Package RzR.Infrastructure.DependencyResolver
```

With the .NET CLI:

```shell
dotnet remove package InvokeDependencyListener
dotnet add package RzR.Infrastructure.DependencyResolver
```

### 2. Update the namespaces

Change every `using` directive and fully qualified type name according to this map.

| Old namespace | New namespace | Public types |
| --- | --- | --- |
| `InvokeDependencyListener` | `RzR.Infrastructure.DependencyResolver` | `InvokeDependencyContainer`, `InvokeDependencyServiceScope` |
| `InvokeDependencyListener.Abstractions` | `RzR.Infrastructure.DependencyResolver.Abstractions` | `IInvokeDependencyContainer`, `IInvokeDependencyServiceScope` |
| `InvokeDependencyListener.Models` | `RzR.Infrastructure.DependencyResolver.Models` | `DependencyResolverConfig` |
| `InvokeDependencyListener.Enums` | `RzR.Infrastructure.DependencyResolver.Enums` | `InvokeDependencyServiceLifetime` |
| `InvokeDependencyListener.Exceptions` | `RzR.Infrastructure.DependencyResolver.Exceptions` | `CyclicDependencyException`, `RequiredServiceNotRegisteredException`, `UnableServiceResolveException`, `NoPublicConstructorException` |

### 3. Check the transitive dependency

The package now depends on `RzR.Extensions.Domain` 7.1.0.5494 instead of `DomainCommonExtensions`. If your own code uses `DomainCommonExtensions` types that came in only through this package, your build fails after the switch. Add a direct reference to `DomainCommonExtensions` 3.2.0.4113, the version the old package used (published by RzR: [nuget.org/packages/DomainCommonExtensions/3.2.0.4113](https://www.nuget.org/packages/DomainCommonExtensions/3.2.0.4113)), or move that code to `RzR.Extensions.Domain`.

### 4. Check the assembly name

The assembly is now `RzR.Infrastructure.DependencyResolver.dll` instead of `InvokeDependencyListener.dll`. Code that only uses the types through `using` directives needs no further change. Update these places:

- Assembly-qualified type names, for example `InvokeDependencyListener.InvokeDependencyContainer, InvokeDependencyListener` becomes `RzR.Infrastructure.DependencyResolver.InvokeDependencyContainer, RzR.Infrastructure.DependencyResolver`.
- Reflection and configuration strings that name the old assembly or namespaces.
- On .NET Framework (net40/net45) applications, remove any `<dependentAssembly>` entry for `InvokeDependencyListener` from `app.config` or `web.config`. A binding redirect only remaps versions of the same assembly, so it cannot point at the renamed assembly.

See the [CHANGELOG](CHANGELOG.md) for the full list of changes.
