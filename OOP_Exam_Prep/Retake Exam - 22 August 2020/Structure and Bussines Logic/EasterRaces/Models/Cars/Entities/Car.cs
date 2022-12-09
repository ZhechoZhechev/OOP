using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Utilities.Messages;
using System;

namespace EasterRaces.Models.Cars.Entities
{
    public abstract class Car : ICar
    {
        private string model;
        private int horsePower;
        private int minHorsePower;
        private int maxHorsePower;

        public Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            Model = model;
            this.minHorsePower = minHorsePower;
            this.maxHorsePower = maxHorsePower;
            HorsePower = horsePower;
            CubicCentimeters = cubicCentimeters;
        }

        public string Model 
        {
            get => this.model;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidModel, value, 4));

                this.model = value;
            }
        }

        public virtual int HorsePower
        {
            get => this.horsePower;
            private set
            {
                if (value < minHorsePower || value > maxHorsePower)
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower, value));

                this.horsePower = value;
            }
        }
        public double CubicCentimeters { get; private set; }

        public double CalculateRacePoints(int laps)
        {
            return CubicCentimeters / HorsePower * laps;
        }
    }
}
