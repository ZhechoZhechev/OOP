
namespace SpaceStation.Models.Mission
{
    using System.Linq;
    using System.Collections.Generic;

    using Contracts;
    using Planets.Contracts;
    using Astronauts.Contracts;
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            var astronautsWithOxy = astronauts.Where(x => x.CanBreath == true).ToList();

            foreach (var astro in astronauts)
            {
                while (astro.CanBreath && planet.Items.Any())
                {
                    var itemToPick = planet.Items.First();
                    astro.Bag.Items.Add(itemToPick);
                    planet.Items.Remove(itemToPick);
                    astro.Breath();
                }
            }
        }
    }
}
