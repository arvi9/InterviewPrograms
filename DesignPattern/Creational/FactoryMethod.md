# Factory Method Design Pattern

## Definition

The **Factory Method** is a **Creational Design Pattern** that provides an interface for creating objects, but allows subclasses or factory classes to decide which specific object to create.

Instead of creating objects using `new` directly, object creation is delegated to a factory method.

---

## Problem

Suppose you have different notification types:

```csharp
EmailNotification email = new EmailNotification();
```

Later, you need SMS and Push notifications:

```csharp
if(type == "Email")
    notification = new EmailNotification();
else if(type == "SMS")
    notification = new SMSNotification();
else if(type == "Push")
    notification = new PushNotification();
```

As new notification types are added, this code becomes difficult to maintain.

---

## Solution

Create a factory that handles object creation.

```csharp
Notification notification = NotificationFactory.CreateNotification("Email");
```

The client doesn't need to know which concrete class is instantiated.

---

## Real-World Example

* Payment Gateway (Credit Card, PayPal, UPI)
* Notifications (Email, SMS, Push)
* Database Connections (SQL Server, MySQL, PostgreSQL)
* Document Generators (PDF, Excel, Word)

---

## C# Example

### Product Interface

```csharp
public interface INotification
{
    void Send();
}
```

### Concrete Products

```csharp
public class EmailNotification : INotification
{
    public void Send()
    {
        Console.WriteLine("Sending Email");
    }
}

public class SmsNotification : INotification
{
    public void Send()
    {
        Console.WriteLine("Sending SMS");
    }
}
```

### Factory Class

```csharp
public class NotificationFactory
{
    public static INotification CreateNotification(string type)
    {
        switch (type)
        {
            case "Email":
                return new EmailNotification();

            case "SMS":
                return new SmsNotification();

            default:
                throw new ArgumentException("Invalid notification type");
        }
    }
}
```

### Client Code

```csharp
class Program
{
    static void Main()
    {
        INotification notification =
            NotificationFactory.CreateNotification("Email");

        notification.Send();
    }
}
```

### Output

```text
Sending Email
```

---

## Advantages

* Encapsulates object creation logic.
* Reduces tight coupling.
* Easier to add new object types.
* Supports Open/Closed Principle (OCP).

---

## Disadvantages

* Adds extra classes.
* Can increase complexity for simple applications.

---

## When to Use

✅ When object creation logic is complex.

✅ When the exact type of object is decided at runtime.

✅ When you want to reduce dependency on concrete classes.

❌ Avoid when object creation is simple and unlikely to change.

---

## Interview Definition

> **Factory Method is a creational design pattern that defines an interface for creating objects while allowing subclasses or factory classes to decide which concrete object to instantiate. It encapsulates object creation and reduces coupling between client code and concrete classes.**
