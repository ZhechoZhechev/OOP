using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animlas
{
    public class Dog : Mammal
    {
        private const double DogWeightMultiplier = 0.40;
        public Dog(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion)
        {
        }

        protected override double WeightMultiplier => DogWeightMultiplier;

        public override string AskForFood()
        {
            return "Woof!";
        }
        public override void Feed(Food food)
        {
            if (food.GetType().Name == "Meat")
            {
                base.Feed(food);
            }
            else throw new InvalidOperationException($"{GetType().Name} does not eat {food.GetType().Name}!");
        }
        public override string ToString()
        {
            return base.ToString() + $"{Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
