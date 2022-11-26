
namespace Formula1.Models
{
    using System;

    using Models.Contracts;
    public abstract class FormulaOneCar : IFormulaOneCar
    {
        private string model;
        private int horsepower;
        private double engineDisplacement;

        protected FormulaOneCar(string model, int horsepower, double engineDisplacement)
        {
            Model = model;
            Horsepower = horsepower;
            EngineDisplacement = engineDisplacement;
        }

        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                    throw new ArgumentException($"Invalid car model: {value}.");

                this.model = value;
            }
        }

        public int Horsepower 
        {
            get => this.horsepower;
            private set 
            {
                if (value < 900 || value > 1050)
                    throw new ArgumentException($"Invalid car horsepower: {value}.");

                this.horsepower = value;
            }
        }

        public double EngineDisplacement 
        {
            get => this.engineDisplacement;
            private set 
            {
                if (value < 1.6 || value > 2.0)
                    throw new ArgumentException($"Invalid car engine displacement: {value}.");

                this.engineDisplacement = value;
            }
        }

        public double RaceScoreCalculator(int laps)
            => (this.engineDisplacement / this.horsepower) * laps;
 
    }
}
