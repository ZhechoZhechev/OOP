
namespace Heroes.Core
{
    using System;
    using System.Linq;

    using Contracts;
    using Repositories;
    using Models.Contracts;
    using Repositories.Contracts;
    using Models.Heroes;
    using Models.Weapons;
    using Models.Map;
    using System.Text;

    public class Controller : IController
    {
        private readonly IRepository<IHero> heroes;
        private readonly IRepository<IWeapon> weapons;
        public Controller()
        {
            this.heroes = new HeroRepository();
            this.weapons = new WeaponRepository();
        }
        public string CreateHero(string type, string name, int health, int armour)
        {
            if (this.heroes.FindByName(name) != null)
                throw new InvalidOperationException($"The hero {name} already exists.");

            if (type != typeof(Barbarian).Name && type != typeof(Knight).Name)
                throw new InvalidOperationException("Invalid hero type.");

            IHero hero;
            if (type == typeof(Barbarian).Name)
            {
                hero = new Barbarian(name, health, armour);
                heroes.Add(hero);
                return $"Successfully added Barbarian {name} to the collection.";
            }
            else
            {
                hero = new Knight(name, health, armour);
                heroes.Add(hero);
                return $"Successfully added Sir {name} to the collection.";
            }

        }
        public string CreateWeapon(string type, string name, int durability)
        {
            if (weapons.FindByName(name) != null)
                throw new InvalidOperationException($"The weapon {name} already exists.");

            if (type != typeof(Mace).Name && type != typeof(Claymore).Name)
                throw new InvalidOperationException("Invalid weapon type.");

            IWeapon weapon;
            if (type == typeof(Mace).Name)
            {
                weapon = new Mace(name, durability);
                weapons.Add(weapon);
                return $"A {type.ToLower()} {name} is added to the collection.";
            }
            else
            {
                weapon = new Claymore(name, durability);
                weapons.Add(weapon);
                return $"A {type.ToLower()} {name} is added to the collection.";
            }
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            var hero = heroes.FindByName(heroName);
            var weapon = weapons.FindByName(weaponName);

            if (hero == null)
                throw new InvalidOperationException($"Hero {heroName} does not exist.");

            if (weapon == null)
                throw new InvalidOperationException($"Hero {weaponName} does not exist.");

            if (hero.Weapon != null)
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");

            var weaponType = weapon.GetType().Name.ToLower();

            hero.AddWeapon(weapon);
            this.weapons.Remove(weapon);
            return $"Hero {heroName} can participate in battle using a {weaponType}.";
        }

        public string StartBattle()
        {
            var map = new Map();

            var aliveAndAmrmedHeroes = this.heroes.Models
                .Where(x => x.IsAlive && x.Weapon != null)
                .ToList();

            return map.Fight(aliveAndAmrmedHeroes);
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();

            var sortedHeroes = heroes.Models
                .OrderBy(x => x.GetType().Name)
                .ThenByDescending(y => y.Health)
                .ThenBy(z => z.Name);
            foreach (var hero in sortedHeroes)
            {
                sb.AppendLine(hero.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
