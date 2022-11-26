
namespace Heroes.Models.Map
{
    using System;

    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Heroes;

    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            var knights = new List<Knight>();
            var barbarians = new List<Barbarian>();

            foreach (var hero in players)
            {
                if (hero.IsAlive && hero.Weapon != null)
                {
                    if (hero is Knight knight)
                    {
                        knights.Add(knight);
                    }
                    else if (hero is Barbarian barbarian)
                    {
                        barbarians.Add(barbarian);
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid hero type.");
                    }
                }
            }
            while (knights.Any(x => x.IsAlive) && barbarians.Any(x => x.IsAlive))
            {
                foreach (var knight in knights)
                {
                    if (knight.IsAlive && knight.Weapon.Durability > 0)
                    {
                        var curKnightDamage = knight.Weapon.DoDamage();

                        foreach (var barbarian in barbarians)
                        {
                            if (barbarian.IsAlive)
                            {
                                barbarian.TakeDamage(curKnightDamage);
                            }
                        }
                    }

                }
                foreach (var barbarian in barbarians)
                {
                    if (barbarian.IsAlive && barbarian.Weapon.Durability > 0)
                    {
                        var curBarbarianDamage = barbarian.Weapon.DoDamage();

                        foreach (var knight in knights)
                        {
                            if (knight.IsAlive)
                            {
                                knight.TakeDamage(curBarbarianDamage);
                            }
                        }
                    }

                }
            }

            if (!barbarians.Any(x => x.IsAlive))
            {
                return $"The knights took {knights.Where(k => !k.IsAlive).Count()} casualties but won the battle.";
            }
            else
            {
                return $"The barbarians took {barbarians.Where(b => !b.IsAlive).Count()} casualties but won the battle.";
            }
        }
    }
}
