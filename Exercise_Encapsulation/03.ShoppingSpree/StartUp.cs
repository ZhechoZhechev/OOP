using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.ShoppingSpree
    {
    public class StartUp
    {
        static void Main()
        {
            List<Person> people = new List<Person>();
            List<Product> products = new List<Product>();
            try
            {
                people = ReadPeopleInfo();
                products = ReadProductsInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string personName = input.Split().First();
                string productName = input.Split().Last();
                Person currentPerson = people.Find(p => p.Name == personName);
                Product currentProduct = products.Find(p => p.Name == productName);
                if (currentPerson != null && currentProduct != null)
                    currentPerson.BuyProduct(currentProduct);
            }

            foreach (Person person in people)
            {
                if (person.Products.Any())
                    Console.WriteLine(person.Name + " - " + String.Join(", ", person.Products));
                else
                    Console.WriteLine($"{person.Name} - Nothing bought");
            }
        }

        private static List<Person> ReadPeopleInfo()
        {
            List<Person> people = new List<Person>();
            string[] info = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < info.Length; i++)
            {
                string name = info[i]
                    .Split('=', StringSplitOptions.RemoveEmptyEntries).First();
                double money = double.Parse(info[i]
                    .Split('=', StringSplitOptions.RemoveEmptyEntries).Last());
                people.Add(new Person(name, money));
            }
            return people;
        }

        private static List<Product> ReadProductsInfo()
        {
            List<Product> products = new List<Product>();
            string[] info = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < info.Length; i++)
            {
                string name = info[i]
                    .Split('=', StringSplitOptions.RemoveEmptyEntries).First();
                double cost = double.Parse(info[i]
                    .Split('=', StringSplitOptions.RemoveEmptyEntries).Last());
                products.Add(new Product(name, cost));
            }
            return products;
        }
    }
}