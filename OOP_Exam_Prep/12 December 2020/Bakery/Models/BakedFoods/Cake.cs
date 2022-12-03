namespace Bakery.Models.BakedFoods
{
    public class Cake : BakedFood
    {
        private const int CAKE_PORTION = 245;

        public Cake(string name, decimal price)
            : base(name, CAKE_PORTION, price)
        {
        }
    }
}
