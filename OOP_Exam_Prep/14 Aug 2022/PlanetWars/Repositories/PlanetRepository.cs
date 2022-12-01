
namespace PlanetWars.Repositories
{
    using Contracts;
    using Models.Planets.Contracts;
    using System.Collections.Generic;

    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> planets;

        public PlanetRepository()
        {
            this.planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => this.planets.AsReadOnly();

        public void AddItem(IPlanet model)
        {
            this.planets.Add(model);
        }

        public IPlanet FindByName(string name)
        {
            return this.planets.Find(x => x.Name == name);
        }

        public bool RemoveItem(string name)
        {
            IPlanet planetToRemove = FindByName(name);
            return this.planets.Remove(planetToRemove);
        }
    }
}
