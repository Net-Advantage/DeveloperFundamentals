# Dependency Inversion in .NET

Outline:

1. Introduction
1. Understanding Dependency Inversion or Inversion of Control (IoC)
1. Types of Dependency Inversion
1. Lifetime of an object
1. Setting up the `IHostBuilder` for hosting the application
1. Implement the `IOptions` pattern with Dependency Injection

Script:

## Introduction

Welcome to our video presentation on Dependency Inversion in .NET 7.0. In this presentation, we will cover the importance of dependency inversion, what it is, the different types, and the lifetime of an object. Using a console application, we will also show how to set up the `ServiceCollection` to register your services, and how to use the `ServiceProvider` to resolve dependencies. We will also build on your knowledge of the Options pattern which we introduced in the `Configuration in .Net` lesson.

## Understanding Dependency Inversion or Inversion of Control (IoC)

Inversion of Control (IoC) is a software design principle that aims to increase the modularity, flexibility, and testability of a system by inverting the traditional control flow of a program. It is a key concept in object-oriented programming and is often used in conjunction with dependency injection and the use of frameworks.

Every program has a starting point from which the rest of the application flows. In .NET, this is the `Main` method. The `Main` method serves as the entry point of the application and initiates the program. It is responsible for creating the objects needed to run the application and calling the necessary methods. This is called tight coupling, as the `Main` method is tightly coupled to the objects it creates and the methods it calls. This design is not ideal because it makes the application inflexible and difficult to test.

We can `invert` the control flow by using a framework, called a `container`, to create the objects and call the methods. This is why we use the term `Inversion of Control`. The framework is responsible for creating the objects and calling the methods, and is loosely coupled to the objects and methods that it creates and calls. This design is more desirable because it makes the application flexible and easier to test.

Inversion of control is essential for developer productivity. By decoupling the system, multiple individuals and teams can independently work on various parts of the system. By separating the system into discrete concerns, work can be parallelized or assigned to people with different sets of domain knowledge to apply their individual expertise to the system. This approach is often referred to as `separation of concerns`.

## Types of Dependency Inversion

There are the three types of dependency inversion techniques used in software design to achieve Inversion of Control (IoC). These techniques help in creating modular, flexible, and testable applications.

1. Dependency Injection:
Dependency Injection is a technique where an object's dependencies are provided by an external entity instead of the object itself creating them. There are two common forms of Dependency Injection:

	a. Constructor Injection:
	In Constructor Injection, dependencies are provided to an object via its constructor. When the object is instantiated, the required dependencies are passed as arguments to the constructor. This approach ensures that the object has all its dependencies before it starts executing.

	b. Property Injection:
	Property Injection, also known as Setter Injection, involves providing dependencies to an object through setter methods or properties. Unlike Constructor Injection, this approach allows for the possibility of changing dependencies after the object has been instantiated.

2. Service Locator:
The Service Locator pattern involves using a centralized registry to manage the creation and retrieval of service instances. Components can request a specific service from the service locator, which handles the creation and management of the service instance. This approach provides a level of indirection, making it easier to swap or modify components without affecting the rest of the application.

3. Event Driven:
Event Driven programming is a paradigm where the flow of control is determined by the occurrence of specific events, such as user actions or changes in the application state. Components can subscribe to and react to these events, without needing to be explicitly aware of the control flow. This approach promotes loose coupling between components, making it easier to develop and maintain applications.

In summary, Dependency Injection, Service Locator, and Event Driven programming are three types of dependency inversion techniques used to achieve Inversion of Control in software design. By using these techniques, developers can create more modular, flexible, and testable applications.

Note:

In .NET, the most common technique used for Dependency Injection is Constructor Injection. This technique involves providing dependencies to an object via its constructor. When the object is instantiated, the required dependencies are passed as arguments to the constructor. This approach ensures that the object has all its dependencies before it starts executing.

While the Service Locator pattern is a valid means for resolving dependencies, it is not recommended for use in .NET applications. The Service Locator pattern is considered an anti-pattern because it introduces a level of indirection that can make it difficult to understand and maintain the code. Instead, it is recommended to use Constructor Injection to provide dependencies to an object.

## Lifetime of an object

Let's discuss the lifetime of an object as it pertains to Dependency Injection (DI). Understanding object lifetimes is crucial when working with DI, as it helps to manage resources effectively and ensures that the application behaves as expected.

When using Dependency Injection, objects are instantiated and managed by a container or an external entity. The container is responsible for creating, injecting, and disposing of objects as required. There are typically three types of lifetimes associated with objects in a DI container:

1. Transient:
Transient lifetime means that a new instance of the object is created each time it is requested from the DI container. This is useful when the object has a short lifespan or is stateless, ensuring that each consumer receives a brand-new instance, thus avoiding any shared state or side effects. Transient objects are typically lightweight and do not hold expensive resources.

2. Scoped:
Scoped lifetime indicates that an object is created once per scope, usually tied to a specific context, such as a single web request or a unit of work. Scoped objects are shared within that scope but are not shared across different scopes. This is useful when the object needs to maintain state or resources within a specific context. When the scope ends, the DI container disposes of the scoped objects, releasing any associated resources.

3. Singleton:
Singleton lifetime means that only one instance of the object is created and shared across the entire application for the duration of the application's lifetime. Singleton objects are useful when the object needs to maintain a global state or manage shared resources, such as a configuration object or a database connection pool. Since the singleton object persists for the entire application lifetime, it is essential to ensure that it is thread-safe and does not hold resources unnecessarily.

In summary, understanding the lifetime of objects in Dependency Injection is crucial for effective resource management and application behavior. By choosing the appropriate lifetime for an object - Transient, Scoped, or Singleton - developers can optimize performance, manage resources, and avoid unexpected issues related to shared state or resource contention.

## `HostBuilder` for creating a hostable application

The `HostBuilder` is the entry point for setting up a host. The host is responsible for app startup, object creation, and lifetime management. It creates the `IServiceProvider`, which is used to resolve dependencies, and the `IConfiguration`, responsible for managing your application's settings.

Before using the `HostBuilder`, you need to add the `Microsoft.Extensions.Hosting` NuGet package to your project. The package will add a number of packages that are required to host the default application.

You also need to add a file called `TheHostService.cs` with a class that implements the `IHostedService` interface. This interface is used to define the behavior of a service that is hosted by the `HostBuilder`. The `HostBuilder` will call the `StartAsync` method when the host starts, and the `StopAsync` method when the host stops. The `HostBuilder` will also call the `DisposeAsync` method when the host is disposed.

```csharp
using Microsoft.Extensions.Hosting;

namespace DependencyInversionFundamentals;

public class TheHostService : IHostedService
{
	public async Task StartAsync(CancellationToken cancellationToken)
	{
		Console.WriteLine("The host is starting up!");
		await Task.CompletedTask;
	}

	public async Task StopAsync(CancellationToken cancellationToken)
	{
		Console.WriteLine("The host is shutting down!");
		await Task.CompletedTask;
	}
}
```

Now, you can use the `HostBuilder` to create a host. Add this code to the `Program.cs` file:

```csharp
using DependencyInversionFundamentals;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("The host being set up!");

var hostBuilder = Host.CreateApplicationBuilder();
hostBuilder.Services.AddHostedService<TheHostService>();

var host = hostBuilder.Build();

Console.WriteLine("The host is starting up!");
await host.RunAsync();
Console.WriteLine("The application has ended!");
```

The `HostBuilder` is used to configure the host and its services. The `HostBuilder` is created by calling the `CreateDefaultBuilder` method. This method will create a host with default configuration. The `ConfigureServices` method is used to register services with the host's `IServiceCollection`. The `Build` method is used to create the host.

## Implement the `IOptions` pattern with Dependency Injection

