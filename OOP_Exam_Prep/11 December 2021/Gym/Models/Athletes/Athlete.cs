using Gym.Models.Athletes.Contracts;
using Gym.Utilities.Messages;
using System;

namespace Gym.Models.Athletes
{
    public abstract class Athlete : IAthlete
    {
        private string fullname;
        private string monivation;
        private int numberOfMedals;

        public Athlete(string fullName, string motivation, int numberOfMedals, int stamina)
        {
            FullName = fullName;
            Motivation = motivation;
            NumberOfMedals = numberOfMedals;
            Stamina = stamina;
        }
        public string FullName 
        {
            get => this.fullname;
            private set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ExceptionMessages.InvalidAthleteName);

                this.fullname = value;
            }
        }

        public string Motivation 
        {
            get => this.monivation;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ExceptionMessages.InvalidAthleteMotivation);

                this.monivation = value;
            }
        }

        public int Stamina { get; protected set; }

        public int NumberOfMedals 
        {
            get => this.numberOfMedals;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(ExceptionMessages.InvalidAthleteMedals);

                this.numberOfMedals = value;
            }
        }

        public abstract void Exercise();
    }
}
