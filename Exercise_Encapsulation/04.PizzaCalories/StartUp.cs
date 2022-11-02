using System;

namespace _04.PizzaCalories
{
    class StartUp
    {
        static void Main()
        {
            try
            {
                string[] pizzaInfo = Console.ReadLine()
                        .Split(" ");
                string pizzaName = pizzaInfo[1];

                string[] doughInfo = Console.ReadLine()
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string flourType = doughInfo[1];
                string bakingTechnique = doughInfo[2];
                double doughGrams = double.Parse(doughInfo[3]);

                Dough dough = new Dough(flourType, bakingTechnique, doughGrams);
                Pizza pizza = new Pizza(pizzaName, dough);


                string input = string.Empty;

                while ((input = Console.ReadLine()) != "END")
                {
                    string[] toppingInfo = input
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    string toppingType = toppingInfo[1];
                    double toppingGrams = double.Parse(toppingInfo[2]);
                    
                    Topping topping = new Topping(toppingType, toppingGrams);

                    pizza.AddTopping(topping);
                }
                Console.WriteLine(pizza);
            }
            catch (ArgumentException ae)
            {

                Console.WriteLine(ae.Message);
            }
        }
    }
}
