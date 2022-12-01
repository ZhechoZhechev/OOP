
namespace CarRacing.Repositories
{
    using System.Collections.Generic;
    using System;

    using Models.Cars.Contracts;
    using Contracts;
    using CarRacing.Utilities.Messages;

    public class CarRepository : IRepository<ICar>
    {
        private List<ICar> cars;

        public CarRepository()
        {
            cars = new List<ICar>();
        }

        public IReadOnlyCollection<ICar> Models => this.cars.AsReadOnly();

        public void Add(ICar model)
        {
            if (model == null)
                throw new ArgumentException(ExceptionMessages.InvalidAddCarRepository);

            cars.Add(model);
        }

        public ICar FindBy(string property)
        {
            return this.cars.Find(x => x.VIN == property);
        }

        public bool Remove(ICar model)
        {
            return cars.Remove(model);
        }
    }
}
