So, how does it generally work, and how can you use the current implementation?

The `container` available methods.
```csharp
RegisterService<TImplementation>(TImplementation instance, InvokeDependencyServiceLifetime lifetime = InvokeDependencyServiceLifetime.Scoped);
RegisterService<TInterface, TImplementation>(InvokeDependencyServiceLifetime lifetime = InvokeDependencyServiceLifetime.Scoped);
RegisterFactory<T>(Func<IInvokeDependencyContainer, T> factory, InvokeDependencyServiceLifetime lifetime = InvokeDependencyServiceLifetime.Scoped);
T GetService<T>();
object GetService(Type serviceType);
T GetRequiredService<T>();
object GetRequiredService(Type serviceType);
IInvokeDependencyServiceScope CreateScope();
```

The first step is to find a place where you can store the `container` data. It can be a class that can be accessed from anywhere in the solution, like a `Core` project.

```csharp
public static class CoreClass
{
    public static IInvokeDependencyContainer ServicesContainer
        => new InvokeDependencyContainer(new DependencyResolverConfig(true));
}
```

The second step is to register services/repositories.

```csharp
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
public calss Handle
{
    public Guid GetId()
    {
        var service = CoreClass.ServicesContainer.GetService<IService>();
        var id = service.GetServiceId();
		
        return id;
    }
}
```

> One more important thing at this time is to specify that `ServicesContainer` can work with services registered in `IServiceCollection`, as at initialization, the `ServicesContainer` was activated, and the option to resolve a service even if it is not registered the its service implementation with its abstraction,  but the service is inherited from the service abstraction.

```csharp
public interface IService {}
public class Service : IService {}
```

```csharp
public class CoreClassX
{
    private IServiceProvider _serviceProvider;

    public void Init()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IServiceOne, ServiceOne>();
        serviceCollection.AddScoped<IServiceTwo, ServiceTwo>();
        serviceCollection.AddScoped<IService, Service>();
        serviceCollection = _serviceCollection.BuildServiceProvider();
    }

    public void GetServiceId()
    {
        Init();
        
        var service = CoreClass.ServicesContainer.GetService<IService>();
        var id = service.GetScopedServiceId();
        
        return id;
    }
}
```
