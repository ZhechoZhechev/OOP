
namespace AquaShop.Repositories
{
    using Models.Decorations.Contracts;
    using Contracts;
    using System.Collections.Generic;

    public class DecorationRepository : IRepository<IDecoration>
    {
        private List<IDecoration> models;
        public DecorationRepository()
        {
            this.models = new List<IDecoration>();
        }
        public IReadOnlyCollection<IDecoration> Models => this.models.AsReadOnly();

        public void Add(IDecoration model) 
            => this.models.Add(model);

        public IDecoration FindByType(string type)
            => models.Find(x => x.GetType().Name == type);

        public bool Remove(IDecoration model) 
            => this.models.Remove(model);

    }
}
