
namespace WildFarm.Models.Animlas
{
    using Interfaces;
    using WildFarm.Models.Foods;

    public abstract class Animal : IAnimal
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public int FoodEaten { get; set; }

        public Animal()
        {
            FoodEaten = 0;
        }
        protected Animal(string name, double weight) :this()
        {
            Name = name;
            Weight = weight;
        }

        protected abstract double WeightMultiplier { get;}
        public abstract string AskForFood();
        public virtual void Feed(Food food)
        {
            Weight += food.Quantity * WeightMultiplier;
            FoodEaten += food.Quantity;
        }
        public override string ToString()
        {
            return $"{GetType().Name} [{Name}, ";
        }
    }
}
