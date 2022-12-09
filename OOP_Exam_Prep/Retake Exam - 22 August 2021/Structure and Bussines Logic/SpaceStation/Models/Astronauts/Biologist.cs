using System;

namespace SpaceStation.Models.Astronauts
{
    public class Biologist : Astronaut
    {
        private const double BiologistStartingOxigen = 70;
        public Biologist(string name)
            : base(name, BiologistStartingOxigen)
        {
        }
        public override void Breath()
        {
            Oxygen = Math.Max(Oxygen - 5, 0);
        }
    }
}
