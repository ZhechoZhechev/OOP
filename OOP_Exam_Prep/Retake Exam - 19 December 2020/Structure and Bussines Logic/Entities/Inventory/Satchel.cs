namespace WarCroft.Entities.Inventory
{
    public class Satchel : Bag
    {
        private const int StachelCapacity = 20;
        public Satchel() 
            : base(StachelCapacity)
        {
        }
    }
}
