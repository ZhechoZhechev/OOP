
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
    using Models.Pilots;
    using Models.Races;

    public class Controller : IController
    {
        private PilotRepository pilotrepository;
        private RaceRepository raceRepository;
        private FormulaOneCarRepository formulaOneCarRepository;

        public Controller()
        {
            this.pilotrepository = new PilotRepository();
            this.raceRepository = new RaceRepository();
            this.formulaOneCarRepository = new FormulaOneCarRepository();
        }

        public string CreatePilot(string fullName)
        {

            if (pilotrepository.Models.Any(x => x.FullName == fullName))
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));

            IPilot pilotToAdd = new Pilot(fullName);

            this.pilotrepository.Add(pilotToAdd);
            return $"Pilot {fullName} is created.";
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (this.formulaOneCarRepository.Models.Any(x => x.Model == model))
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

            this.formulaOneCarRepository.Add(car);

            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);

        }

        public string CreateRace(string raceName, int numberOfLaps)
        {

            if (raceRepository.Models.Any(x => x.RaceName == raceName))
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));

            IRace raceToAdd = new Race(raceName, numberOfLaps);
            this.raceRepository.Add(raceToAdd);

            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {

            if (!pilotrepository.Models.Any(x => x.FullName == pilotName) 
                || pilotrepository.Models.First(x => x.FullName == pilotName).CanRace)
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));

            if (!formulaOneCarRepository.Models.Any(x => x.Model == carModel))
                throw new NullReferenceException
                    (string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));

            IPilot pilotToRace = pilotrepository.Models.First(x => x.FullName == pilotName);
            IFormulaOneCar carToAdd = formulaOneCarRepository.Models.First(x => x.Model == carModel);
            pilotToRace.AddCar(carToAdd);
            formulaOneCarRepository.Remove(carToAdd);

            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, carToAdd.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            if (!raceRepository.Models.Any(x => x.RaceName == raceName))
                throw new NullReferenceException(String.Format(Utilities.ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            IRace raceToAddTo = raceRepository.Models.First(x => x.RaceName == raceName);

            if (!pilotrepository.Models.Any(x => x.FullName == pilotFullName))
                throw new InvalidOperationException(String.Format(Utilities.ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));

            IPilot pilotToAdd = pilotrepository.Models.First(x => x.FullName == pilotFullName);

            if ((!pilotToAdd.CanRace) ||
                raceToAddTo.Pilots.Any(x => x.FullName == pilotFullName))
            {
                throw new InvalidOperationException(String.Format(Utilities.ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }
            raceToAddTo.AddPilot(pilotToAdd);
            return String.Format(Utilities.OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string StartRace(string raceName)
        {

            if (!raceRepository.Models.Any(x => x.RaceName == raceName))
                throw new NullReferenceException
                    (string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));

            var raceToStart = raceRepository.FindByName(raceName);
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
