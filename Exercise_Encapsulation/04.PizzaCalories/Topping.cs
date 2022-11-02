using System;
using System.Collections.Generic;
using System.Text;

namespace _04.PizzaCalories
{
   public class Topping
    {
        private const double MOD_MEAT = 1.2;
        private const double MOD_VEGGIES = 0.8;
        private const double MOD_CHEESE = 1.1;
        private const double MOD_SAUCE = 0.9;

        private string toppingType;
        private double toppingGrams;

        public Topping(string toppingType, double toppingGrams)
        {
            this.ToppingType = toppingType;
            this.ToppingGrams = toppingGrams;
        }

        private string ToppingType 
        {
            set 
            {
                if (value.ToLower() != "meat" && value.ToLower() != "veggies"
                    && value.ToLower() != "cheese" && value.ToLower() != "sauce")
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }
                this.toppingType = value;
            }
        }
        private double ToppingGrams 
        {
            set 
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{this.toppingType} weight should be in the range [1..50].");
                }
                this.toppingGrams = value;
            }
        }
        public double ToppingCaloriesPerGram 
        {
            get 
            {
                double calsPerGram = 2;

                if (this.toppingType.ToLower() == "meat") calsPerGram *= MOD_MEAT;
                else if (this.toppingType.ToLower() == "veggies") calsPerGram *= MOD_VEGGIES;
                else if (this.toppingType.ToLower() == "cheese") calsPerGram *= MOD_CHEESE;
                else if (this.toppingType.ToLower() == "sauce") calsPerGram *= MOD_SAUCE;
                return calsPerGram;
            }
        }
        public double GetToppingCalories() => this.toppingGrams * this.ToppingCaloriesPerGram;
    }
}
