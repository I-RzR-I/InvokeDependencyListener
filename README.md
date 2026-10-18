| Name     | Details |
|----------|----------|
| RzR.Infrastructure.DependencyResolver | [![NuGet Version](https://img.shields.io/nuget/v/RzR.Infrastructure.DependencyResolver.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/RzR.Infrastructure.DependencyResolver/) [![Nuget Downloads](https://img.shields.io/nuget/dt/RzR.Infrastructure.DependencyResolver.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/RzR.Infrastructure.DependencyResolver)|

<details>
  <summary>Old version</summary>
  
| Name     | Details |
|----------|----------|
| InvokeDependencyListener | [![NuGet Version](https://img.shields.io/nuget/v/InvokeDependencyListener.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/InvokeDependencyListener/) [![Nuget Downloads](https://img.shields.io/nuget/dt/InvokeDependencyListener.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/InvokeDependencyListener)|


</details>

<br />

This repository provides a straightforward, lightweight, minimalistic implementation of Dependency Injection (DI) that was created to address the need for avoiding redundant class instance initialization and to promote a cleaner, more maintainable codebase. In many cases, initializing new instances manually can clutter code and lead to errors, especially in larger projects. This simple DI solution is designed to help manage class dependencies in a lightweight manner.

The implementation was born out of necessity, as previous experiences showed that using or installing `Microsoft.Extensions.DependencyInjection` was not always feasible or suitable for certain projects. Given those constraints, this repository offers an alternative DI solution that doesn't require an external DI framework, making it perfect for smaller projects or situations where a more complex DI framework would be overkill or overthinked.

This is not a full-fledged DI framework, but rather a basic implementation designed to address the most common scenarios where DI can enhance the clarity and maintainability of your code. The goal is to provide just enough DI functionality to improve code organization without the overhead of a larger library.

This may be a good solution for small projects or anyone looking to learn or experiment with DI concepts without diving into complex frameworks.

To understand more efficiently how you can use available functionalities please consult the [using documentation/file](docs/usage.md).

**In case you wish to use it in your project, u can install the package from <a href="https://www.nuget.org/packages/RzR.Infrastructure.DependencyResolver" target="_blank">nuget.org</a>** or specify what version you want:

> `Install-Package RzR.Infrastructure.DependencyResolver -Version x.x.x.x`

## Migrating from InvokeDependencyListener

This package was previously published as `InvokeDependencyListener`. To migrate, remove the `InvokeDependencyListener` package, install `RzR.Infrastructure.DependencyResolver` and change your `using` directives to the `RzR.Infrastructure.DependencyResolver` namespaces. Type names, the API and the runtime behaviour are unchanged.

See the [migration guide](docs/usage.md#migrating-from-invokedependencylistener) for the full namespace map and the assembly-name notes, and the [CHANGELOG](docs/CHANGELOG.md) for the list of changes.

## Content
1. [USING](docs/usage.md)
1. [CHANGELOG](docs/CHANGELOG.md)
1. [BRANCH-GUIDE](docs/branch-guide.md)