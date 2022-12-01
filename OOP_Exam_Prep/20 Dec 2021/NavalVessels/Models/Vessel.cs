
namespace NavalVessels.Models
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    using Contracts;
    public abstract class Vessel : IVessel
    {
        private string name;
        private ICaptain captain;
        private List<string> targets;

        public Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
        {
            Name = name;
            MainWeaponCaliber = mainWeaponCaliber;
            Speed = speed;
            ArmorThickness = armorThickness;
            targets = new List<string>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Vessel name cannot be null or empty.");

                this.name = value;
            }
        }

        public ICaptain Captain
        {
            get => this.captain;
            set
            {
                if (value == null)
                    throw new NullReferenceException("Captain cannot be null.");

                this.captain = value;
            }
        }
        public double ArmorThickness { get; set; }

        public double MainWeaponCaliber { get; protected set; }

        public double Speed { get; protected set; }

        public ICollection<string> Targets => this.targets.AsReadOnly();

        public void Attack(IVessel target)
        {
            if (target == null)
                throw new NullReferenceException("Target cannot be null.");

            if (target.ArmorThickness > this.MainWeaponCaliber)
                target.ArmorThickness -= this.MainWeaponCaliber;
            else
                target.ArmorThickness = 0;

            this.targets.Add(target.Name);
        }

        public abstract void RepairVessel();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string targetResult = targets.Count == 0 ? "None" : string.Join(", ", targets);

            sb.AppendLine($"- {Name}")
                .AppendLine($" *Type: {this.GetType().Name}")
                .AppendLine($" *Armor thickness: {ArmorThickness}")
                .AppendLine($" *Main weapon caliber: {MainWeaponCaliber}")
                .AppendLine($" *Speed: {Speed} knots")
                .AppendLine($" *Targets: {targetResult}");

            return sb.ToString().TrimEnd();
        }
    }
}
