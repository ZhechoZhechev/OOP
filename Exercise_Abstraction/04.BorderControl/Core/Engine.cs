
namespace BorderControl.Core
{
    using System;

    using BorderControl.IO.Interfaces;
    using BorderControl.Models;
    using BorderControl.Models.Interfaces;
    using Interfaces;
    using System.Collections.Generic;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly Citizen citizen;
        private readonly Robot robot;


        private Engine()
        {
            this.citizen = new Citizen();
            this.robot = new Robot();
        }
        public Engine(IReader reader, IWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            List<IDentifiable> entities = new List<IDentifiable>();

            string input = string.Empty;
            while ((input = this.reader.ReadLine()) != "End")
            {
                string[] info = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = info[0];
                if (info.Length == 3)
                {
                    int age = int.Parse(info[1]);
                    string id = info[2];

                    Citizen citizen = new Citizen(age, name, id);
                    entities.Add(citizen);
                }
                else if (info.Length == 2)
                {
                    string id = info[1];

                    Robot robot = new Robot(name, id);
                    entities.Add(robot);
                }
            }
            string fakeIdsLast = this.reader.ReadLine();

            foreach (var item in entities)
            {
                string result = item.CheckIds(fakeIdsLast);

                if (result != null) writer.WriteLine(result);
            }
        }
    }
}
