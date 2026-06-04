# Facade Design Pattern

## Definition

The **Facade** is a **Structural Design Pattern** that provides a **simple, unified interface** to a complex subsystem.

It hides the complexity of multiple classes and exposes a single entry point for the client.

---

## Real-World Example

### Home Theater System

To watch a movie, you need to:

```text
Turn on TV
Turn on Sound System
Turn on DVD Player
Set Input Source
Play Movie
```

Instead of calling all these steps manually, a **HomeTheaterFacade** provides:

```csharp
homeTheater.WatchMovie();
```

---

## Problem

Suppose your application has multiple services:

```csharp
paymentService.ProcessPayment();
inventoryService.UpdateStock();
shippingService.ShipOrder();
emailService.SendConfirmation();
```

The client must know and manage all subsystem classes.

---

## Solution

Create a Facade that coordinates these services.

---

## C# Example

### Subsystem Classes

```csharp
public class PaymentService
{
    public void ProcessPayment()
    {
        Console.WriteLine("Payment Processed");
    }
}

public class InventoryService
{
    public void UpdateStock()
    {
        Console.WriteLine("Inventory Updated");
    }
}

public class ShippingService
{
    public void ShipOrder()
    {
        Console.WriteLine("Order Shipped");
    }
}
```

---

### Facade

```csharp
public class OrderFacade
{
    private readonly PaymentService _paymentService;
    private readonly InventoryService _inventoryService;
    private readonly ShippingService _shippingService;

    public OrderFacade()
    {
        _paymentService = new PaymentService();
        _inventoryService = new InventoryService();
        _shippingService = new ShippingService();
    }

    public void PlaceOrder()
    {
        _paymentService.ProcessPayment();
        _inventoryService.UpdateStock();
        _shippingService.ShipOrder();

        Console.WriteLine("Order Completed");
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
        OrderFacade orderFacade = new OrderFacade();

        orderFacade.PlaceOrder();
    }
}
```

---

### Output

```text
Payment Processed
Inventory Updated
Order Shipped
Order Completed
```

---

## Class Diagram

```text
Client
   |
   v
OrderFacade
   |
   +--------------------+
   |                    |
   v                    v
PaymentService    InventoryService
                         |
                         v
                  ShippingService
```

---

## Real-World Software Examples

### ASP.NET Core

```csharp
builder.Services.AddControllers();
```

This internally configures many services but exposes a simple API.

### E-commerce

```csharp
orderFacade.PlaceOrder();
```

Internally:

* Payment Processing
* Inventory Update
* Shipping
* Email Notification

### Banking

```csharp
bankFacade.TransferMoney();
```

Internally:

* Validate Account
* Debit Account
* Credit Account
* Send Notification

---

## Advantages

✅ Simplifies complex systems

✅ Reduces client dependency on subsystem classes

✅ Improves readability

✅ Promotes loose coupling

---

## Disadvantages

❌ Facade can become a "God Class" if too much logic is added

❌ May hide useful subsystem functionality

---

## When to Use

✅ Complex subsystem with many classes

✅ Need a simple API for clients

✅ Reduce coupling between client and subsystem

Examples:

* Order Processing
* Payment Systems
* Banking Operations
* Home Theater Controls

---

## Facade vs Adapter

| Feature    | Facade                      | Adapter                                     |
| ---------- | --------------------------- | ------------------------------------------- |
| Purpose    | Simplifies a complex system | Makes incompatible interfaces work together |
| Focus      | Ease of use                 | Compatibility                               |
| Works With | Multiple classes            | Usually one class/interface                 |
| Example    | OrderFacade                 | PaymentAdapter                              |

---

## Facade vs Decorator

| Feature      | Facade                 | Decorator                |
| ------------ | ---------------------- | ------------------------ |
| Purpose      | Simplify subsystem     | Add behavior dynamically |
| Focus        | Simplicity             | Extension                |
| Relationship | Uses subsystem classes | Wraps same interface     |

---

## Interview Definition

> **Facade is a structural design pattern that provides a simplified, unified interface to a set of interfaces in a complex subsystem, making the subsystem easier to use and reducing coupling between clients and subsystem classes.**
