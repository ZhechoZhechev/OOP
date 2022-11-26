
namespace Heroes.Models.Heroes
{
    using System;
    using System.Text;
    using Contracts;
    using Weapons;

    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;

        protected Hero(string name, int health, int armour)
        {
            this.Name = name;
            this.Health = health;
            this.Armour = armour;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Hero name cannot be null or empty.");
                }
                this.name = value;
            }
        }

        public int Health
        {
            get => this.health;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Hero health cannot be below 0.");
                }
                this.health = value;
            }
        }

        public int Armour
        {
            get => this.armour;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Hero armour cannot be below 0.");
                }
                this.armour = value;
            }
        }

        public IWeapon Weapon
        {
            get => this.weapon;
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Weapon cannot be null.");
                }
                this.weapon = value;
            }
        }

        public bool IsAlive => this.Health > 0;

        public void AddWeapon(IWeapon weapon)
        {
            this.Weapon = weapon;
        }
        // arm = 10, dmg = 20
        public void TakeDamage(int points)
        {
            if(Armour > points) 
            {
                Armour -= points;
            }
            else
            {
                if (Health > points - Armour)
                {
                    Health -= (points - Armour);
                }
                else
                {
                    Health = 0;
                }
                Armour = 0;
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name}: {this.Name}")
                .AppendLine($"--Health: {this.Health}")
                .AppendLine($"--Armour: {this.Armour}")
                .AppendLine($"--Weapon: {(this.Weapon == null ? "Unarmed" : this.Weapon.Name)}");

            return sb.ToString().TrimEnd();
        }
    }
}
