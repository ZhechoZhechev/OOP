
namespace Formula1.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Utilities;
    using Contracts;
    using Repositories;
    using Models.Contracts;
    using Models;

    public class Controller : IController
    {
        private PilotRepository pilotrepository;
        private RaceRepository raceRepository;
        private FormulaOneCarRepository FormulaOneCarRepository;

        public Controller()
        {
            this.pilotrepository = new PilotRepository();
            this.raceRepository = new RaceRepository();
            this.FormulaOneCarRepository = new FormulaOneCarRepository();
        }

        public string CreatePilot(string fullName)
        {
            var pilotToAdd = pilotrepository.FindByName(fullName);

            if (pilotToAdd != null)
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));

            this.pilotrepository.Add(pilotToAdd);
            return $"Pilot {fullName} is created.";
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (this.FormulaOneCarRepository.Models.Any(x => x.Model == model))
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.CarExistErrorMessage, model));

            IFormulaOneCar car;
            switch (type)
            {
                case "Ferrari": car = new Ferrari(model, horsepower, engineDisplacement);
                    break;
                case "Williams": car = new Williams(model, horsepower, engineDisplacement);
                    break;
                default: throw new InvalidOperationException
                        (string.Format(ExceptionMessages.InvalidTypeCar, type));
            }

            this.FormulaOneCarRepository.Add(car);

            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);

        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            var raceToAdd = this.raceRepository.FindByName(raceName);

            if (raceToAdd != null)
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));

            this.raceRepository.Add(raceToAdd);

            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            var pilotToAddCarTo = pilotrepository.FindByName(pilotName);
            var carToAdd = FormulaOneCarRepository.FindByName(carModel);

            if (pilotToAddCarTo == null || pilotToAddCarTo.CanRace == true)
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));

            if (carToAdd == null)
                throw new NullReferenceException
                    (string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));

            pilotToAddCarTo.AddCar(carToAdd);
            FormulaOneCarRepository.Remove(carToAdd);

            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, carToAdd.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            var raceToAddTo = raceRepository.FindByName(raceName);
            var pilotToAdd = pilotrepository.FindByName(pilotFullName);

            if (raceToAddTo == null)
                throw new NullReferenceException
                    (string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));

            if (pilotToAdd == null || pilotToAdd.CanRace == false
                || raceToAddTo.Pilots.Any(x => x.FullName == pilotFullName))
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            
            raceToAddTo.AddPilot(pilotToAdd);

            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string StartRace(string raceName)
        {
            var raceToStart = raceRepository.FindByName(raceName);

            if (raceToStart == null)
                throw new NullReferenceException
                    (string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));

            if (raceToStart.Pilots.Count < 3)
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));

            if(raceToStart.TookPlace == true)
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));

            raceToStart.TookPlace = true;
            int laps = raceToStart.NumberOfLaps;

            var pilotsSorted = raceToStart.Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(laps));

            var firstPilot = pilotsSorted.First();
            var secondPilot = pilotsSorted.Skip(1).First();
            var thirdPilot = pilotsSorted.Skip(2).First();

            firstPilot.WinRace();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Pilot {firstPilot.FullName} wins the {raceName} race.")
                .AppendLine($"Pilot {secondPilot.FullName} is second in the {raceName} race.")
                .AppendLine($"Pilot {thirdPilot.FullName} is third in the {raceName} race.");

            return sb.ToString().TrimEnd();
        }

        public string PilotReport()
        {
            var orderedPilots = pilotrepository.Models.OrderByDescending(x => x.NumberOfWins);
            return String.Join(Environment.NewLine, orderedPilots);
        }

        public string RaceReport()
        {
            var filteredRaces = raceRepository.Models.Where(x => x.TookPlace);
            return String.Join(Environment.NewLine, filteredRaces.Select(x => x.RaceInfo()));
        }

    }
}
