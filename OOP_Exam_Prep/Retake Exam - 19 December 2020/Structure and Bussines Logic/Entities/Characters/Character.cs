using System;
using System.Globalization;
using System.Transactions;
using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        // TODO: Implement the rest of the class.
        private string name;
        private double baseHealth;
        private double health;
        private double baseArmor;
        private double armor;
        private double abilityPoints;
        private Bag bag;

        public Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
            Name = name;
            BaseHealth = health;
            Health = health;
            BaseArmor = armor;
            Armor = armor;
            AbilityPoints = abilityPoints;
            this.bag = bag;
            IsAlive = true;

        }
        public Bag Bag
        {
            get { return bag; }
            private set { bag = value; }
        }

        public double AbilityPoints
        {
            get { return abilityPoints; }
            private set { abilityPoints = value; }
        }

        public double Armor
        {
            get => armor;
            private set
            {
                if (value < 0)
                    armor = 0;
                else
                    armor = value;
            }
        }

        public double BaseArmor
        {
            get { return baseArmor; }
            private set { baseArmor = value; }
        }

        public double Health
        {
            get => health;
            set
            {
                if (value > baseHealth)
                    health = baseHealth;
                else if (value < 0)
                    health = 0;
                else
                    health = value;
            }
        }

        public double BaseHealth
        {
            get { return baseHealth; }
            private set { baseHealth = value; }
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);

                name = value;
            }
        }


        public bool IsAlive { get; set; }

        public void TakeDamage(double hitPoints) 
        {
            EnsureAlive();
            if (Armor > hitPoints)
                Armor -= hitPoints;
            else
            {
                if (Armor + Health > hitPoints)
                {
                    Health -= hitPoints - Armor;
                }
                else
                {
                    Health = 0;
                }
                Armor = 0;
            }
        }
        public void UseItem(Item item) 
        {
            EnsureAlive();

            item.AffectCharacter(this);
        }
        public void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }
        public override string ToString()
        {
            string aliveOrNot = this.IsAlive ? "Alive" : "Dead";
            return $"{Name} - HP: {Health}/{BaseHealth}, AP: {Armor}/{BaseArmor}, Status: {aliveOrNot}";
        }
    }
}