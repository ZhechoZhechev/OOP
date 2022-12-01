namespace PlanetWars.Repositories
{
    using Contracts;
    using Models.MilitaryUnits.Contracts;
    using System.Collections.Generic;

    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private List<IMilitaryUnit> militaryUnits;
        public UnitRepository()
        {
            this.militaryUnits = new List<IMilitaryUnit>();
        }
        public IReadOnlyCollection<IMilitaryUnit> Models => this.militaryUnits.AsReadOnly();

        public void AddItem(IMilitaryUnit model)
        {
            this.militaryUnits.Add(model);
        }

        public IMilitaryUnit FindByName(string name)
        {
            return this.militaryUnits.Find(x => x.GetType().Name == name);
        }

        public bool RemoveItem(string name)
        {
            IMilitaryUnit unitToremove = this.militaryUnits.Find(x => x.GetType().Name == name);
            return this.militaryUnits.Remove(unitToremove);
        }
    }
}
