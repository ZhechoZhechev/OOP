namespace BirthdayCelebrations.Models.Interfaces
{
   public interface IBuyer
    {
        int Food { get; }
        string Name { get; }
        int Age { get; }
        void BuyFood();
    }
}
