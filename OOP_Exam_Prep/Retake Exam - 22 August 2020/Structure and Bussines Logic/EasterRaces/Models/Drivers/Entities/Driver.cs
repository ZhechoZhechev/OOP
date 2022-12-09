using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Utilities.Messages;
using System;

namespace EasterRaces.Models.Drivers.Entities
{
    public class Driver : IDriver
    {
        private string name;
        private ICar car;

        public Driver(string name)
        {
            Name = name;
            CanParticipate = false;
        }
        public string Name 
        {
            get => this.name;
            private set 
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidName, value, 5));

                this.name = value;
            }
        }

        public ICar Car
        {
            get => this.car;
            private set
            {
                if (value == null)
                    throw new ArgumentException(string.Format(ExceptionMessages.CarInvalid));

                this.car = value;
            }
        }
        public int NumberOfWins { get; private set; }

        public bool CanParticipate { get; private set; }

        public void AddCar(ICar car)
        {
            Car = car;
            CanParticipate = true;
        }

        public void WinRace()
        {
            NumberOfWins++;
        }
    }
}
