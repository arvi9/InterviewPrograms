# Decorator Design Pattern

## Definition

The **Decorator** is a **Structural Design Pattern** that allows you to **add new behavior to an object dynamically** without modifying its existing code.

It wraps an object and adds extra functionality before or after calling the original object's behavior.

---

## Real-World Example

### Coffee Shop

```text
Simple Coffee
      ↓
Milk Decorator
      ↓
Sugar Decorator
      ↓
Whipped Cream Decorator
```

Each decorator adds extra features and cost without changing the original coffee class.

---

## Problem

Suppose you have a notification service:

```csharp
notification.Send("Hello");
```

Later, you need:

* Email Notification
* SMS Notification
* Logging
* Encryption

Creating separate classes for every combination becomes difficult.

---

## Solution

Use decorators to add functionality dynamically.

---

## C# Example

### Component Interface

```csharp
public interface INotification
{
    void Send(string message);
}
```

---

### Concrete Component

```csharp
public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"Email Sent: {message}");
    }
}
```

---

### Base Decorator

```csharp
public abstract class NotificationDecorator : INotification
{
    protected readonly INotification _notification;

    protected NotificationDecorator(INotification notification)
    {
        _notification = notification;
    }

    public virtual void Send(string message)
    {
        _notification.Send(message);
    }
}
```

---

### Logging Decorator

```csharp
public class LoggingDecorator : NotificationDecorator
{
    public LoggingDecorator(INotification notification)
        : base(notification)
    {
    }

    public override void Send(string message)
    {
        Console.WriteLine("Logging Message...");
        base.Send(message);
    }
}
```

---

### Encryption Decorator

```csharp
public class EncryptionDecorator : NotificationDecorator
{
    public EncryptionDecorator(INotification notification)
        : base(notification)
    {
    }

    public override void Send(string message)
    {
        string encrypted = $"[Encrypted] {message}";
        base.Send(encrypted);
    }
}
```

---

### Client Code

```csharp
class Program
{
    static void Main()
    {
        INotification notification =
            new LoggingDecorator(
                new EncryptionDecorator(
                    new EmailNotification()));

        notification.Send("Hello World");
    }
}
```

---

### Output

```text
Logging Message...
Email Sent: [Encrypted] Hello World
```

---

## Class Diagram

```text
Client
   |
   v
INotification
   ^
   |
EmailNotification

INotification
   ^
   |
NotificationDecorator
   ^
   |
+-------------------+
|                   |
LoggingDecorator  EncryptionDecorator
```

---

## Real-World Software Examples

* ASP.NET Core Middleware Pipeline
* StreamReader / StreamWriter decorators
* Logging wrappers
* Caching wrappers
* Compression wrappers

Example:

```csharp
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCompression();
```

Each middleware decorates the request pipeline with additional behavior.

---

## Advantages

✅ Add functionality without modifying existing code

✅ Follows Open/Closed Principle (OCP)

✅ Flexible and reusable

✅ Combine behaviors dynamically

---

## Disadvantages

❌ Many small classes can be created

❌ Can make debugging more difficult

---

## When to Use

✅ Add responsibilities dynamically

✅ Avoid large inheritance hierarchies

✅ Extend functionality without changing existing code

Examples:

* Logging
* Caching
* Compression
* Encryption
* Validation

---

## Decorator vs Inheritance

| Feature               | Decorator    | Inheritance    |
| --------------------- | ------------ | -------------- |
| Add behavior          | Runtime      | Compile time   |
| Flexibility           | High         | Low            |
| Multiple combinations | Easy         | Difficult      |
| Code modification     | Not required | Often required |

---

## Interview Definition

> **Decorator is a structural design pattern that dynamically adds new behavior or responsibilities to an object by wrapping it inside another object that implements the same interface.**
