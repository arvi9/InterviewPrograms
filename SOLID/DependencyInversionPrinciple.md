# D - Dependency Inversion Principle (DIP)

## Definition

**High-level modules should not depend on low-level modules. Both should depend on abstractions.**

**Abstractions should not depend on details. Details should depend on abstractions.**

### Simple Meaning

> **Depend on Interfaces, not Concrete Classes.**

---

## Bad Example (Violates DIP)

### Low-Level Module

```csharp
public class EmailService
{
    public void Send(string message)
    {
        Console.WriteLine($"Email Sent: {message}");
    }
}
```

### High-Level Module

```csharp
public class NotificationManager
{
    private readonly EmailService _emailService;

    public NotificationManager()
    {
        _emailService = new EmailService();
    }

    public void Notify(string message)
    {
        _emailService.Send(message);
    }
}
```

### Problem

`NotificationManager` is tightly coupled to `EmailService`.

If tomorrow you want:

* SMS
* Push Notification
* WhatsApp Notification

You must modify `NotificationManager`.

---

## Good Example (Follows DIP)

### Step 1: Create an Abstraction

```csharp
public interface INotificationService
{
    void Send(string message);
}
```

---

### Step 2: Implement the Interface

```csharp
public class EmailService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"Email Sent: {message}");
    }
}
```

```csharp
public class SmsService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS Sent: {message}");
    }
}
```

---

### Step 3: Depend on Abstraction

```csharp
public class NotificationManager
{
    private readonly INotificationService _notificationService;

    public NotificationManager(
        INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public void Notify(string message)
    {
        _notificationService.Send(message);
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
        INotificationService service =
            new EmailService();

        NotificationManager manager =
            new NotificationManager(service);

        manager.Notify("Order Created");
    }
}
```

---

## Real-World Example

### Bad

```text
NotificationManager
        |
        v
   EmailService
```

High-level module depends directly on low-level module.

---

### Good

```text
NotificationManager
        |
        v
INotificationService
      ^
      |
+-----+------+
|            |
EmailService SmsService
```

Both depend on the abstraction.

---

## ASP.NET Core Example

### Dependency Injection

#### Register Service

```csharp
builder.Services.AddScoped<
    INotificationService,
    EmailService>();
```

#### Consume Service

```csharp
public class OrderService
{
    private readonly INotificationService _notificationService;

    public OrderService(
        INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
}
```

The service depends on an interface, not a concrete implementation.

---

## Benefits

✅ Loose coupling

✅ Easier unit testing

✅ Easier to replace implementations

✅ Better maintainability

✅ Supports Dependency Injection (DI)

---

## Interview Definition

> **Dependency Inversion Principle (DIP) states that high-level modules should not depend on low-level modules. Both should depend on abstractions, and abstractions should not depend on details; details should depend on abstractions.**

---

## Easy Memory Trick

**D = Depend on Abstractions**

Ask yourself:

> **"Am I depending on a concrete class or an interface?"**

❌ Bad

```csharp
private EmailService _emailService;
```

Direct dependency on a concrete class.

✅ Good

```csharp
private INotificationService _notificationService;
```

Dependency on an abstraction.

### Key Rule

**Depend on Interfaces, Not Implementations.**

### SOLID Summary

| Principle | Meaning                                                                               |
| --------- | ------------------------------------------------------------------------------------- |
| **S**     | Single Responsibility Principle – One class, one responsibility                       |
| **O**     | Open/Closed Principle – Open for extension, closed for modification                   |
| **L**     | Liskov Substitution Principle – Child should replace parent without breaking behavior |
| **I**     | Interface Segregation Principle – Small, focused interfaces                           |
| **D**     | Dependency Inversion Principle – Depend on abstractions, not concrete classes         |
