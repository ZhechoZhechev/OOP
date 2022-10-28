namespace NeedForSpeed
{
   public class Vehicle
    {
        const double DefaultFuelConsumption = 1.25;
        public int HorsePower { get; set; }
        public double Fuel { get; set; }
        public Vehicle(int horsePower, double fuel)
        {
            HorsePower = horsePower;
            Fuel = fuel;
        }
        public virtual double FuelConsumption
        {
            get
            {
                return DefaultFuelConsumption;
            }
        }
        public virtual void Drive(double kilometers)
        {
            Fuel -= FuelConsumption * kilometers;
        }
    }
}
