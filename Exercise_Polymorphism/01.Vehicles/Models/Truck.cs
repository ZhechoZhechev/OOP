
namespace Vehicles.Models
{
    using System;
    public class Truck : Vehicle
    {
        private const double CONS_WITH_AIRCONDITION = 1.6;
        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption + CONS_WITH_AIRCONDITION, tankCapacity)
        {
        }
        public override void Refuel(double liters)
        {
            if (liters <= 0)
            {
                Console.WriteLine("Fuel must be a positive number");
            }
            else if (this.fuelQuantity + (liters * 0.95) > this.tankCapacity)
            {
                Console.WriteLine($"Cannot fit {liters} fuel in the tank");
            }
            else
            {
                this.fuelQuantity += liters * 0.95;
            }
        }
    }
}
