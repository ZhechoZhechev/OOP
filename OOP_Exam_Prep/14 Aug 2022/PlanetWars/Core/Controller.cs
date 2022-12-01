namespace PlanetWars.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Utilities.Messages;
    using Repositories;
    using Models.Planets;
    using Models.MilitaryUnits.Contracts;
    using Models.MilitaryUnits;
    using Models.Weapons.Contracts;
    using Models.Weapons;
    using Models.Planets.Contracts;

    public class Controller : IController
    {
        private PlanetRepository planets;

        public Controller()
        {
            this.planets = new PlanetRepository();
        }

        public object IMillitaryUnit { get; private set; }

        public string AddUnit(string unitTypeName, string planetName)
        {
            if (this.planets.FindByName(planetName) == null)
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.UnexistingPlanet, planetName));

            if (!new string[] { "SpaceForces", "StormTroopers", "AnonymousImpactUnit" }.Contains(unitTypeName))
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));

            var planetToAdUnit = planets.FindByName(planetName);

            if (planetToAdUnit.Army.Any(x => x.GetType().Name == unitTypeName))
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));

            IMilitaryUnit unitToAdd = null;
            switch (unitTypeName)
            {
                case "SpaceForces": unitToAdd = new SpaceForces();
                    break;
                case "StormTroopers": unitToAdd = new StormTroopers();
                    break;
                case "AnonymousImpactUnit": unitToAdd = new AnonymousImpactUnit();
                    break;
            }

            planetToAdUnit.Spend(unitToAdd.Cost);
            planetToAdUnit.AddUnit(unitToAdd);

            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            if(planets.FindByName(planetName)== null)
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.UnexistingPlanet, planetName));

            var planetToAddWeapon = planets.FindByName(planetName);

            if (planetToAddWeapon.Weapons.Any(x => x.GetType().Name == weaponTypeName))
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));

            if(!new string[] { "BioChemicalWeapon", "NuclearWeapon", "SpaceMissiles" }.Contains(weaponTypeName))
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));

            IWeapon weaponToAdd = null;
            switch (weaponTypeName)
            {
                case "BioChemicalWeapon": weaponToAdd = new BioChemicalWeapon(destructionLevel);
                    break;
                case "NuclearWeapon": weaponToAdd = new NuclearWeapon(destructionLevel);
                    break;
                case "SpaceMissiles": weaponToAdd = new SpaceMissiles(destructionLevel);
                    break;
            }

            planetToAddWeapon.Spend(weaponToAdd.Price);
            planetToAddWeapon.AddWeapon(weaponToAdd);

            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            if (this.planets.FindByName(name) != null)
                return string.Format(OutputMessages.ExistingPlanet, name);

            this.planets.AddItem(new Planet(name, budget));
            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var planet in planets.Models
                .OrderByDescending(x => x.MilitaryPower)
                .ThenBy(x => x.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }
            return sb.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            var firtsPlanet = planets.FindByName(planetOne);
            var secondPlanet = planets.FindByName(planetTwo);

            string winner = "none";
            if (firtsPlanet.MilitaryPower > secondPlanet.MilitaryPower)
            {
                winner = "first";
            }
            else if (firtsPlanet.MilitaryPower < secondPlanet.MilitaryPower)
            {
                winner = "second";
            }

            if (winner == "none")
            {
                bool firstHasNuclear = firtsPlanet.Weapons.Any(x => x.GetType().Name == "NuclearWeapon");
                bool secondHasNuclear = secondPlanet.Weapons.Any(x => x.GetType().Name == "NuclearWeapon");

                if (firstHasNuclear && !secondHasNuclear)
                {
                    winner = "first";
                }
                else if (!firstHasNuclear && secondHasNuclear)
                {
                    winner = "second";
                }
            }
            string output = string.Empty;
            switch (winner)
            {
                case "first":
                    output = CombatAftermath(firtsPlanet, secondPlanet); break;
                case "second":
                    output = CombatAftermath(secondPlanet, firtsPlanet); break;
                case "none":
                    firtsPlanet.Spend(firtsPlanet.Budget / 2);
                    secondPlanet.Spend(secondPlanet.Budget / 2);
                    output = OutputMessages.NoWinner;
                    break;
            }
            return output;
        }
        private string CombatAftermath(IPlanet winner, IPlanet loser)
        {
            double salvageProfit = loser.Army.Sum(x => x.Cost) +
                                   loser.Weapons.Sum(x => x.Price);

            winner.Spend(winner.Budget / 2);
            winner.Profit(loser.Budget / 2);
            winner.Profit(salvageProfit);
            planets.RemoveItem(loser.Name);
            return String.Format(OutputMessages.WinnigTheWar, winner.Name, loser.Name);
        }

        public string SpecializeForces(string planetName)
        {
            if(planets.FindByName(planetName) == null)
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.UnexistingPlanet, planetName));

            var targetedPlanet = planets.FindByName(planetName);

            if (!targetedPlanet.Army.Any())
                throw new InvalidOperationException(ExceptionMessages.NoUnitsFound);

            targetedPlanet.Spend(1.25);
            targetedPlanet.TrainArmy();

            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }
    }
}
