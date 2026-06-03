// Method Overloading ( or Compile-time (Static) Polymorphism):
using System;

public class Calculator
{
    // Two ints
    public int Add(int a, int b)
    {
        return a + b;
    }

    // Two doubles
    public double Add(double a, double b)
    {
        return a + b;
    }

    // Three ints
    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }
}

public class Program
{
    public static void Main()
    {
        var calc = new Calculator();
        Console.WriteLine(calc.Add(2, 3));        // Calls Add(int, int) -> 5
        Console.WriteLine(calc.Add(2.5, 3.5));    // Calls Add(double, double) -> 6
        Console.WriteLine(calc.Add(1, 2, 3));     // Calls Add(int, int, int) -> 6
    }
}
