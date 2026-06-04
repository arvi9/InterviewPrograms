# Microservices Architecture

## Definition

**Microservices** is an architectural style where an application is divided into **small, independent, loosely coupled services**, with each service responsible for a specific business capability.

Each microservice can:

* Be developed independently
* Be deployed independently
* Have its own database
* Scale independently
* Be owned by a separate team

---

## Monolith vs Microservices

| Monolith               | Microservices             |
| ---------------------- | ------------------------- |
| Single application     | Multiple small services   |
| Single deployment      | Independent deployments   |
| Shared database        | Database per service      |
| Scale entire app       | Scale individual services |
| Easier initially       | More complex initially    |
| Harder for large teams | Better for large teams    |

---

## Example: E-Commerce System

### Monolithic Architecture

```text
E-Commerce Application

├── Product Module
├── Order Module
├── Customer Module
├── Payment Module
└── Shipping Module

Single Database
```

---

### Microservices Architecture

```text
E-Commerce System

├── Product Service
├── Order Service
├── Customer Service
├── Payment Service
└── Shipping Service
```

Each service has its own database.

```text
Product Service  → Product DB
Order Service    → Order DB
Customer Service → Customer DB
Payment Service  → Payment DB
Shipping Service → Shipping DB
```

---

# Key Characteristics

| Characteristic         | Description                                           |
| ---------------------- | ----------------------------------------------------- |
| Independent Deployment | Deploy one service without affecting others           |
| Decentralized Data     | Each service owns its data                            |
| Fault Isolation        | Failure of one service should not crash entire system |
| Scalability            | Scale only the busy service                           |
| Technology Flexibility | Different services can use different technologies     |

---

# Communication Between Services

## 1. Synchronous Communication

Using REST APIs or gRPC.

```text
Order Service
      ↓
Payment Service
      ↓
Response
```

Example:

```csharp
var paymentResult =
    await _paymentApi.ProcessPayment(order);
```

### Pros

* Simple
* Easy to understand

### Cons

* Tight runtime dependency
* Higher latency

---

## 2. Asynchronous Communication

Using message brokers.

```text
Order Service
      ↓
Publish Event
      ↓
RabbitMQ / Kafka
      ↓
Payment Service
```

Example:

```csharp
await _bus.Publish(
    new OrderPlacedEvent(orderId));
```

### Pros

* Loosely coupled
* Better scalability
* Better resilience

### Cons

* More complex
* Eventual consistency

---

# API Gateway

Clients should not call every service directly.

```text
Client
   ↓
API Gateway
   ↓
--------------------
| Order Service    |
| Product Service  |
| Payment Service  |
--------------------
```

Responsibilities:

* Authentication
* Authorization
* Routing
* Rate Limiting
* Logging

Popular options:

* YARP
* Ocelot
* Kong
* NGINX

---

# Service Discovery

Services need to find each other.

```text
Order Service
      ↓
Service Registry
      ↓
Payment Service
```

Tools:

* Consul
* Eureka
* Kubernetes Service Discovery

---

# Database Per Service

### Bad

```text
Order Service
Payment Service
Customer Service
      ↓
 Shared Database
```

Problems:

* Tight coupling
* Schema conflicts
* Deployment issues

---

### Good

```text
Order Service    → Order DB
Payment Service  → Payment DB
Customer Service → Customer DB
```

Benefits:

* Loose coupling
* Independent deployments
* Better scalability

---

# Event-Driven Architecture

Services communicate using events.

```text
Order Placed
      ↓
RabbitMQ
      ↓
Payment Service
      ↓
Payment Completed
      ↓
Shipping Service
```

Popular brokers:

* RabbitMQ
* Apache Kafka
* Azure Service Bus

---

# Distributed Transactions

A transaction may span multiple services.

Example:

```text
1. Create Order
2. Process Payment
3. Reserve Inventory
```

Avoid:

```text
Distributed SQL Transactions
```

Use:

```text
Saga Pattern
```

### Saga Example

```text
Order Created
      ↓
Payment Processed
      ↓
Inventory Reserved
      ↓
Shipping Created
```

If Inventory fails:

```text
Refund Payment
Cancel Order
```

---

# Resilience Patterns

## Circuit Breaker

Stops calling a failing service.

```text
Order Service
      ↓
Payment Service (Down)
      ↓
Circuit Open
```

.NET Example:

```csharp
services.AddHttpClient()
        .AddPolicyHandler(
            Policy.Handle<HttpRequestException>()
                  .CircuitBreakerAsync(
                      5,
                      TimeSpan.FromSeconds(30)));
```

---

## Retry

Retry temporary failures.

```csharp
Policy
   .Handle<HttpRequestException>()
   .RetryAsync(3);
```

---

## Bulkhead

Prevents one service from consuming all resources.

---

# Observability

Essential in microservices.

### Logging

* Centralized logging

Tools:

* Serilog
* ELK Stack

### Monitoring

Tools:

* Prometheus
* Grafana

### Distributed Tracing

Tools:

* OpenTelemetry
* Jaeger

---

# Microservices with ASP.NET Core

Typical Stack:

| Component        | Technology              |
| ---------------- | ----------------------- |
| API              | ASP.NET Core Web API    |
| Database         | SQL Server / PostgreSQL |
| ORM              | EF Core                 |
| Messaging        | RabbitMQ / Kafka        |
| Gateway          | YARP / Ocelot           |
| Cache            | Redis                   |
| Containerization | Docker                  |
| Orchestration    | Kubernetes              |
| Monitoring       | Prometheus + Grafana    |
| Logging          | Serilog                 |
| Cloud            | Azure                   |

---

# Advantages

✅ Independent deployment

✅ Independent scaling

✅ Fault isolation

✅ Technology flexibility

✅ Faster development for large teams

✅ Better maintainability

---

# Disadvantages

❌ Operational complexity

❌ Network latency

❌ Distributed transactions

❌ Eventual consistency

❌ Monitoring complexity

❌ Higher infrastructure cost

---

# Principal Software Engineer Interview Answer

**"What are Microservices?"**

> Microservices is an architectural style where an application is decomposed into small, independently deployable services aligned to business capabilities. Each service owns its data, communicates through APIs or events, and can be developed, deployed, and scaled independently. Key concerns include service discovery, API gateways, resilience, observability, distributed transactions, and event-driven communication. Microservices are most suitable for large, complex systems with multiple teams and scalability requirements.
