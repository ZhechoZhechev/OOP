using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories.Contracts;
using System.Collections.Generic;

namespace ChristmasPastryShop.Repositories
{
    public class DelicacyRepository : IRepository<IDelicacy>
    {
        private List<IDelicacy> delicacies;

        public DelicacyRepository()
        {
            delicacies = new List<IDelicacy>();
        }
        public IReadOnlyCollection<IDelicacy> Models => this.delicacies.AsReadOnly();

        public void AddModel(IDelicacy model)
        {
            this.delicacies.Add(model);
        }
    }
}
