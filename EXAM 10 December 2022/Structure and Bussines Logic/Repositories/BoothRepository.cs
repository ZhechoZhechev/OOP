using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Repositories.Contracts;
using System.Collections.Generic;

namespace ChristmasPastryShop.Repositories
{
    public class BoothRepository : IRepository<IBooth>
    {
        private List<IBooth> booths;

        public BoothRepository()
        {
            this.booths = new List<IBooth>();
        }
        public IReadOnlyCollection<IBooth> Models => this.booths.AsReadOnly();

        public void AddModel(IBooth model)
        {
            this.booths.Add(model);
        }
    }
}
