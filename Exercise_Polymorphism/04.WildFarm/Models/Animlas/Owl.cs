namespace WildFarm.Models.Animlas
{
    using System;
    using Foods;

    public class Owl : Bird
    {
        private const double OwlWeightMultiplier = 0.25;
        public Owl(string name, double weight, double wingSize)
            : base(name, weight, wingSize)
        {
        }

        protected override double WeightMultiplier => OwlWeightMultiplier;

        public override string AskForFood()
        {
            return "Hoot Hoot";
        }
        public override void Feed(Food food)
        {
            if (food.GetType().Name == "Meat") base.Feed(food);

            else throw new InvalidOperationException($"{GetType().Name} does not eat {food.GetType().Name}!");
        }
    }
}
