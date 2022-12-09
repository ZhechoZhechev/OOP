namespace WarCroft.Entities.Inventory
{
    public class Backpack : Bag
    {
        private const int BackpackCapacity = 100;
        public Backpack() : base(BackpackCapacity)
        {
        }
    }
}
