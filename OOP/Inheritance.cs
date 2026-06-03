public class Vehicle
{
    protected string Make;
    protected string Model;
    protected int Year;

    public Vehicle(string make, string model, int year)
    {
        Make = make;
        Model = model;
        Year = year;
    }

    public void StartEngine()
    {
        Console.WriteLine("Engine started");
    }

    public void StopEngine()
    {
        Console.WriteLine("Engine stopped");
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"{Year} {Make} {Model}");
    }
}
