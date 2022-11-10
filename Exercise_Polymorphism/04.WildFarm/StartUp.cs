namespace WildFarm
{
    using System;
    using System.Collections.Generic;
    using WildFarm.Models.Animlas;
    using WildFarm.Models.Foods;

    public class StartUp
    {
        static void Main()
        {
            string userInput = string.Empty;

            ICollection<Animal> animals = new List<Animal>();
            while ((userInput = Console.ReadLine()) != "End")
            {
                try
                {

                    Animal animal = Factory.CreateAnimal(userInput);
                    Console.WriteLine(animal.AskForFood());
                    Food food = Factory.GetFood(Console.ReadLine());
                    animals.Add(animal);
                    animal.Feed(food);

                }
                catch (InvalidOperationException io)
                {
                    Console.WriteLine(io.Message);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }

        }
    }
}
