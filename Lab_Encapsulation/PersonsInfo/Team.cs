using System;
using System.Collections.Generic;

namespace PersonsInfo
{
    public class Team
    {
        private string name;
        private List<Person> firstTeam;
        private List<Person> reserveTeam;

        public Team(string name)
        {
            this.firstTeam = new List<Person>();
            this.reserveTeam = new List<Person>();
            this.name = name;
        }
        public IReadOnlyCollection<Person> FirstTeam { get => this.firstTeam.AsReadOnly(); }
        public IReadOnlyCollection<Person> ReserveTeam { get => this.reserveTeam.AsReadOnly(); }

        public void AddPlayer(Person person)
        {
            if (person.Age < 40)
            {
                firstTeam.Add(person);
            }
            else
            {
                reserveTeam.Add(person);
            }
        }
        public override string ToString()
        {
            return $"First team has {firstTeam.Count} players.{Environment.NewLine}Reserve team has {reserveTeam.Count} players.";
        }

    }
}
