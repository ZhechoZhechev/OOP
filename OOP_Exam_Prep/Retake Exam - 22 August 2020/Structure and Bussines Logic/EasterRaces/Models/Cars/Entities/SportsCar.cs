namespace EasterRaces.Models.Cars.Entities
{
    public class SportsCar : Car
    {
        private const double SportsCarCubicCms = 3000;
        private const int SportsCarMinHP = 250;
        private const int SportsCarMaxHP = 450;

        public SportsCar(string model, int horsePower)
            : base(model, horsePower, SportsCarCubicCms, SportsCarMinHP, SportsCarMaxHP)
        {
        }
    }
}
