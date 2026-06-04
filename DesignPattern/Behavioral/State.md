# State Design Pattern

## Definition

The **State** is a **Behavioral Design Pattern** that allows an object to change its behavior when its internal state changes.

The object appears to change its class at runtime.

---

## Problem

Suppose an Order can have different states:

* New
* Paid
* Shipped
* Delivered

Without State Pattern:

```csharp
public void ProcessOrder()
{
    if (state == "New")
    {
        Console.WriteLine("Order Created");
    }
    else if (state == "Paid")
    {
        Console.WriteLine("Order Paid");
    }
    else if (state == "Shipped")
    {
        Console.WriteLine("Order Shipped");
    }
}
```

As states increase, the code becomes difficult to maintain.

---

## Solution

Move state-specific behavior into separate state classes.

---

## C# Example

### State Interface

```csharp
public interface IOrderState
{
    void Handle(OrderContext context);
}
```

---

### Concrete States

```csharp
public class NewState : IOrderState
{
    public void Handle(OrderContext context)
    {
        Console.WriteLine("Order Created");
        context.SetState(new PaidState());
    }
}

public class PaidState : IOrderState
{
    public void Handle(OrderContext context)
    {
        Console.WriteLine("Order Paid");
        context.SetState(new ShippedState());
    }
}

public class ShippedState : IOrderState
{
    public void Handle(OrderContext context)
    {
        Console.WriteLine("Order Shipped");
    }
}
```

---

### Context

```csharp
public class OrderContext
{
    private IOrderState _state;

    public OrderContext(IOrderState state)
    {
        _state = state;
    }

    public void SetState(IOrderState state)
    {
        _state = state;
    }

    public void Process()
    {
        _state.Handle(this);
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
        OrderContext order =
            new OrderContext(new NewState());

        order.Process();
        order.Process();
        order.Process();
    }
}
```

---

### Output

```text
Order Created
Order Paid
Order Shipped
```

---

## Class Diagram

```text
          IOrderState
                ^
                |
   +------------+------------+
   |            |            |
NewState   PaidState   ShippedState
                ^
                |
          OrderContext
```

---

## Real-World Examples

### Order Processing

```text
New
 ↓
Paid
 ↓
Shipped
 ↓
Delivered
```

### Vending Machine

```text
No Coin
   ↓
Coin Inserted
   ↓
Product Dispensed
```

### Traffic Signal

```text
Red
 ↓
Green
 ↓
Yellow
 ↓
Red
```

### Document Workflow

```text
Draft
 ↓
Review
 ↓
Approved
 ↓
Published
```

---

## Advantages

✅ Eliminates large `if-else` or `switch` statements

✅ State-specific behavior is isolated

✅ Easier to add new states

✅ Follows Open/Closed Principle (OCP)

---

## Disadvantages

❌ Creates many state classes

❌ Can increase complexity for simple workflows

---

## When to Use

✅ Object behavior changes based on state

✅ Large `if-else` or `switch` statements based on state

✅ State transitions are complex

Examples:

* Order Management
* Workflow Systems
* Traffic Lights
* Vending Machines
* Document Approval Processes

---

## State vs Strategy

| Feature         | State                   | Strategy                   |
| --------------- | ----------------------- | -------------------------- |
| Purpose         | Represent object state  | Represent an algorithm     |
| State Change    | Usually automatic       | Chosen by client           |
| Behavior Change | Based on internal state | Based on selected strategy |
| Example         | Order Status            | Payment Method             |

### Example

**Strategy**

```csharp
context.SetStrategy(new PayPalPayment());
```

Client chooses the behavior.

**State**

```csharp
context.SetState(new PaidState());
```

The object often changes state itself as part of processing.

---

## State vs Command

| Feature | State                          | Command              |
| ------- | ------------------------------ | -------------------- |
| Purpose | Change behavior based on state | Encapsulate requests |
| Focus   | State transitions              | Actions              |
| Example | Order Status                   | CreateOrderCommand   |

---

## Interview Definition

> **State is a behavioral design pattern that allows an object to alter its behavior when its internal state changes, by encapsulating state-specific behavior into separate state classes.**

### Easy Memory Trick

* **State = Behavior changes because state changes**
* The object behaves differently depending on its current state.

**Examples:**

* Order: New → Paid → Shipped
* Traffic Light: Red → Green → Yellow
* Vending Machine: No Coin → Coin Inserted → Dispense

**Question to ask yourself:**
*"Does an object's behavior change based on its current state?"*

If **Yes → Use State Pattern**.
