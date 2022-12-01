
namespace PlanetWars.Models.Weapons
{
    using System;

    using Contracts;
    using Utilities.Messages;

    public abstract class Weapon : IWeapon
    {
        private int destructionLevel;

        public Weapon(int destructionLevel, double price)
        {
            Price = price;
            DestructionLevel = destructionLevel;
        }
        public double Price { get; private set; }

        public int DestructionLevel 
        {
            get => this.destructionLevel;
            private set 
            {
                if (value < 1)
                    throw new ArgumentException(ExceptionMessages.TooLowDestructionLevel);
                else if (value > 10)
                    throw new ArgumentException(ExceptionMessages.TooHighDestructionLevel);

                this.destructionLevel = value;
            }
        }
    }
}
