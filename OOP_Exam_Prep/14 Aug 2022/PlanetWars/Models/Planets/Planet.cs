
namespace PlanetWars.Models.Planets
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Contracts;
    using Repositories;
    using Weapons.Contracts;
    using MilitaryUnits.Contracts;
    using Utilities.Messages;

    public class Planet : IPlanet
    {
        private UnitRepository army;
        private WeaponRepository weapons;
        private string name;
        private double budget;

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            this.army = new UnitRepository();
            this.weapons = new WeaponRepository();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);

                this.name = value;
            }
        }

        public double Budget
        {
            get => this.budget;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);

                this.budget = value;
            }
        }

        public double MilitaryPower
        {
            get
            {
                double totalAmount = Army.Select(x => x.EnduranceLevel).Sum() +
                     Weapons.Select(x => x.DestructionLevel).Sum();

                if (Army.Any(x => x.GetType().Name == "AnonymousImpactUnit"))
                    totalAmount *= 1.30;
                if (Weapons.Any(x => x.GetType().Name == "NuclearWeapon"))
                    totalAmount *= 1.45;

                return Math.Round(totalAmount, 3);
            }
        }

        public IReadOnlyCollection<IMilitaryUnit> Army => this.army.Models;

        public IReadOnlyCollection<IWeapon> Weapons => this.weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
            army.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.AddItem(weapon);
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();
            string forcesResult = army.Models.Any() ?
                $"--Forces: {string.Join(", ", army.Models.Select(x => x.GetType().Name))}"
                : "--Forces: No units";
            string equipmentReult = weapons.Models.Any() ?
                $"--Combat equipment: {string.Join(", ", weapons.Models.Select(x => x.GetType().Name))}"
                : "--Combat equipment: No weapons";

            sb.AppendLine($"Planet: {Name}")
              .AppendLine($"--Budget: {Budget} billion QUID")
              .AppendLine(forcesResult)
              .AppendLine(equipmentReult)
              .AppendLine($"--Military Power: {MilitaryPower}");

            return sb.ToString().TrimEnd();
        }

        public void Profit(double amount)
        {
            this.budget += amount;
        }

        public void Spend(double amount)
        {
            if (this.budget < amount)
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            this.budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var unit in Army)
            {
                unit.IncreaseEndurance();
            }
        }
    }
}
