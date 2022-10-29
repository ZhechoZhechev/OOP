using System;
using System.Collections.Generic;

namespace Animals
{
    public class StartUp
    {
        public static void Main()
        {
            List<Animal> animals = new List<Animal>();

            string input = string.Empty;

            while ((input = Console.ReadLine()) != "Beast!")
            {
                string animalType = input;
                string[] info = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = info[0];
                int age = int.Parse(info[1]);
                string gender = info[2];

                Animal animal = new Animal();

                if (animalType == "Dog") animal = new Dog(name, age, gender);
                else if (animalType == "Cat") animal = new Cat(name, age, gender);
                else if (animalType == "Frog") animal = new Frog(name, age, gender);
                else if (animalType == "Kitten") animal = new Kitten(name, age);
                else if (animalType == "Tomcat") animal = new Tomcat(name, age);

                animals.Add(animal);
            }

            foreach (var item in animals)
            {
                Console.WriteLine(item);
            }

        }
    }
}
