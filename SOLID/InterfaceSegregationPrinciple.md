# I - Interface Segregation Principle (ISP)

## Definition

**Clients should not be forced to depend on interfaces they do not use.**

In simple terms:

> **Many small, focused interfaces are better than one large interface.**

---

## Bad Example (Violates ISP)

```csharp
public interface IWorker
{
    void Work();
    void Eat();
}
```

### Human Worker

```csharp
public class HumanWorker : IWorker
{
    public void Work()
    {
        Console.WriteLine("Working");
    }

    public void Eat()
    {
        Console.WriteLine("Eating");
    }
}
```

### Robot Worker

```csharp
public class RobotWorker : IWorker
{
    public void Work()
    {
        Console.WriteLine("Working");
    }

    public void Eat()
    {
        throw new NotImplementedException();
    }
}
```

### Problem

A robot does not eat, but it is forced to implement `Eat()`.

This violates ISP.

---

## Good Example (Follows ISP)

Split the interface into smaller interfaces.

### Work Interface

```csharp
public interface IWorkable
{
    void Work();
}
```

### Eat Interface

```csharp
public interface IEatable
{
    void Eat();
}
```

### Human Worker

```csharp
public class HumanWorker : IWorkable, IEatable
{
    public void Work()
    {
        Console.WriteLine("Working");
    }

    public void Eat()
    {
        Console.WriteLine("Eating");
    }
}
```

### Robot Worker

```csharp
public class RobotWorker : IWorkable
{
    public void Work()
    {
        Console.WriteLine("Working");
    }
}
```

Now each class implements only what it needs.

---

## ASP.NET Core Example

### Bad

```csharp
public interface IFileStorage
{
    void UploadFile();
    void DownloadFile();
    void DeleteFile();
    void GenerateThumbnail();
}
```

### Azure Blob Storage

```csharp
public class AzureBlobStorage : IFileStorage
{
    public void UploadFile() { }

    public void DownloadFile() { }

    public void DeleteFile() { }

    public void GenerateThumbnail()
    {
        throw new NotImplementedException();
    }
}
```

### Problem

Not every storage provider supports thumbnails.

---

## Good

```csharp
public interface IFileStorage
{
    void UploadFile();
    void DownloadFile();
}
```

```csharp
public interface IFileDeletion
{
    void DeleteFile();
}
```

```csharp
public interface IThumbnailGenerator
{
    void GenerateThumbnail();
}
```

Implement only the required interfaces.

---

## Real-World Example

### Bad

```text
IMultiFunctionMachine
    ├─ Print()
    ├─ Scan()
    ├─ Fax()
    └─ Copy()
```

A simple printer must implement all methods.

---

### Good

```text
IPrinter
    └─ Print()

IScanner
    └─ Scan()

IFax
    └─ Fax()

ICopier
    └─ Copy()
```

Classes implement only the features they support.

---

## Benefits

✅ Smaller interfaces

✅ Less coupling

✅ Easier maintenance

✅ Easier testing

✅ Better flexibility

---

## Interview Definition

> **Interface Segregation Principle (ISP) states that clients should not be forced to depend on methods they do not use. Large interfaces should be split into smaller, more specific interfaces.**

---

## Easy Memory Trick

**I = Interface Segregation**

Ask yourself:

> **"Am I forcing a class to implement methods it doesn't need?"**

If **Yes**, ISP is violated.

### Example

❌ Bad

```csharp
public interface IWorker
{
    void Work();
    void Eat();
}
```

Robot must implement `Eat()`.

✅ Good

```csharp
IWorkable
IEatable
```

Robot implements only `IWorkable`.

### Key Rule

**Keep interfaces small and focused.**

**Don't force classes to implement unused methods.**
