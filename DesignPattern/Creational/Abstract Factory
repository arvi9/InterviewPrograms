# Abstract Factory Design Pattern

## Definition

The **Abstract Factory** is a **Creational Design Pattern** that provides an interface for creating **families of related objects** without specifying their concrete classes.

While **Factory Method** creates **one object**, **Abstract Factory** creates **multiple related objects**.

---

## Real-World Example

Imagine building UI controls for different operating systems:

### Windows Family

* Windows Button
* Windows TextBox

### Mac Family

* Mac Button
* Mac TextBox

The client should get all controls from the same family.

---

## Structure

```text
Abstract Factory
       |
       +----------------+
       |                |
WindowsFactory    MacFactory
       |                |
       |                |
   Button          Button
   TextBox         TextBox
```

---

## C# Example

### Abstract Products

```csharp
public interface IButton
{
    void Render();
}

public interface ITextBox
{
    void Render();
}
```

---

### Concrete Products

```csharp
public class WindowsButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Windows Button");
    }
}

public class MacButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Mac Button");
    }
}

public class WindowsTextBox : ITextBox
{
    public void Render()
    {
        Console.WriteLine("Windows TextBox");
    }
}

public class MacTextBox : ITextBox
{
    public void Render()
    {
        Console.WriteLine("Mac TextBox");
    }
}
```

---

### Abstract Factory

```csharp
public interface IUIFactory
{
    IButton CreateButton();
    ITextBox CreateTextBox();
}
```

---

### Concrete Factories

```csharp
public class WindowsFactory : IUIFactory
{
    public IButton CreateButton()
    {
        return new WindowsButton();
    }

    public ITextBox CreateTextBox()
    {
        return new WindowsTextBox();
    }
}

public class MacFactory : IUIFactory
{
    public IButton CreateButton()
    {
        return new MacButton();
    }

    public ITextBox CreateTextBox()
    {
        return new MacTextBox();
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
        IUIFactory factory = new WindowsFactory();

        IButton button = factory.CreateButton();
        ITextBox textBox = factory.CreateTextBox();

        button.Render();
        textBox.Render();
    }
}
```

---

### Output

```text
Windows Button
Windows TextBox
```

---

## Factory Method vs Abstract Factory

| Feature    | Factory Method      | Abstract Factory                      |
| ---------- | ------------------- | ------------------------------------- |
| Creates    | One object          | Family of related objects             |
| Complexity | Simple              | More complex                          |
| Example    | Create Notification | Create UI Controls (Button + TextBox) |
| Focus      | Single product      | Product family                        |

---

## Advantages

✅ Ensures related objects work together

✅ Promotes consistency

✅ Follows Open/Closed Principle (OCP)

✅ Reduces dependency on concrete classes

---

## Disadvantages

❌ More interfaces and classes

❌ Adding a new product type requires changes to all factories

---

## When to Use

✅ When you need to create a family of related objects.

✅ When products must be used together.

✅ When switching between entire product families (Windows, Mac, Linux).

---

## Interview Definition

> **Abstract Factory is a creational design pattern that provides an interface for creating families of related or dependent objects without specifying their concrete classes. It allows clients to work with multiple related products through a common factory interface.**
