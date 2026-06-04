# Observer Design Pattern

## Definition

The **Observer** is a **Behavioral Design Pattern** that defines a **one-to-many dependency** between objects. When one object (Subject) changes its state, all dependent objects (Observers) are automatically notified and updated.

---

## Real-World Example

### YouTube Channel Subscription

```text
YouTube Channel (Subject)
         |
         +---- Subscriber 1 (Observer)
         +---- Subscriber 2 (Observer)
         +---- Subscriber 3 (Observer)
```

When a new video is uploaded, all subscribers receive a notification.

---

## Problem

Suppose multiple systems need to know when an order is placed:

* Email Service
* SMS Service
* Inventory Service

Without Observer:

```csharp
orderService.PlaceOrder();

emailService.SendEmail();
smsService.SendSms();
inventoryService.UpdateStock();
```

The order service becomes tightly coupled to all other services.

---

## Solution

Use the Observer pattern so observers automatically receive notifications.

---

## C# Example

### Observer Interface

```csharp
public interface IObserver
{
    void Update(string message);
}
```

---

### Concrete Observers

```csharp
public class EmailNotifier : IObserver
{
    public void Update(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}

public class SmsNotifier : IObserver
{
    public void Update(string message)
    {
        Console.WriteLine($"SMS: {message}");
    }
}
```

---

### Subject

```csharp
public class OrderService
{
    private readonly List<IObserver> _observers =
        new List<IObserver>();

    public void Subscribe(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Unsubscribe(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify(string message)
    {
        foreach (var observer in _observers)
        {
            observer.Update(message);
        }
    }

    public void PlaceOrder()
    {
        Console.WriteLine("Order Placed");

        Notify("New Order Created");
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
        OrderService orderService = new OrderService();

        orderService.Subscribe(new EmailNotifier());
        orderService.Subscribe(new SmsNotifier());

        orderService.PlaceOrder();
    }
}
```

---

### Output

```text
Order Placed
Email: New Order Created
SMS: New Order Created
```

---

## Class Diagram

```text
         IObserver
             ^
             |
   +---------+---------+
   |                   |
EmailNotifier    SmsNotifier

             ^
             |
        OrderService
         (Subject)
```

---

## Real-World Software Examples

### ASP.NET Core Events

```csharp
eventPublisher.Publish(orderCreatedEvent);
```

Multiple handlers receive the event.

### Message Brokers

* RabbitMQ
* Kafka
* Azure Service Bus

```text
Publisher
    |
    v
 Message Broker
    |
    +---- Consumer 1
    +---- Consumer 2
```

### UI Events

```csharp
button.Click += Button_Click;
```

The button is the Subject, event handlers are Observers.

---

## Advantages

✅ Loose coupling between subject and observers

✅ Easy to add new observers

✅ Follows Open/Closed Principle (OCP)

✅ Supports event-driven architecture

---

## Disadvantages

❌ Too many observers can impact performance

❌ Notification order is not always guaranteed

❌ Debugging can be harder

---

## When to Use

✅ Multiple objects need updates when state changes

✅ Event-driven systems

✅ Publish/Subscribe scenarios

Examples:

* Notifications
* Order Processing
* Stock Price Updates
* Chat Applications
* UI Events

---

## Observer vs Strategy

| Feature      | Observer                | Strategy            |
| ------------ | ----------------------- | ------------------- |
| Purpose      | Notify multiple objects | Choose an algorithm |
| Relationship | One-to-many             | One-to-one          |
| Category     | Behavioral              | Behavioral          |
| Example      | Email/SMS notifications | Payment methods     |

---

## Observer vs Mediator

| Feature       | Observer                 | Mediator                 |
| ------------- | ------------------------ | ------------------------ |
| Communication | Subject → Many Observers | Through Central Mediator |
| Coupling      | Loose                    | Loose                    |
| Focus         | Notifications            | Coordination             |

---

## Interview Definition

> **Observer is a behavioral design pattern that establishes a one-to-many relationship between objects so that when the subject changes state, all registered observers are automatically notified and updated.**

### Easy Memory Trick

* **Observer = Publish → Subscribe**
* One publisher, many subscribers.
* When something changes, everyone gets notified.

**Examples:**

* YouTube subscribers get notified of new videos.
* Email/SMS notifications after an order is placed.
* RabbitMQ/Kafka consumers receive published messages.

**Question to ask yourself:**
*"Do multiple objects need to be notified when one object changes?"*

If **Yes → Use Observer Pattern**.
