using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;
using System.Collections.Generic;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : IRepository<ICar>
    {
        private List<ICar> cars;

        public CarRepository()
        {
            this.cars = new List<ICar>();
        }
        public void Add(ICar model)
        {
            this.cars.Add(model);
        }

        public IReadOnlyCollection<ICar> GetAll()
        {
            return this.cars.AsReadOnly();
        }

        public ICar GetByName(string name)
        {
            var targetCar = this.cars.Find(x => x.Model == name);

            return targetCar;
        }

        public bool Remove(ICar model)
        {
           return this.cars.Remove(model);
        }
    }
}
