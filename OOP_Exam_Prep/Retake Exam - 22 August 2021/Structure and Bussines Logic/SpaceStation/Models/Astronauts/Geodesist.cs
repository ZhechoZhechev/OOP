namespace SpaceStation.Models.Astronauts
{
    public class Geodesist : Astronaut
    {
        private const double GeodesistStartingOxigen = 50;
        public Geodesist(string name)
            : base(name, GeodesistStartingOxigen)
        {
        }
    }
}
