using EasterRaces.Models.Races.Contracts;
using EasterRaces.Repositories.Contracts;
using System.Collections.Generic;

namespace EasterRaces.Repositories.Entities
{
    public class RaceRepository : IRepository<IRace>
    {
        private List<IRace> races;

        public RaceRepository()
        {
            this.races = new List<IRace>();
        }
        public void Add(IRace model)
        {
            this.races.Add(model);
        }

        public IReadOnlyCollection<IRace> GetAll()
        {
            return this.races.AsReadOnly();
        }

        public IRace GetByName(string name)
        {
            var targetedRace = this.races.Find(x => x.Name == name);

            return targetedRace;
        }

        public bool Remove(IRace model)
        {
            throw new System.NotImplementedException();
        }
    }
}
