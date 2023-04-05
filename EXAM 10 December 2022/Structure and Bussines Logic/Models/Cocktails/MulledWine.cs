namespace ChristmasPastryShop.Models.Cocktails
{
    public class MulledWine : Cocktail
    {
        const double MULLED_WINE_PRICE = 13.50;
        public MulledWine(string cocktailName, string size)
            : base(cocktailName, size, MULLED_WINE_PRICE)
        {

        }
    }
}
