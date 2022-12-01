
namespace CarRacing.Models.Racers
{
    using CarRacing.Models.Cars.Contracts;
    using CarRacing.Utilities.Messages;
    using Contracts;
    using System;

    public abstract class Racer : IRacer
    {
        private string username;
        private string racingBehavior;
        private int drivingExperience;
        private ICar car;

        public Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {
            Username = username;
            RacingBehavior = racingBehavior;
            DrivingExperience = drivingExperience;
            Car = car;
        }
        public string Username 
        {
            get => this.username;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.InvalidRacerName);

                this.username = value;
            }
        }

        public string RacingBehavior
        {
            get => this.racingBehavior;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.InvalidRacerBehavior);

                this.racingBehavior = value;
            }
        }

        public int DrivingExperience
        {
            get => this.drivingExperience;
            protected set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException(ExceptionMessages.InvalidRacerDrivingExperience);

                this.drivingExperience = value;
            }
        }
        public ICar Car
        {
            get => this.car;
            private set
            {
                if (value == null)
                    throw new ArgumentException(ExceptionMessages.InvalidRacerCar);

                this.car = value;
            }
        }

        public bool IsAvailable()
        {
            return car.FuelAvailable >= car.FuelConsumptionPerRace;
        }

        public virtual void Race()
        {
            this.car.Drive();
        }
    }
}
