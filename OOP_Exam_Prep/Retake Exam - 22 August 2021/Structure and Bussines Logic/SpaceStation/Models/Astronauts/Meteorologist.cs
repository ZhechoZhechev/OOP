namespace SpaceStation.Models.Astronauts
{
    public class Meteorologist : Astronaut
    {
        private const double MeteorologistStartingOxigen = 90;
        public Meteorologist(string name)
            : base(name, MeteorologistStartingOxigen)
        {
        }
    }
}
