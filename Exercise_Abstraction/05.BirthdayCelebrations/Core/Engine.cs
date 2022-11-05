
namespace BorderControl.Core
{
    using System;

    using BorderControl.IO.Interfaces;
    using BorderControl.Models;
    using Interfaces;
    using BirthdayCelebrations.Models;
    using BirthdayCelebrations.Models.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly Citizen citizen;
        private readonly Rebel rebel;

        private Engine()
        {
            this.citizen = new Citizen();
            this.rebel = new Rebel();
        }
        public Engine(IReader reader, IWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
           int num = int.Parse(this.reader.ReadLine());

            List<IBuyer> listBuyers = new List<IBuyer>();

            for (int i = 0; i < num; i++)
            {
                string[] input = this.reader.ReadLine()
                     .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = input[0];
                int age = int.Parse(input[1]);
                if (input.Length == 4)
                {
                    string id = input[2];
                    string birthDate = input[3];

                    Citizen citizen = new Citizen(age, name, id, birthDate);
                    listBuyers.Add(citizen);
                }
                else if (input.Length == 3)
                {
                    string group = input[2];
                    Rebel rebel = new Rebel(name, age, group);
                    listBuyers.Add(rebel);
                }
            }

            string info = string.Empty;
            while ((info = reader.ReadLine()) != "End")
            {
                string nameToBuyFood = info;
                var buyer = listBuyers.FirstOrDefault(x => x.Name == nameToBuyFood);
                if (buyer != null)
                {
                    buyer.BuyFood();
                }
                else continue;
            }

            writer.WriteLine(listBuyers.Sum(x => x.Food).ToString());
        }
    }
}
