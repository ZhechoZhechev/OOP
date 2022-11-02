
namespace _03.ShoppingSpree
{
    using System;
    using System.Collections.Generic;
    public class Person
    {
        private string name;
        private double money;
        private List<Product> products;

        public Person()
        {
            this.products = new List<Product>();
        }
        public Person(string name, double money) : this()
        {
            Name = name;
            Money = money;
        }
        public IReadOnlyCollection<Product> Products { get { return products.AsReadOnly(); } }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ErrorMessages.EmptyNameMsg);
                }
                this.name = value;
            }
        }
        public double Money
        {
            get => this.money;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ErrorMessages.NegativeMoneyValue);
                }
                this.money = value;
            }
        }
        public void BuyProduct(Product product) 
        {
            if (this.money >= product.Cost)
            {
                Console.WriteLine($"{this.Name} bought {product}");
                this.money -= product.Cost;
                this.products.Add(product);
            }
            else
                Console.WriteLine($"{this.Name} can't afford {product}");
        }
    }
}
