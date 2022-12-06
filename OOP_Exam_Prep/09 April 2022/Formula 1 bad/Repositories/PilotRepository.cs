
namespace Formula1.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;

    public class PilotRepository : IRepository<IPilot>
    {
        private List<IPilot> pilots;
        public PilotRepository()
        {
            this.pilots = new List<IPilot>();
        }
        public IReadOnlyCollection<IPilot> Models => this.pilots.AsReadOnly();

        public void Add(IPilot model)
        {
            this.pilots.Add(model);
        }

        public IPilot FindByName(string name)
        {
            return this.pilots.Find(x => x.FullName == name);
        }

        public bool Remove(IPilot model)
        {
           return this.pilots.Remove(model);
        }
    }
}
