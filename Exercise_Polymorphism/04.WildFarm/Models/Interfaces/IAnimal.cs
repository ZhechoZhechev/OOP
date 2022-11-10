namespace WildFarm.Models.Interfaces
{
    using Foods;
    public interface IAnimal
    {
        string Name { get; }
        double Weight { get; }
        int FoodEaten { get; }

        string AskForFood();
        void Feed(Food food);
    }
}
