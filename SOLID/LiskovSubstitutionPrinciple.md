# L - Liskov Substitution Principle (LSP)

## Definition

**Objects of a derived class should be replaceable with objects of the base class without affecting the correctness of the program.**

In simple terms:

> **A child class should be able to replace its parent class without breaking the application.**

---

## Bad Example (Violates LSP)

### Bird Example

```csharp
public class Bird
{
    public virtual void Fly()
    {
        Console.WriteLine("Flying");
    }
}
```

```csharp
public class Sparrow : Bird
{
    public override void Fly()
    {
        Console.WriteLine("Sparrow Flying");
    }
}
```

```csharp
public class Ostrich : Bird
{
    public override void Fly()
    {
        throw new NotImplementedException();
    }
}
```

### Client Code

```csharp
public void MakeBirdFly(Bird bird)
{
    bird.Fly();
}
```

```csharp
MakeBirdFly(new Sparrow()); // Works
MakeBirdFly(new Ostrich()); // Exception
```

### Problem

`Ostrich` is a `Bird`, but it cannot perform the behavior expected by `Bird`.

This breaks LSP.

---

## Good Example (Follows LSP)

Separate flying behavior.

### Base Class

```csharp
public abstract class Bird
{
}
```

### Flying Birds

```csharp
public interface IFlyable
{
    void Fly();
}
```

```csharp
public class Sparrow : Bird, IFlyable
{
    public void Fly()
    {
        Console.WriteLine("Sparrow Flying");
    }
}
```

### Non-Flying Bird

```csharp
public class Ostrich : Bird
{
}
```

### Client Code

```csharp
public void MakeBirdFly(IFlyable bird)
{
    bird.Fly();
}
```

Now only flying birds are passed.

---

## Another Example

### Bad

```csharp
public class Rectangle
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }
}
```

```csharp
public class Square : Rectangle
{
    public override int Width
    {
        set
        {
            base.Width = value;
            base.Height = value;
        }
    }

    public override int Height
    {
        set
        {
            base.Width = value;
            base.Height = value;
        }
    }
}
```

### Problem

```csharp
Rectangle rect = new Square();

rect.Width = 5;
rect.Height = 10;
```

Expected:

```text
Width = 5
Height = 10
```

Actual:

```text
Width = 10
Height = 10
```

The behavior changes unexpectedly.

This violates LSP.

---

## ASP.NET Core Example

### Bad

```csharp
public interface IFileStorage
{
    void UploadFile();
    void DeleteFile();
}
```

```csharp
public class ReadOnlyStorage : IFileStorage
{
    public void UploadFile()
    {
        throw new NotSupportedException();
    }

    public void DeleteFile()
    {
        throw new NotSupportedException();
    }
}
```

A consumer expects all implementations to support upload and delete.

This breaks LSP.

---

## Benefits

✅ Reliable inheritance

✅ Better polymorphism

✅ Fewer runtime errors

✅ Easier maintenance

✅ Better code reuse

---

## Interview Definition

> **Liskov Substitution Principle (LSP) states that objects of a derived class should be substitutable for objects of their base class without altering the correctness of the program.**

---

## Easy Memory Trick

**L = Replace Child with Parent Safely**

Ask yourself:

> **"Can I replace the parent object with the child object without breaking the code?"**

If **No**, LSP is violated.

### Example

❌ Bad

```csharp
Bird bird = new Ostrich();
bird.Fly(); // Exception
```

✅ Good

```csharp
IFlyable bird = new Sparrow();
bird.Fly();
```

### Key Rule

**Inheritance should model a true "is-a" relationship.**

If a child class cannot fully behave like its parent, inheritance is probably the wrong choice.
