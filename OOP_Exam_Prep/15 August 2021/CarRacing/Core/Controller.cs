using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using CarRacing.Utilities.Messages;
using System;
using System.Linq;

namespace CarRacing.Core
{
    public class Controller : IController
    {
        private CarRepository cars;
        private RacerRepository racers;
        private IMap map;
        public Controller()
        {
            this.cars = new CarRepository();
            this.racers = new RacerRepository();
            this.map = new Map();
        }
        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            ICar carToAdd;
            switch (type)
            {
                case "SuperCar": carToAdd = new SuperCar(make, model, VIN, horsePower);
                    break;
                case "TunedCar": carToAdd = new TunedCar(make, model, VIN, horsePower);
                    break;
                default: throw new ArgumentException(ExceptionMessages.InvalidCarType);
            }

            this.cars.Add(carToAdd);
            return string.Format(OutputMessages.SuccessfullyAddedCar, make, model, VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            var racersCar = this.cars.FindBy(carVIN);

            if (racersCar == null)
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);

            IRacer racerToAdd;
            switch (type)
            {
                case "ProfessionalRacer": racerToAdd = new ProfessionalRacer(username, racersCar);
                    break;
                case "StreetRacer": racerToAdd = new StreetRacer(username, racersCar);
                    break;
                default: throw new ArgumentException(ExceptionMessages.InvalidRacerType);
            }

            this.racers.Add(racerToAdd);
            return string.Format(OutputMessages.SuccessfullyAddedRacer, racerToAdd.Username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            var racerOne = racers.FindBy(racerOneUsername);
            var racerTwo = racers.FindBy(racerTwoUsername);

            if (racerOne == null)
                throw new ArgumentException
                    (string.Format(ExceptionMessages.RacerCannotBeFound, racerOneUsername));
            if (racerTwo == null)
                throw new ArgumentException
                    (string.Format(ExceptionMessages.RacerCannotBeFound, racerTwoUsername));
            
            return this.map.StartRace(racerOne, racerTwo);
        }

        public string Report()
        {
            var modelsSorted = this.racers.Models.OrderByDescending(x => x.DrivingExperience)
                .ThenBy(z => z.Username);

            return string.Join(Environment.NewLine, modelsSorted);
        }
    }
}
