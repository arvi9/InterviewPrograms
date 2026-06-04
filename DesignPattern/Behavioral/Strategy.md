# Strategy Design Pattern

## Definition

The **Strategy** is a **Behavioral Design Pattern** that defines a family of algorithms, encapsulates each one, and makes them interchangeable at runtime.

It allows you to change an object's behavior without modifying its code.

---

## Problem

Suppose an e-commerce application supports multiple payment methods:

```csharp
if (paymentType == "CreditCard")
{
    // Credit Card Payment Logic
}
else if (paymentType == "PayPal")
{
    // PayPal Payment Logic
}
else if (paymentType == "UPI")
{
    // UPI Payment Logic
}
```

As new payment methods are added, the code becomes harder to maintain.

---

## Solution

Move each payment algorithm into its own strategy class.

---

## C# Example

### Strategy Interface

```csharp
public interface IPaymentStrategy
{
    void Pay(decimal amount);
}
```

---

### Concrete Strategies

```csharp
public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using Credit Card");
    }
}

public class PayPalPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using PayPal");
    }
}

public class UpiPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using UPI");
    }
}
```

---

### Context Class

```csharp
public class PaymentContext
{
    private IPaymentStrategy _paymentStrategy;

    public PaymentContext(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void SetStrategy(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void ProcessPayment(decimal amount)
    {
        _paymentStrategy.Pay(amount);
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
        PaymentContext context =
            new PaymentContext(new CreditCardPayment());

        context.ProcessPayment(1000);

        context.SetStrategy(new UpiPayment());

        context.ProcessPayment(500);
    }
}
```

---

### Output

```text
Paid 1000 using Credit Card
Paid 500 using UPI
```

---

## Class Diagram

```text
Client
   |
   v
PaymentContext
   |
   v
IPaymentStrategy
   ^
   |
+--------------------+
|         |          |
CreditCard PayPal   UPI
```

---

## Real-World Examples

### Payment Processing

```csharp
context.SetStrategy(new PayPalPayment());
context.ProcessPayment(1000);
```

### Sorting Algorithms

```csharp
context.SetStrategy(new QuickSort());
context.SetStrategy(new MergeSort());
```

### Discount Calculation

```csharp
context.SetStrategy(new PremiumCustomerDiscount());
context.SetStrategy(new RegularCustomerDiscount());
```

### Shipping Cost Calculation

```csharp
context.SetStrategy(new AirShipping());
context.SetStrategy(new SeaShipping());
```

---

## Advantages

✅ Eliminates large `if-else` or `switch` statements

✅ Follows Open/Closed Principle (OCP)

✅ Easy to add new algorithms

✅ Algorithms can be changed at runtime

✅ Promotes code reuse

---

## Disadvantages

❌ Creates multiple classes

❌ Slight increase in complexity

❌ Client must know which strategy to use

---

## When to Use

✅ Multiple ways to perform the same task

✅ Large `if-else` or `switch` statements

✅ Need to switch behavior at runtime

Examples:

* Payment Methods
* Discount Rules
* Tax Calculations
* Sorting Algorithms
* Shipping Calculations

---

## Strategy vs Factory Method

| Feature        | Strategy                     | Factory Method        |
| -------------- | ---------------------------- | --------------------- |
| Purpose        | Select an algorithm/behavior | Create objects        |
| Category       | Behavioral                   | Creational            |
| Runtime Change | Yes                          | Usually No            |
| Example        | Payment Method               | Create Payment Object |

---

## Strategy vs State

| Feature       | Strategy             | State                        |
| ------------- | -------------------- | ---------------------------- |
| Purpose       | Choose behavior      | Represent object state       |
| Controlled By | Client               | Object itself                |
| Change        | Explicitly by client | Automatically based on state |

---

## Interview Definition

> **Strategy is a behavioral design pattern that defines a family of algorithms, encapsulates each one in a separate class, and makes them interchangeable at runtime, allowing behavior to be selected without modifying client code.**

### Easy Memory Trick

* **Strategy = Different ways to do the same thing**
* Payment by Credit Card, PayPal, UPI
* Sorting by QuickSort, MergeSort, BubbleSort
* Shipping by Air, Sea, Road

**Question to ask yourself:**
*"Do I have multiple algorithms or behaviors that can be swapped at runtime?"*

If **Yes → Use Strategy Pattern**.
