# S - Single Responsibility Principle (SRP)

## Definition

**A class should have only one reason to change.**

In other words, a class should have **one responsibility and one job only**.

---

## Bad Example (Violates SRP)

```csharp
public class Employee
{
    public void CalculateSalary()
    {
        Console.WriteLine("Calculating Salary");
    }

    public void SaveToDatabase()
    {
        Console.WriteLine("Saving Employee");
    }

    public void GenerateReport()
    {
        Console.WriteLine("Generating Report");
    }
}
```

### Problems

The `Employee` class has multiple responsibilities:

1. Salary Calculation
2. Database Operations
3. Report Generation

If any of these requirements change, the class must be modified.

---

## Good Example (Follows SRP)

### Employee

```csharp
public class Employee
{
    public string Name { get; set; }
    public decimal Salary { get; set; }
}
```

### Salary Service

```csharp
public class SalaryCalculator
{
    public decimal Calculate(Employee employee)
    {
        return employee.Salary;
    }
}
```

### Repository

```csharp
public class EmployeeRepository
{
    public void Save(Employee employee)
    {
        Console.WriteLine("Employee Saved");
    }
}
```

### Report Service

```csharp
public class EmployeeReport
{
    public void Generate(Employee employee)
    {
        Console.WriteLine("Report Generated");
    }
}
```

Now each class has a single responsibility.

---

## Real-World Example

### Bad

```text
Employee Class
 ├─ Calculate Salary
 ├─ Save Employee
 ├─ Generate Report
 └─ Send Email
```

### Good

```text
Employee
SalaryCalculator
EmployeeRepository
EmployeeReport
EmailService
```

Each class handles one responsibility.

---

## ASP.NET Core Example

### Bad

```csharp
public class OrderService
{
    public void CreateOrder()
    {
        // Create Order
        // Save Order
        // Send Email
        // Generate Invoice
    }
}
```

### Good

```csharp
public class OrderService
{
    public void CreateOrder()
    {
        // Create Order
    }
}

public class OrderRepository
{
    public void Save()
    {
    }
}

public class EmailService
{
    public void Send()
    {
    }
}

public class InvoiceService
{
    public void Generate()
    {
    }
}
```

---

## Benefits

✅ Easier to maintain

✅ Easier to test

✅ Easier to reuse

✅ Less coupling

✅ Better readability

---

## Interview Definition

> **Single Responsibility Principle (SRP) states that a class should have only one responsibility and therefore only one reason to change.**

---

## Easy Memory Trick

**S = Single Responsibility**

Ask yourself:

> **"Does this class have more than one job?"**

If **Yes**, split it into multiple classes.

### Example

❌ One class does:

* Save Data
* Send Email
* Generate Report

✅ Separate classes:

* Repository
* Email Service
* Report Service

**One Class = One Responsibility = One Reason to Change**.
