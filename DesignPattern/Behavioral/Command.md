# Command Design Pattern

## Definition

The **Command** is a **Behavioral Design Pattern** that encapsulates a request as an object, allowing you to parameterize, queue, log, and undo operations.

Instead of calling a method directly, you create a command object that contains all information needed to perform the action.

---

## Real-World Example

### TV Remote Control

```text
Remote Button (Invoker)
        ↓
     Command
        ↓
       TV (Receiver)
```

When you press the Power button, the remote doesn't turn on the TV directly. It executes a command that knows how to call the TV.

---

## Problem

Without Command Pattern:

```csharp
tv.TurnOn();
tv.TurnOff();
```

The client is tightly coupled to the TV implementation.

---

## Solution

Encapsulate each request in a command object.

---

## C# Example

### Receiver

```csharp
public class TV
{
    public void TurnOn()
    {
        Console.WriteLine("TV is ON");
    }

    public void TurnOff()
    {
        Console.WriteLine("TV is OFF");
    }
}
```

---

### Command Interface

```csharp
public interface ICommand
{
    void Execute();
}
```

---

### Concrete Commands

```csharp
public class TurnOnCommand : ICommand
{
    private readonly TV _tv;

    public TurnOnCommand(TV tv)
    {
        _tv = tv;
    }

    public void Execute()
    {
        _tv.TurnOn();
    }
}

public class TurnOffCommand : ICommand
{
    private readonly TV _tv;

    public TurnOffCommand(TV tv)
    {
        _tv = tv;
    }

    public void Execute()
    {
        _tv.TurnOff();
    }
}
```

---

### Invoker

```csharp
public class RemoteControl
{
    private ICommand _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void PressButton()
    {
        _command.Execute();
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
        TV tv = new TV();

        ICommand turnOn = new TurnOnCommand(tv);
        ICommand turnOff = new TurnOffCommand(tv);

        RemoteControl remote = new RemoteControl();

        remote.SetCommand(turnOn);
        remote.PressButton();

        remote.SetCommand(turnOff);
        remote.PressButton();
    }
}
```

---

### Output

```text
TV is ON
TV is OFF
```

---

## Class Diagram

```text
Client
   |
   v
RemoteControl (Invoker)
   |
   v
ICommand
   ^
   |
+------------------+
|                  |
TurnOnCommand   TurnOffCommand
       |
       v
      TV (Receiver)
```

---

## Real-World Software Examples

### Undo / Redo

```text
Command Stack
   ↑
Undo
Redo
```

Each action is stored as a command.

### Queue Processing

```text
Client
   ↓
Command Queue
   ↓
Worker
```

Commands can be executed later.

### Background Jobs

* Hangfire
* Quartz.NET

A job is essentially a command executed asynchronously.

### CQRS

```csharp
CreateOrderCommand
UpdateOrderCommand
DeleteOrderCommand
```

Each request is represented as a command object.

---

## Advantages

✅ Decouples sender from receiver

✅ Supports Undo/Redo

✅ Supports logging and auditing

✅ Supports queues and delayed execution

✅ Follows Open/Closed Principle (OCP)

---

## Disadvantages

❌ Creates many command classes

❌ Adds extra complexity

---

## When to Use

✅ Need Undo/Redo functionality

✅ Need request queuing

✅ Need logging/auditing

✅ Need delayed execution

✅ Need to decouple sender and receiver

Examples:

* Remote Controls
* CQRS Commands
* Job Scheduling
* Workflow Systems
* Menu Actions

---

## Command vs Strategy

| Feature        | Command               | Strategy                 |
| -------------- | --------------------- | ------------------------ |
| Purpose        | Encapsulate a request | Encapsulate an algorithm |
| Focus          | Action/Operation      | Behavior/Algorithm       |
| Example        | TurnOnCommand         | PaymentStrategy          |
| Stores Request | Yes                   | No                       |

---

## Command vs Observer

| Feature      | Command                  | Observer                   |
| ------------ | ------------------------ | -------------------------- |
| Purpose      | Execute an action        | Notify subscribers         |
| Relationship | One request → One action | One event → Many listeners |
| Example      | CreateOrderCommand       | OrderCreatedEvent          |

---

## Interview Definition

> **Command is a behavioral design pattern that encapsulates a request as an object, allowing requests to be parameterized, queued, logged, and undone while decoupling the sender from the receiver.**

### Easy Memory Trick

* **Command = Action wrapped in an object**
* A button click becomes a command.
* A job becomes a command.
* A CQRS request becomes a command.

**Examples:**

* Remote Control buttons
* Undo/Redo operations
* Hangfire jobs
* CQRS Commands

**Question to ask yourself:**
*"Do I need to represent an action/request as an object?"*

If **Yes → Use Command Pattern**.
