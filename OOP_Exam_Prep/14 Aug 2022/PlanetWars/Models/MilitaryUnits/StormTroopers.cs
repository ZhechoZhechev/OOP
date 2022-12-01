namespace PlanetWars.Models.MilitaryUnits
{
    public class StormTroopers : MilitaryUnit
    {
        private const double STORM_TROOPER_COST = 2.5;
        public StormTroopers()
            : base(STORM_TROOPER_COST)
        {
        }
    }
}
