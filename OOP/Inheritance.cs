using System;

namespace VehicleInheritanceDemo
{
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

    public class ElectricCar : Vehicle
    {
        private int _batteryCapacity;

        public ElectricCar(string make, string model, int year, int batteryCapacity)
            : base(make, model, year)
        {
            _batteryCapacity = batteryCapacity;
        }

        public void ChargeBattery()
        {
            Console.WriteLine($"Charging {_batteryCapacity} kWh battery");
        }
    }

    public class GasCar : Vehicle
    {
        private double _fuelTankSize;

        public GasCar(string make, string model, int year, double fuelTankSize)
            : base(make, model, year)
        {
            _fuelTankSize = fuelTankSize;
        }

        public void FillTank()
        {
            Console.WriteLine($"Filling {_fuelTankSize} L fuel tank");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create an Electric Car object
            ElectricCar tesla = new ElectricCar(
                make: "Tesla",
                model: "Model 3",
                year: 2025,
                batteryCapacity: 75);

            Console.WriteLine("=== Electric Car ===");
            tesla.DisplayInfo();
            tesla.StartEngine();
            tesla.ChargeBattery();
            tesla.StopEngine();

            Console.WriteLine();

            // Create a Gas Car object
            GasCar toyota = new GasCar(
                make: "Toyota",
                model: "Camry",
                year: 2024,
                fuelTankSize: 60);

            Console.WriteLine("=== Gas Car ===");
            toyota.DisplayInfo();
            toyota.StartEngine();
            toyota.FillTank();
            toyota.StopEngine();
        }
    }
}
