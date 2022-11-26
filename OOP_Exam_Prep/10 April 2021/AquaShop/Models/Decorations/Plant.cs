namespace AquaShop.Models.Decorations
{
    public class Plant : Decoration
    {
        private const int PLANT_CONFORT = 5;
        private const decimal PLANT_PRICE = 10;
        public Plant()
            : base(PLANT_CONFORT, PLANT_PRICE)
        {
        }
    }
}
