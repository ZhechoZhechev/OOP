namespace AquaShop.Models.Fish
{
    public class SaltwaterFish : Fish
    {
        private const int SALTWATERFISH_INITIAL_SIZE = 3;
        public SaltwaterFish(string name, string species, decimal price)
            : base(name, species, price)
        {
            Size = SALTWATERFISH_INITIAL_SIZE;
        }

        public override void Eat()
        {
            Size += 2;
        }
    }
}
