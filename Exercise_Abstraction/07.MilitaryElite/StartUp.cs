
namespace MilitaryElite
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MilitaryElite.Models.Enums;
    using MilitaryElite.Models.Interfaces;
    using Models;
    class StartUp
    {
        static void Main()
        {
            var soldiers = new HashSet<ISoldier>();

            string input = string.Empty;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] commands = input.Split();

                string soldierType = commands[0];
                int id = int.Parse(commands[1]);
                string firstName = commands[2];
                string lastName = commands[3];

                Soldier soldier;
                if (soldierType == "Private")
                {
                    decimal salary = decimal.Parse(commands[4]);
                    soldier = new Private(firstName, lastName, id, salary);
                }
                else if (soldierType == "LieutenantGeneral")
                {
                    decimal salary = decimal.Parse(commands[4]);
                    ICollection<IPrivate> privates = FindPrivates(commands, soldiers);
                    soldier = new LieutenantGeneral(firstName, lastName, id, salary, privates);
                }
                else if (soldierType == "Engineer")
                {
                    decimal salary = decimal.Parse(commands[4]);
                    string corpsText = commands[5];

                    bool isCorpsValid = Enum.TryParse(corpsText, true, out Corps corps);
                    if (!isCorpsValid) continue;
                    ICollection<IRepair> repairs = AssemblyRepairs(commands);
                    soldier = new Engineer(firstName, lastName, id, salary, corps, repairs);
                }
                else if (soldierType == "Commando")
                {
                    decimal salary = decimal.Parse(commands[4]);
                    string corpsText = commands[5];

                    bool isCorpsValid = Enum.TryParse(corpsText, true, out Corps corps);
                    if (!isCorpsValid) continue;
                    ICollection<IMission> missions = AssemblyMissions(commands);
                    soldier = new Commando(firstName, lastName, id, salary, corps, missions);
                }
                else if (soldierType == "Spy")
                {
                    int codeNumber = int.Parse(commands[4]);
                    soldier = new Spy(firstName, lastName, id, codeNumber);
                }
                else continue;
                soldiers.Add(soldier);

            }
                PrintSoldiers(soldiers);
        }

        private static ICollection<IMission> AssemblyMissions(string[] commands)
        {
            string[] missInfo = commands.Skip(6).ToArray();
            ICollection<IMission> missions = new HashSet<IMission>();

            for (int i = 0; i < missInfo.Length; i += 2)
            {
                string codeName = missInfo[i];
                string stateText = missInfo[i + 1];

                bool isStateValid = Enum.TryParse<State>(stateText, false, out State state);
                if (!isStateValid) continue;

                IMission mission = new Mission(codeName, state);
                missions.Add(mission);
            }
            return missions;
        }

        private static ICollection<IRepair> AssemblyRepairs(string[] commands)
        {
            string[] repInfo = commands.Skip(6).ToArray();
            ICollection<IRepair> repairs = new HashSet<IRepair>();

            for (int i = 0; i < repInfo.Length; i += 2)
            {
                string partName = repInfo[i];
                int hoursworked = int.Parse(repInfo[i + 1]);

                IRepair repair = new Repair(partName, hoursworked);
                repairs.Add(repair);
            }
            return repairs;
        }

        private static ICollection<IPrivate> FindPrivates(string[] commands, HashSet<ISoldier> soldiers)
        {
            int[] privatesIds = commands.Skip(5)
                .Select(int.Parse).ToArray();
            ICollection<IPrivate> privates = new HashSet<IPrivate>();

            foreach (var id in privatesIds)
            {
                var curSoldier = (IPrivate)soldiers.FirstOrDefault(x => x.Id == id);
                privates.Add(curSoldier);
            }
            return privates;
        }
        private static void PrintSoldiers(HashSet<ISoldier> soldiers)
        {
            foreach (ISoldier soldier in soldiers)
            {
                Console.WriteLine(soldier.ToString());
            }
        }
    }
}
