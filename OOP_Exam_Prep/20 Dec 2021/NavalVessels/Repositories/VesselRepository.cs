

namespace NavalVessels.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;

    public class VesselRepository : IRepository<IVessel>
    {
        private List<IVessel> vessels;

        public VesselRepository()
        {
            this.vessels = new List<IVessel>();
        }
        public IReadOnlyCollection<IVessel> Models => this.vessels.AsReadOnly();

        public void Add(IVessel model)
        {
            this.vessels.Add(model);
        }

        public IVessel FindByName(string name)
        {
           return this.vessels.Find(x => x.Name == name);
        }

        public bool Remove(IVessel model)
        {
            return this.vessels.Remove(model);
        }
    }
}
