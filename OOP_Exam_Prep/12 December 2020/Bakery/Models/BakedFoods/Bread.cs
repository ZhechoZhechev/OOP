namespace Bakery.Models.BakedFoods
{
    public class Bread : BakedFood
    {
        private const int BREAD_PORTION = 200;

        public Bread(string name, decimal price)
            : base(name, BREAD_PORTION, price)
        {
        }
    }
}
