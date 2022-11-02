using System;

namespace _03.ShoppingSpree
{
    public class Product
    {
        private string name;
        private double cost;
        public Product(string name, double cost)
        {
            Name = name;
            Cost = cost;
        }

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
        public double Cost
        {
            get => this.cost;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ErrorMessages.NegativeMoneyValue);
                }
                this.cost = value;
            }
        }
        public override string ToString()
            => this.Name;

    }
}
