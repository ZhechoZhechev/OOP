
namespace PlanetWars.Repositories
{
    using System.Collections.Generic;

    using Contracts;
    using Models.Weapons.Contracts;

    public class WeaponRepository : IRepository<IWeapon>
    {
        private List<IWeapon> weapons;

        public WeaponRepository()
        {
            this.weapons = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models => this.weapons.AsReadOnly();

        public void AddItem(IWeapon model)
        {
            this.weapons.Add(model);
        }

        public IWeapon FindByName(string name)
        {
             return this.weapons.Find(x => x.GetType().Name == name);
        }

        public bool RemoveItem(string name)
        {
            IWeapon weapon = this.weapons.Find(x => x.GetType().Name == name);
            return this.weapons.Remove(weapon);
        }
    }
}
