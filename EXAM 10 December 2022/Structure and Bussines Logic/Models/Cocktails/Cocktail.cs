using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        private string name;
        private double price;
        public Cocktail(string cocktailName, string size, double price)
        {
            Name = cocktailName;
            Size = size;
            Price = price;
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);

                this.name = value;
            }
        }

        public string Size { get; private set; }

        public double Price
        {
            get => this.price;
            private set
            {
                if (Size == "Large")
                {
                    this.price = value;
                }
                else if (Size == "Middle")
                {
                    this.price = (2 * value) / 3;
                }
                else if (Size == "Small") 
                {
                    this.price = value / 3;
                }
            }
        }

        public override string ToString()
        {
            return $"{Name} ({Size}) - {Price:f2} lv";
        }
    }
}
