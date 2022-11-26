namespace AquaShop.Models.Aquariums
{
    class SaltwaterAquarium : Aquarium
    {
        private const int SALTWATERAQ_CAPACITY = 25;
        public SaltwaterAquarium(string name)
            : base(name, SALTWATERAQ_CAPACITY)
        {
        }
    }
}
