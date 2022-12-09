namespace EasterRaces.Models.Cars.Entities
{
    public class MuscleCar : Car
    {
        private const double MuscleCarCubicCms = 5000;
        private const int MuscleCarMinHP = 400;
        private const int MuscleCarMaxHP = 600;

        public MuscleCar(string model, int horsePower)
            : base(model, horsePower, MuscleCarCubicCms, MuscleCarMinHP, MuscleCarMaxHP)
        {
        }
    }
}
