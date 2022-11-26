namespace AquaShop.Models.Fish
{
    public class FreshwaterFish : Fish
    {
        private const int FRESHWATERFISH_INITIAL_SIZE = 3;

        public FreshwaterFish(string name, string species, decimal price)
            : base(name, species, price)
        {
            Size = FRESHWATERFISH_INITIAL_SIZE;
        }

        public override void Eat()
        {
            Size += 3;
        }
    }
}
