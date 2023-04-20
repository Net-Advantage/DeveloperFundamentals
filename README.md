# DeveloperFundamentals
Developer Fundamentals are what I consider a minimum set of have-to-know aspects of .Net and C# every developer should know and understand.

# In order

- Configuration Fundamentals
- Dependency Injection Fundamentals
- Logging Fundamentals
- Exception Handling Fundamentals
- xUnit Testing Fundamentals
- Integration Testing Fundamentals
- Web API Fundamentals
- Web Application Fundamentals
- Blazor WebAssembly Fundamentals

# dotnet CLI

## Create a solution using the dotnet CLI
```powershell
dotnet new sln -n DeveloperFundamentals
```

## Create a project using the dotnet CLI

### Console application
```powershell
dotnet new console -n DeveloperFundamentals.ConsoleApp
```

### Class library
```powershell
dotnet new classlib -n DeveloperFundamentals.ClassLibrary
```

### xUnit test project
```powershell
dotnet new xunit -n DeveloperFundamentals.XUnitTests
```

### ASP.NET Core Web API
```powershell
dotnet new webapi -n DeveloperFundamentals.WebApi
```

### ASP.NET Core Web Application
```powershell
dotnet new webapp -n DeveloperFundamentals.WebApp
```

### Blazor WebAssembly application
```powershell
dotnet new blazorwasm -n DeveloperFundamentals.BlazorWasm
```
