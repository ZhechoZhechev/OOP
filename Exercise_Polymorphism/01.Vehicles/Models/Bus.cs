namespace Vehicles.Models
{
    using System;
    class Bus : Vehicle
    {
        private const double CONS_WITH_AIRCONDITION = 1.4;
        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }
        public void DriveEmpty(double km)
        {
            base.Drive(km);
        }
        public override void Drive(double km)
        {
            double newFuelAmount = this.fuelQuantity - (this.fuelConsumption + CONS_WITH_AIRCONDITION)  * km;
            if (newFuelAmount < 0)
                Console.WriteLine($"{this.GetType().Name} needs refueling");
            else
            {
                this.fuelQuantity = newFuelAmount;
                Console.WriteLine($"{this.GetType().Name} travelled {km} km");
            }
        }
    }
}
