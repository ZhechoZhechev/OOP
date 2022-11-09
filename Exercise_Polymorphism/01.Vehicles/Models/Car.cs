namespace Vehicles.Models
{
    public class Car : Vehicle
    {
        private const double CONS_WITH_AIRCONDITION = 0.9;
        public Car(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption + CONS_WITH_AIRCONDITION, tankCapacity)
        {
        }
    }
}
