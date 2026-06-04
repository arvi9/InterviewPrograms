# Domain-Driven Design (DDD)

## Definition

**Domain-Driven Design (DDD)** is a software design approach that focuses on modeling software around the **business domain** and business rules.

The main goal is to create software that closely reflects how the business works, making it easier to understand, maintain, and evolve.

---

## Key Concepts of DDD

| Concept             | Description                                                            |
| ------------------- | ---------------------------------------------------------------------- |
| Domain              | The business area being solved (e.g., E-Commerce, Banking, Healthcare) |
| Entity              | An object with a unique identity                                       |
| Value Object        | An object without identity, defined only by its values                 |
| Aggregate           | A cluster of related objects treated as a single unit                  |
| Aggregate Root      | Entry point to an Aggregate                                            |
| Repository          | Provides access to Aggregate Roots                                     |
| Domain Service      | Business logic that doesn't belong to an Entity                        |
| Application Service | Coordinates use cases and workflows                                    |
| Domain Event        | Something important that happened in the domain                        |
| Bounded Context     | Boundary where a specific domain model applies                         |
| Ubiquitous Language | Common language shared by business and developers                      |

---

# DDD Layered Architecture

```text
Presentation Layer
       ↓
Application Layer
       ↓
Domain Layer
       ↓
Infrastructure Layer
```

### Presentation Layer

* Controllers
* APIs
* UI

### Application Layer

* Use Cases
* Commands
* Queries
* DTOs

### Domain Layer

* Entities
* Value Objects
* Domain Services
* Domain Events

### Infrastructure Layer

* Database
* Email Service
* External APIs
* Message Queues

---

# Entity Example

An Entity has a unique identity.

```csharp
public class Customer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public Customer(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
```

Even if two customers have the same name, their IDs are different.

---

# Value Object Example

A Value Object has no identity.

```csharp
public record Address(
    string Street,
    string City,
    string State,
    string ZipCode);
```

Two addresses with the same values are considered equal.

```csharp
var a1 = new Address("Main St", "Bangalore", "KA", "560001");
var a2 = new Address("Main St", "Bangalore", "KA", "560001");

Console.WriteLine(a1 == a2); // True
```

---

# Aggregate & Aggregate Root

### Example: Order System

```text
Order
 ├── OrderItem
 ├── OrderItem
 └── OrderItem
```

### Aggregate Root

```csharp
public class Order
{
    private readonly List<OrderItem> _items = new();

    public IReadOnlyCollection<OrderItem> Items => _items;

    public void AddItem(string product, decimal price)
    {
        _items.Add(new OrderItem(product, price));
    }
}
```

Clients should interact through `Order`, not directly with `OrderItem`.

---

# Repository Pattern

Repository abstracts data access.

```csharp
public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);

    Task SaveAsync(Order order);
}
```

Implementation:

```csharp
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task SaveAsync(Order order)
    {
        await _context.SaveChangesAsync();
    }
}
```

---

# Domain Service

Business logic that doesn't belong to one entity.

Example:

```csharp
public class DiscountService
{
    public decimal CalculateDiscount(Customer customer, decimal amount)
    {
        return customer.IsPremium
            ? amount * 0.20m
            : amount * 0.05m;
    }
}
```

---

# Application Service

Coordinates the use case.

```csharp
public class OrderApplicationService
{
    private readonly IOrderRepository _repository;

    public OrderApplicationService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task PlaceOrderAsync(Guid orderId)
    {
        var order = await _repository.GetByIdAsync(orderId);

        order.Submit();

        await _repository.SaveAsync(order);
    }
}
```

---

# Domain Event

Represents something important that happened.

```csharp
public record OrderPlacedEvent(Guid OrderId);
```

Raised from domain:

```csharp
AddDomainEvent(
    new OrderPlacedEvent(Id));
```

Handled later:

```csharp
public class OrderPlacedEventHandler
{
    public Task Handle(OrderPlacedEvent notification)
    {
        // Send email
        // Publish message
        return Task.CompletedTask;
    }
}
```

---

# Bounded Context

Large systems are split into separate business areas.

Example:

```text
E-Commerce System

├── Ordering Context
├── Inventory Context
├── Shipping Context
├── Payment Context
└── Customer Context
```

Each context has its own:

* Entities
* Database
* Business rules

For example:

```text
Customer (Ordering Context)

Customer
├── Id
├── Name
└── ShippingAddress
```

```text
Customer (Support Context)

Customer
├── Id
├── Tickets
└── ComplaintHistory
```

Same word, different meaning.

---

# Ubiquitous Language

Business and developers use the same terminology.

Example:

❌ Technical terms

```text
CreateRecord()
InsertData()
UpdateTable()
```

✅ Business terms

```text
PlaceOrder()
ShipOrder()
CancelOrder()
ApprovePayment()
```

Code should reflect business language.

---

# DDD Folder Structure (ASP.NET Core)

```text
src

├── Api
│
├── Application
│   ├── Commands
│   ├── Queries
│   ├── DTOs
│
├── Domain
│   ├── Entities
│   ├── ValueObjects
│   ├── Events
│   ├── Services
│   ├── Repositories
│
├── Infrastructure
│   ├── Persistence
│   ├── Repositories
│   ├── Messaging
│   └── ExternalServices
│
└── Shared
```

---

# When to Use DDD

### Use DDD When

✅ Complex business rules

✅ Large enterprise applications

✅ Multiple teams

✅ Long-term projects

✅ Microservices architecture

Examples:

* Banking
* Insurance
* ERP
* Healthcare
* E-Commerce

---

### Avoid DDD When

❌ Simple CRUD applications

❌ Small internal tools

❌ Simple CMS

❌ Very small projects

DDD adds complexity and may be unnecessary for simple systems.

---

# Principal Software Engineer Interview Answer

**"What is Domain-Driven Design?"**

> Domain-Driven Design is an approach to software design that focuses on modeling software around the business domain. It uses concepts such as Entities, Value Objects, Aggregates, Repositories, Domain Services, Domain Events, and Bounded Contexts to create maintainable and scalable systems. DDD is particularly useful for complex enterprise applications where business rules are the primary source of complexity.
