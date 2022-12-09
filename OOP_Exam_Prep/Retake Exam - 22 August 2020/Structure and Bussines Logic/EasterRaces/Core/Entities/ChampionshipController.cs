using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private DriverRepository drivers;
        private CarRepository cars;
        private RaceRepository races;

        public ChampionshipController()
        {
            this.drivers = new DriverRepository();
            this.cars = new CarRepository();
            this.races = new RaceRepository();
        }
        public string AddCarToDriver(string driverName, string carModel)
        {
            if (this.drivers.GetByName(driverName) == null)
                throw new InvalidOperationException(string.Format
                    (ExceptionMessages.DriverNotFound, driverName));

            if(this.cars.GetByName(carModel) == null)
                throw new InvalidOperationException(string.Format
                    (ExceptionMessages.CarNotFound, carModel));

            var carToAdd = this.cars.GetByName(carModel);
            var driverAddTo = this.drivers.GetByName(driverName);

            driverAddTo.AddCar(carToAdd);
            return string.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            var driverToAdd = this.drivers.GetByName(driverName);
            var raceToAddTo = this.races.GetByName(raceName);

            if(driverToAdd == null)
                throw new InvalidOperationException(string.Format
                    (ExceptionMessages.DriverNotFound, driverName));
            if(raceToAddTo == null)
                throw new InvalidOperationException(string.Format
                    (ExceptionMessages.RaceNotFound, raceName));

            raceToAddTo.AddDriver(driverToAdd);
            return string.Format(OutputMessages.DriverAdded, driverName, raceName);
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            if(this.cars.GetByName(model) != null)
                throw new ArgumentException(string.Format
                    (ExceptionMessages.CarExists, model));

            ICar carToCreate = null;
            switch (type)
            {
                case "Muscle": carToCreate = new MuscleCar(model, horsePower);
                    break;
                case "Sports": carToCreate = new SportsCar(model, horsePower);
                    break;
            }

            this.cars.Add(carToCreate);

            return string.Format(OutputMessages.CarCreated, carToCreate.GetType().Name, model);
        }

        public string CreateDriver(string driverName)
        {
            if (this.drivers.GetByName(driverName) != null)
                throw new ArgumentException(string.Format
                    (ExceptionMessages.DriversExists, driverName));

            Driver driverToAdd = new Driver(driverName);
            this.drivers.Add(driverToAdd);

            return string.Format(OutputMessages.DriverCreated, driverName);
        }

        public string CreateRace(string name, int laps)
        {

            if (this.races.GetByName(name) != null)
                throw new InvalidOperationException(string.Format
                    (ExceptionMessages.RaceExists, name));

            Race raceToCreate = new Race(name, laps);
            this.races.Add(raceToCreate);

            return string.Format(OutputMessages.RaceCreated, name);
        }

        public string StartRace(string raceName)
        {
            var targetedRace = this.races.GetByName(raceName);
            if (targetedRace == null)
                throw new InvalidOperationException(string.Format
                    (ExceptionMessages.RaceNotFound, raceName));

            if(targetedRace.Drivers.Count < 3)
                throw new InvalidOperationException(string.Format
                    (ExceptionMessages.RaceInvalid, raceName, 3));

            var sortedDriver = targetedRace.Drivers.OrderByDescending
                (x => x.Car.CalculateRacePoints(targetedRace.Laps)).Take(3);

            var winner = sortedDriver.First();
            var second = sortedDriver.Skip(1).First();
            var third = sortedDriver.Skip(2).First();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Driver {winner.Name} wins {raceName} race.")
                .AppendLine($"Driver {second.Name} is second in {raceName} race.")
                .AppendLine($"Driver {third.Name} is third in {raceName} race.");

            return sb.ToString().TrimEnd();
        }
    }
}
