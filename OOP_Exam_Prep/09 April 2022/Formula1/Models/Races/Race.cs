
namespace Formula1.Models.Races
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    using Contracts;

    public class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;
        private readonly ICollection<IPilot> pilots;

        public Race(string raceName, int numberOfLaps)
        {
            RaceName = raceName;
            NumberOfLaps = numberOfLaps;
            TookPlace = false;
            pilots = new List<IPilot>();
        }
        public string RaceName 
        {
            get => this.raceName;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                    throw new ArgumentException($"Invalid race name: {value}.");

                this.raceName = value;
            }
        }

        public int NumberOfLaps 
        {
            get => this.numberOfLaps;
            private set 
            {
                if (value < 1)
                    throw new ArgumentException($"Invalid lap numbers: {value}.");

                this.numberOfLaps = value;
            }
        }

        public bool TookPlace { get; set; }

        public ICollection<IPilot> Pilots => this.pilots;

        public void AddPilot(IPilot pilot)
        {
            this.pilots.Add(pilot);
        }

        public string RaceInfo()
        {
            StringBuilder sb = new StringBuilder();
            string tookPlace = TookPlace ? "Yes" : "No";

            sb.AppendLine($"The {RaceName} race has:")
                .AppendLine($"Participants: {Pilots.Count}")
                .AppendLine($"Number of laps: {NumberOfLaps}")
                .AppendLine(tookPlace);

            return sb.ToString().TrimEnd();
        }
    }
}
