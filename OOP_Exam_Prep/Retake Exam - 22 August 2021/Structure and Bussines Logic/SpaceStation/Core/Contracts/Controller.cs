using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Core.Contracts
{
    public class Controller : IController
    {
        private AstronautRepository astronauts;
        private PlanetRepository planets;
        private Mission mission;
        private HashSet<string> exploredPlanets;
        public Controller()
        {
            this.astronauts = new AstronautRepository();
            this.planets = new PlanetRepository();
            this.mission = new Mission();
            this.exploredPlanets = new HashSet<string>();
        }
        public string AddAstronaut(string type, string astronautName)
        {

            IAstronaut astronaut;
            switch (type)
            {
                case "Biologist": astronaut = new Biologist(astronautName);
                    break;
                case "Geodesist": astronaut = new Geodesist(astronautName);
                    break;
                case "Meteorologist": astronaut = new Meteorologist(astronautName);
                    break;
                default: throw new InvalidOperationException("Astronaut type doesn't exists!");
            }

            this.astronauts.Add(astronaut);
            return $"Successfully added {astronaut.GetType().Name}: {astronautName}!";
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            var planet = new Planet(planetName);
            foreach (var item in items)
            {
                planet.Items.Add(item);
            }

            this.planets.Add(planet);
            return $"Successfully added Planet: {planetName}!";
        }

        public string ExplorePlanet(string planetName)
        {
            ICollection<IAstronaut> astronautsSelected = this.astronauts.Models.Where(x => x.Oxygen > 60).ToArray();

            if (!astronautsSelected.Any())
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            var planetToExplore = this.planets.FindByName(planetName);

            mission.Explore(planetToExplore, astronautsSelected);
            exploredPlanets.Add(planetName);

            return $"Planet: {planetName} was explored! Exploration finished with" +
                $" {astronautsSelected.Where(x => x.CanBreath == false).Count()} dead astronauts!";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{exploredPlanets.Count} planets were explored!")
                .AppendLine("Astronauts info:");
            foreach (var astro in astronauts.Models)
            {
                sb.AppendLine(astro.ToString());
            }

            return sb.ToString().TrimEnd();
                
        }

        public string RetireAstronaut(string astronautName)
        {
            var astronautToRetire = astronauts.FindByName(astronautName);
            if (astronautToRetire == null)
            {
                throw new InvalidOperationException($"Astronaut {astronautName} doesn't exists!");
            }

            this.astronauts.Remove(astronautToRetire);
            return $"Astronaut {astronautName} was retired!";
        }
    }
}
