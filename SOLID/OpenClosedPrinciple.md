# O - Open/Closed Principle (OCP)

## Definition

**Software entities (classes, modules, functions) should be open for extension but closed for modification.**

This means:

* ✅ You should be able to add new functionality.
* ❌ You should not need to modify existing, tested code.

---

## Bad Example (Violates OCP)

Suppose we have a payment service:

```csharp
public class PaymentService
{
    public void ProcessPayment(string paymentType)
    {
        if (paymentType == "CreditCard")
        {
            Console.WriteLine("Credit Card Payment");
        }
        else if (paymentType == "PayPal")
        {
            Console.WriteLine("PayPal Payment");
        }
    }
}
```

### Problem

When a new payment method is added:

```csharp
else if (paymentType == "UPI")
{
    Console.WriteLine("UPI Payment");
}
```

You must modify existing code every time.

---

## Good Example (Follows OCP)

### Step 1: Create an Abstraction

```csharp
public interface IPaymentMethod
{
    void Pay(decimal amount);
}
```

---

### Step 2: Create Implementations

```csharp
public class CreditCardPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using Credit Card");
    }
}

public class PayPalPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using PayPal");
    }
}
```

---

### Step 3: Use the Abstraction

```csharp
public class PaymentService
{
    public void ProcessPayment(
        IPaymentMethod paymentMethod,
        decimal amount)
    {
        paymentMethod.Pay(amount);
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
        PaymentService service = new PaymentService();

        service.ProcessPayment(
            new CreditCardPayment(),
            1000);

        service.ProcessPayment(
            new PayPalPayment(),
            500);
    }
}
```

---

### Adding a New Payment Method

```csharp
public class UpiPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using UPI");
    }
}
```

No changes are needed in `PaymentService`.

---

## Real-World Example

### Bad

```text
PaymentService
    |
    +-- if CreditCard
    +-- if PayPal
    +-- if UPI
    +-- if NetBanking
```

Every new payment type requires modifying existing code.

### Good

```text
IPaymentMethod
      ^
      |
+-----+-----+-----+
|     |     |     |
CC  PayPal UPI NetBanking
```

New payment methods are added by creating new classes.

---

## ASP.NET Core Example

### Bad

```csharp
if(notificationType == "Email")
{
}
else if(notificationType == "SMS")
{
}
```

### Good

```csharp
public interface INotification
{
    void Send();
}
```

```csharp
public class EmailNotification : INotification
{
    public void Send() { }
}

public class SmsNotification : INotification
{
    public void Send() { }
}
```

Add new notification types without modifying existing code.

---

## Benefits

✅ Easier to extend

✅ Less risk of breaking existing code

✅ Better maintainability

✅ Supports plug-and-play functionality

✅ Encourages polymorphism

---

## Common Design Patterns That Support OCP

* Strategy Pattern
* Factory Method Pattern
* Abstract Factory Pattern
* Decorator Pattern
* Template Method Pattern

---

## Interview Definition

> **Open/Closed Principle (OCP) states that software entities should be open for extension but closed for modification, meaning new functionality should be added through extensions rather than changing existing code.**

---

## Easy Memory Trick

**O = Open for Extension, Closed for Modification**

Ask yourself:

> **"Can I add a new feature without changing existing code?"**

If **Yes**, the code follows OCP.

### Example

❌ Bad:

```csharp
if(type == "Email")
else if(type == "SMS")
else if(type == "Push")
```

Every new type requires modifying existing code.

✅ Good:

```csharp
INotification
    ↓
EmailNotification
SmsNotification
PushNotification
```

Add a new implementation without changing existing classes.

**Extend behavior, don't modify working code.**
