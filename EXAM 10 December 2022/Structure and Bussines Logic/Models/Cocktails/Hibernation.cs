namespace ChristmasPastryShop.Models.Cocktails
{
    public class Hibernation : Cocktail
    {
        const double HIBERNATION_PRICE = 10.50;
        public Hibernation(string cocktailName, string size)
            : base(cocktailName, size, HIBERNATION_PRICE)
        {

        }
    }
}
