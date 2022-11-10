namespace WildFarm.Models.Animlas
{
    using System;

    using Foods;
    public class Hen : Bird
    {
        private const double HenWeightMultiplier = 0.35;
        public Hen(string name, double weight, double wingSize)
            : base(name, weight, wingSize)
        {
        }

        protected override double WeightMultiplier => HenWeightMultiplier;

        public override string AskForFood()
        {
            return "Cluck";
        }
        public override void Feed(Food food)
        {
            if (food.GetType().Name == "Meat" || food.GetType().Name == "Vegetable"
                || food.GetType().Name == "Fruit" || food.GetType().Name == "Seeds")
            {
                base.Feed(food);
            }
            else throw new InvalidOperationException($"{GetType().Name} does not eat {food.GetType().Name}!");
        }
    }
}
