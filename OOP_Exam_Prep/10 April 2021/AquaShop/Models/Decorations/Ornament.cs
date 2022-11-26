namespace AquaShop.Models.Decorations
{
    public class Ornament : Decoration
    {
        private const int ORNAMENT_CONFORT = 1;
        private const decimal ORNAMENT_PRICE = 5;
        public Ornament()
            : base(ORNAMENT_CONFORT, ORNAMENT_PRICE)
        {
        }
    }
}
