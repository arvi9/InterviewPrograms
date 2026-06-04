# Mediator Design Pattern

## Definition

The **Mediator** is a **Behavioral Design Pattern** that defines an object that encapsulates how a group of objects interact.

Instead of objects communicating directly with each other, they communicate through a **Mediator**, reducing dependencies between them.

---

## Real-World Example

### Air Traffic Control

```text
Airplane A
      \
       \
Airplane B ---> Air Traffic Controller <--- Airplane C
       /
      /
Airplane D
```

Airplanes do not communicate directly with each other. They communicate through the Air Traffic Controller (Mediator).

---

## Problem

Without a Mediator:

```text
User1 <--> User2
User1 <--> User3
User2 <--> User3
```

Every object needs references to every other object, creating tight coupling.

---

## Solution

Introduce a Mediator that handles communication.

---

## C# Example

### Mediator Interface

```csharp
public interface IChatMediator
{
    void SendMessage(string message, User sender);
}
```

---

### Concrete Mediator

```csharp
public class ChatMediator : IChatMediator
{
    private readonly List<User> _users = new();

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public void SendMessage(string message, User sender)
    {
        foreach (var user in _users)
        {
            if (user != sender)
            {
                user.Receive(message);
            }
        }
    }
}
```

---

### Colleague Class

```csharp
public class User
{
    private readonly IChatMediator _mediator;

    public string Name { get; }

    public User(string name, IChatMediator mediator)
    {
        Name = name;
        _mediator = mediator;
    }

    public void Send(string message)
    {
        Console.WriteLine($"{Name}: {message}");
        _mediator.SendMessage(message, this);
    }

    public void Receive(string message)
    {
        Console.WriteLine($"{Name} received: {message}");
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
        ChatMediator mediator = new ChatMediator();

        User john = new User("John", mediator);
        User alice = new User("Alice", mediator);
        User bob = new User("Bob", mediator);

        mediator.AddUser(john);
        mediator.AddUser(alice);
        mediator.AddUser(bob);

        john.Send("Hello Everyone!");
    }
}
```

---

### Output

```text
John: Hello Everyone!
Alice received: Hello Everyone!
Bob received: Hello Everyone!
```

---

## Class Diagram

```text
         IChatMediator
                ^
                |
        ChatMediator
                ^
                |
+---------------+---------------+
|               |               |
User         User           User
```

---

## Real-World Software Examples

### Chat Application

```text
Users
   ↓
Chat Server (Mediator)
   ↓
Other Users
```

### ASP.NET Core with MediatR

```csharp
await mediator.Send(new CreateOrderCommand());
```

Instead of controllers directly calling services, requests go through MediatR.

### CQRS

```csharp
CreateOrderCommand
        ↓
      MediatR
        ↓
CreateOrderHandler
```

Mediator coordinates communication.

---

## Advantages

✅ Reduces coupling between objects

✅ Centralizes communication logic

✅ Easier to maintain and extend

✅ Follows Single Responsibility Principle (SRP)

---

## Disadvantages

❌ Mediator can become very large

❌ Too much logic in one place can create a "God Object"

---

## When to Use

✅ Many objects communicate with each other

✅ Communication logic is complex

✅ Want to reduce object dependencies

Examples:

* Chat Systems
* Workflow Engines
* UI Components
* CQRS with MediatR
* Air Traffic Control Systems

---

## Mediator vs Observer

| Feature       | Mediator                 | Observer            |
| ------------- | ------------------------ | ------------------- |
| Purpose       | Coordinate communication | Notify subscribers  |
| Communication | Through central mediator | Subject → Observers |
| Relationship  | Many-to-many             | One-to-many         |
| Example       | Chat Room                | Email Notifications |

---

## Mediator vs Command

| Feature | Mediator           | Command              |
| ------- | ------------------ | -------------------- |
| Purpose | Coordinate objects | Encapsulate requests |
| Focus   | Communication      | Actions              |
| Example | MediatR            | CreateOrderCommand   |

---

## Interview Definition

> **Mediator is a behavioral design pattern that centralizes communication between multiple objects by introducing a mediator object, reducing direct dependencies and simplifying interactions.**

### Easy Memory Trick

* **Mediator = Central Communication Hub**
* Objects do not talk to each other directly.
* All communication goes through the mediator.

**Examples:**

* Air Traffic Controller
* Chat Server
* MediatR in ASP.NET Core
* Workflow Engine

**Question to ask yourself:**
*"Are too many objects directly communicating with each other?"*

If **Yes → Use Mediator Pattern**.
