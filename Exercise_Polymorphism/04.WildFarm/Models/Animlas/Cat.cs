namespace WildFarm.Models.Animlas
{
    using System;
    using Foods;

    public class Cat : Feline
    {
        private const double CatWeightMultiplier = 0.30;
        public Cat(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed)
        {

        }

        protected override double WeightMultiplier => CatWeightMultiplier;

        public override string AskForFood()
        {
            return "Meow";
        }
        public override void Feed(Food food)
        {
            if (food.GetType().Name == "Meat" || food.GetType().Name == "Vegetable")
            {
                base.Feed(food);
            }
            else new InvalidOperationException($"{GetType().Name} does not eat {food.GetType().Name}!");
        }
    }
}
