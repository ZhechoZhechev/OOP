namespace AquaShop.Models.Aquariums
{
    public class FreshwaterAquarium : Aquarium
    {
        private const int FRESHWATERAQ_CAPACITY = 50;
        public FreshwaterAquarium(string name)
            : base(name, FRESHWATERAQ_CAPACITY)
        {
        }
    }
}
