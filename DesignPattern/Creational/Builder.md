# Builder Design Pattern

## Definition

The **Builder** is a **Creational Design Pattern** that separates the construction of a complex object from its representation, allowing the same construction process to create different representations.

It is useful when an object has **many optional parameters**.

---

## Problem

Suppose you have a `User` class:

```csharp
public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}
```

Creating objects can become messy:

```csharp
User user = new User
{
    Name = "John",
    Age = 30,
    Email = "john@test.com",
    Address = "New York"
};
```

For larger classes with many optional properties, object creation becomes difficult to read and maintain.

---

## Solution

Use a **Builder** to construct the object step by step.

---

## C# Example

### Product

```csharp
public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}
```

---

### Builder

```csharp
public class UserBuilder
{
    private readonly User _user = new User();

    public UserBuilder SetName(string name)
    {
        _user.Name = name;
        return this;
    }

    public UserBuilder SetAge(int age)
    {
        _user.Age = age;
        return this;
    }

    public UserBuilder SetEmail(string email)
    {
        _user.Email = email;
        return this;
    }

    public UserBuilder SetAddress(string address)
    {
        _user.Address = address;
        return this;
    }

    public User Build()
    {
        return _user;
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
        User user = new UserBuilder()
                        .SetName("John")
                        .SetAge(30)
                        .SetEmail("john@test.com")
                        .SetAddress("New York")
                        .Build();

        Console.WriteLine(user.Name);
    }
}
```

---

### Output

```text
John
```

---

## Real-World Example

### Building a House

```text
House Builder
    → Add Foundation
    → Add Walls
    → Add Roof
    → Build House
```

### Software Examples

* Creating HTTP Requests
* Creating Database Connection Strings
* Building Email Messages
* Configuring Complex Objects

Example:

```csharp
var request = new HttpRequestBuilder()
                    .SetUrl("api/users")
                    .SetMethod("GET")
                    .AddHeader("Authorization", "token")
                    .Build();
```

---

## Advantages

✅ Makes object creation readable

✅ Handles many optional parameters

✅ Supports method chaining (Fluent API)

✅ Separates construction logic from object

---

## Disadvantages

❌ Requires additional builder class

❌ Overkill for simple objects

---

## When to Use

✅ Object has many optional fields

✅ Object creation is complex

✅ Need step-by-step construction

✅ Want readable, fluent code

---

## Factory Method vs Builder

| Feature         | Factory Method      | Builder                             |
| --------------- | ------------------- | ----------------------------------- |
| Purpose         | Create an object    | Build a complex object step by step |
| Complexity      | Simple              | Complex                             |
| Object Creation | Single call         | Multiple steps                      |
| Example         | Create Notification | Create User/House/Request           |

---

## Interview Definition

> **Builder is a creational design pattern that constructs complex objects step by step. It separates the object construction process from its representation, making object creation more readable, flexible, and maintainable.**
