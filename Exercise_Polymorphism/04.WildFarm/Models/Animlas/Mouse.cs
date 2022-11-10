namespace WildFarm.Models.Animlas
{
    using System;

    using Foods;

    public class Mouse : Mammal
    {
        private const double MouseWeightMultiplier = 0.10;
        public Mouse(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion)
        {
        }

        protected override double WeightMultiplier => MouseWeightMultiplier;

        public override string AskForFood()
        {
            return "Squeak";
        }
        public override void Feed(Food food)
        {
            if (food.GetType().Name == "Vegetable" || food.GetType().Name == "Fruit")
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
