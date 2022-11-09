
namespace Raiding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Raiding.Models;
    class StartUp
    {
        static void Main()
        {
            List<BaseHero> raid = new List<BaseHero>();

            int membersNum = int.Parse(Console.ReadLine());

            while (raid.Count < membersNum)
            {
                string name = Console.ReadLine();
                string type = Console.ReadLine();
                try
                {
                    raid.Add(CreateNewHero(name, type));
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }

            }
            int bossHealth = int.Parse(Console.ReadLine());

            foreach (var hero in raid)
            {
                Console.WriteLine(hero.CastAbility());
            }
            int raidDmg = raid.Select(x => x.Power).Sum();

            string result = raidDmg >= bossHealth ? "Victory!" : "Defeat...";

            Console.WriteLine(result);
        }

        private static BaseHero CreateNewHero(string name, string type)
        {
            switch (type)
            {
                case "Druid":
                    return new Druid(name);
                case "Paladin":
                    return new Paladin(name);
                case "Rogue":
                    return new Rogue(name);
                case "Warrior":
                    return new Warrior(name);
                default: throw new ArgumentException("Invalid hero!");
            }
        }
    }
}
