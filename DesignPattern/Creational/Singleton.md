# Singleton Design Pattern

## Definition

The **Singleton** is a **Creational Design Pattern** that ensures **only one instance** of a class exists throughout the application's lifetime and provides a global point of access to that instance.

---

## Problem

Suppose multiple parts of an application create separate instances of a logger:

```csharp
Logger logger1 = new Logger();
Logger logger2 = new Logger();
```

This can lead to unnecessary memory usage and inconsistent behavior.

---

## Solution

Make the constructor private and provide a single shared instance.

---

## C# Singleton Example

```csharp
public sealed class Logger
{
    private static readonly Logger _instance = new Logger();

    private Logger()
    {
    }

    public static Logger Instance
    {
        get { return _instance; }
    }

    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}
```

### Client Code

```csharp
class Program
{
    static void Main()
    {
        Logger logger1 = Logger.Instance;
        Logger logger2 = Logger.Instance;

        logger1.Log("Application Started");

        Console.WriteLine(
            ReferenceEquals(logger1, logger2));
    }
}
```

### Output

```text
Application Started
True
```

Both variables point to the same object.

---

## Lazy Singleton (Recommended)

The instance is created only when it is first needed.

```csharp
public sealed class Logger
{
    private static readonly Lazy<Logger> _instance =
        new Lazy<Logger>(() => new Logger());

    private Logger()
    {
    }

    public static Logger Instance
    {
        get { return _instance.Value; }
    }

    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}
```

---

## Real-World Examples

* Application Configuration Manager
* Logger Service
* Cache Manager
* Database Connection Manager
* Application Settings

---

## Advantages

✅ Ensures only one instance exists

✅ Saves memory

✅ Provides global access point

✅ Useful for shared resources

---

## Disadvantages

❌ Global state can make testing difficult

❌ Can create tight coupling

❌ Violates Single Responsibility Principle in some cases

❌ Can become an anti-pattern if overused

---

## When to Use

✅ Only one instance should exist

✅ Shared resource across the application

✅ Centralized configuration or logging

Examples:

* Logger
* Cache
* Configuration Service

---

## When Not to Use

❌ If multiple instances may be needed later

❌ If dependency injection can manage the lifetime

❌ In modern ASP.NET Core applications, prefer registering services as Singleton in the DI container instead of implementing the Singleton pattern manually

Example:

```csharp
builder.Services.AddSingleton<ILoggerService, LoggerService>();
```

---

## Interview Definition

> **Singleton is a creational design pattern that ensures a class has only one instance and provides a global point of access to that instance.**

---

## Creational Patterns Summary

| Pattern          | Purpose                                           |
| ---------------- | ------------------------------------------------- |
| Singleton        | Ensure only one instance exists                   |
| Factory Method   | Create one object without exposing creation logic |
| Abstract Factory | Create families of related objects                |
| Builder          | Build complex objects step by step                |

### Easy Memory Trick

* **Singleton** → One instance
* **Factory Method** → One product
* **Abstract Factory** → Family of products
* **Builder** → Step-by-step construction of a product
