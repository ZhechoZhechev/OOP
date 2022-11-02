
namespace _04.PizzaCalories
{
    using System;
    using System.Collections.Generic;
    public class Pizza
    {
        private string name;
        private List<Topping> toppings;
        private Dough dough;
        public Pizza(string name, Dough dough)
        {
            Name = name;
            Dough = dough;
            this.toppings = new List<Topping>();

        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)
                    || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }
                this.name = value;
            }
        }
        public Dough Dough { get => this.dough; set => this.dough = value; }
        public int ToppingsCount => this.toppings.Count;
        public double Totalcalories 
        {
            get 
            {
                double totalCalories = this.dough.GetDoughCalories();
                foreach (var topping in toppings)
                {
                    totalCalories += topping.GetToppingCalories();
                }
                return totalCalories;
            }
        }
        public void AddTopping(Topping topping) 
        {
            if (ToppingsCount == 10) throw new ArgumentException("Number of toppings should be in range [0..10].");
            else this.toppings.Add(topping);

        }
        public override string ToString()
        {
            return $"{Name} - {Totalcalories:f2} Calories.";
        }
    }
}
