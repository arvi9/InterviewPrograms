# Adapter Design Pattern

## Definition

The **Adapter** is a **Structural Design Pattern** that allows two incompatible interfaces to work together by acting as a bridge between them.

It converts the interface of one class into another interface that the client expects.

---

## Real-World Example

### Mobile Charger Adapter

```text
Wall Socket (Target Interface)
          ↓
       Adapter
          ↓
Mobile Charger Plug (Adaptee)
```

The adapter allows incompatible components to work together.

---

## Problem

Suppose your application expects payment processing through:

```csharp
public interface IPaymentProcessor
{
    void Pay(decimal amount);
}
```

But a third-party payment service provides:

```csharp
public class ThirdPartyPaymentGateway
{
    public void MakePayment(decimal amount)
    {
        Console.WriteLine($"Paid {amount}");
    }
}
```

The interfaces don't match.

---

## Solution

Create an Adapter that converts one interface to another.

---

## C# Example

### Target Interface

```csharp
public interface IPaymentProcessor
{
    void Pay(decimal amount);
}
```

---

### Adaptee (Existing Class)

```csharp
public class ThirdPartyPaymentGateway
{
    public void MakePayment(decimal amount)
    {
        Console.WriteLine($"Payment of {amount} processed");
    }
}
```

---

### Adapter

```csharp
public class PaymentAdapter : IPaymentProcessor
{
    private readonly ThirdPartyPaymentGateway _gateway;

    public PaymentAdapter(ThirdPartyPaymentGateway gateway)
    {
        _gateway = gateway;
    }

    public void Pay(decimal amount)
    {
        _gateway.MakePayment(amount);
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
        IPaymentProcessor processor =
            new PaymentAdapter(
                new ThirdPartyPaymentGateway());

        processor.Pay(1000);
    }
}
```

---

### Output

```text
Payment of 1000 processed
```

---

## Class Diagram

```text
Client
   |
   v
IPaymentProcessor (Target)
   ^
   |
PaymentAdapter
   |
   v
ThirdPartyPaymentGateway (Adaptee)
```

---

## Real-World Software Examples

* Integrating third-party APIs
* Legacy system integration
* Payment gateway wrappers
* Database provider adapters
* File format converters

Examples:

* PayPal Adapter
* Stripe Adapter
* SMS Provider Adapter

---

## Advantages

✅ Allows incompatible classes to work together

✅ Reuses existing code

✅ Improves flexibility

✅ Follows Open/Closed Principle (OCP)

---

## Disadvantages

❌ Adds extra layer of abstraction

❌ Can increase complexity if overused

---

## When to Use

✅ Integrating third-party libraries

✅ Working with legacy code

✅ When interfaces don't match

✅ When modifying existing code is not possible

---

## Adapter vs Facade

| Feature       | Adapter                                 | Facade                       |
| ------------- | --------------------------------------- | ---------------------------- |
| Purpose       | Make incompatible interfaces compatible | Simplify a complex subsystem |
| Focus         | Interface conversion                    | Interface simplification     |
| Existing APIs | Different interfaces                    | Multiple complex interfaces  |
| Example       | PayPal → IPaymentProcessor              | Home Theater Controller      |

---

## Interview Definition

> **Adapter is a structural design pattern that converts the interface of an existing class into another interface expected by the client, allowing incompatible classes to work together.**
