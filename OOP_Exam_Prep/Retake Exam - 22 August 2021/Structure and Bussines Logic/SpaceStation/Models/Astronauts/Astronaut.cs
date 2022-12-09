using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags;
using SpaceStation.Models.Bags.Contracts;
using System;
using System.Linq;
using System.Text;

namespace SpaceStation.Models.Astronauts
{
    public abstract class Astronaut : IAstronaut
    {
        private string name;
        private double oxygen;
        private IBag bag;

        public Astronaut(string name, double oxygen)
        {
            Name = name;
            Oxygen = oxygen;
            bag = new Backpack();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Astronaut name cannot be null or empty.");

                this.name = value;
            }
        }

        public double Oxygen
        {
            get => this.oxygen;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException("Cannot create Astronaut with negative oxygen!");

                this.oxygen = value;
            }
        }

        public bool CanBreath => oxygen > 0;

        public IBag Bag => this.bag;

        public virtual void Breath()
        {
           Oxygen =  Math.Max(Oxygen - 10, 0);
        }

        public override string ToString()
        {
            string result = this.Bag.Items.Any() ? string.Join(", ", this.Bag.Items) : "none";

            StringBuilder sb = new StringBuilder();


            sb.AppendLine($"Name: {Name}")
                .AppendLine($"Oxygen: {Oxygen}")
                .AppendLine($"Bag items: {result}");

            return sb.ToString().TrimEnd();
        }
    }
}
