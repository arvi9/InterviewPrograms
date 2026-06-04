# Proxy Design Pattern

## Definition

The **Proxy** is a **Structural Design Pattern** that provides a **placeholder or surrogate object** to control access to another object.

The proxy acts as an intermediary between the client and the real object.

---

## Real-World Example

### ATM Machine

```text
Customer
    ↓
ATM (Proxy)
    ↓
Bank Account (Real Object)
```

The ATM verifies the user before allowing access to the bank account.

---

## Problem

Suppose accessing a file is expensive or requires authorization:

```csharp
FileService fileService = new FileService();
fileService.ReadFile();
```

You may want to:

* Check permissions
* Log access
* Cache results
* Delay object creation

Without modifying `FileService`.

---

## Solution

Create a Proxy that controls access to the real object.

---

## C# Example

### Subject Interface

```csharp
public interface IFileService
{
    void ReadFile();
}
```

---

### Real Subject

```csharp
public class FileService : IFileService
{
    public void ReadFile()
    {
        Console.WriteLine("Reading File...");
    }
}
```

---

### Proxy

```csharp
public class FileServiceProxy : IFileService
{
    private readonly FileService _fileService;

    public FileServiceProxy()
    {
        _fileService = new FileService();
    }

    public void ReadFile()
    {
        Console.WriteLine("Checking Permissions...");

        bool hasAccess = true;

        if (hasAccess)
        {
            _fileService.ReadFile();
        }
        else
        {
            Console.WriteLine("Access Denied");
        }
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
        IFileService service = new FileServiceProxy();

        service.ReadFile();
    }
}
```

---

### Output

```text
Checking Permissions...
Reading File...
```

---

## Class Diagram

```text
Client
   |
   v
IFileService
   ^
   |
+------------------+
|                  |
FileServiceProxy   FileService
      |
      +----> Controls Access
```

---

## Types of Proxy

### 1. Virtual Proxy

Creates expensive objects only when needed.

```csharp
Image image = new ImageProxy();
image.Display();
```

The real image loads only on first use.

---

### 2. Protection Proxy

Controls access based on permissions.

```csharp
if(user.IsAdmin)
{
    realObject.Execute();
}
```

---

### 3. Remote Proxy

Represents an object located on another server.

Example:

```text
Client
   ↓
Proxy
   ↓
Remote Service
```

Used in distributed systems.

---

### 4. Caching Proxy

Stores results to improve performance.

```csharp
product = cache.Get(id);

if(product == null)
{
    product = database.Get(id);
}
```

---

## Real-World Software Examples

### ASP.NET Core

Authorization Middleware

```csharp
app.UseAuthorization();
```

Acts like a protection proxy before accessing resources.

### Entity Framework

Lazy Loading Proxies

```csharp
var customer = context.Customers.First();
customer.Orders;
```

Orders are loaded only when accessed.

### API Gateway

```text
Client
   ↓
API Gateway
   ↓
Microservices
```

The gateway acts as a proxy.

---

## Advantages

✅ Controls access to objects

✅ Adds security

✅ Supports lazy loading

✅ Can improve performance through caching

✅ Hides remote communication details

---

## Disadvantages

❌ Adds extra layer of abstraction

❌ Can increase complexity

❌ Additional object creation

---

## When to Use

✅ Need authorization checks

✅ Need lazy loading

✅ Need caching

✅ Need remote service access

✅ Need logging/auditing before object access

---

## Proxy vs Decorator

| Feature     | Proxy                           | Decorator            |
| ----------- | ------------------------------- | -------------------- |
| Purpose     | Control access                  | Add behavior         |
| Focus       | Security, caching, lazy loading | Extend functionality |
| Client View | Same interface                  | Same interface       |
| Example     | Authorization Proxy             | Logging Decorator    |

---

## Proxy vs Adapter

| Feature   | Proxy            | Adapter               |
| --------- | ---------------- | --------------------- |
| Purpose   | Control access   | Convert interface     |
| Interface | Same as target   | Different from target |
| Example   | FileServiceProxy | PaymentAdapter        |

---

## Interview Definition

> **Proxy is a structural design pattern that provides a surrogate or placeholder object to control access to another object, allowing features such as authorization, lazy loading, caching, logging, or remote access without changing the real object's code.**
