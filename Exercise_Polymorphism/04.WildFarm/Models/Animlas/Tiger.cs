namespace WildFarm.Models.Animlas
{
    using System;

    using Foods;

    public class Tiger : Feline
    {
        private const double TigerWeightMultiplier = 1.00;
        public Tiger(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed)
        {
        }

        protected override double WeightMultiplier => TigerWeightMultiplier;

        public override string AskForFood()
        {
            return "ROAR!!!";
        }
        public override void Feed(Food food)
        {
            if (food.GetType().Name == "Meat") base.Feed(food);

            else throw new InvalidOperationException($"{GetType().Name} does not eat {food.GetType().Name}!");
        }
    }
}
