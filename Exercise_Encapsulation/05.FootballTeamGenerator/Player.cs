namespace _05.FootballTeamGenerator
{
    using System;
    public class Player
    {
        private string name;
        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            Name = name;
            Stats = new Stats(endurance, sprint, dribble, passing, shooting);
        }
        public string Name
        {
            get => this.name;
            set
            {
                if (string.IsNullOrWhiteSpace(value)|| string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.EmptyNameMessage);
                }
                this.name = value;
            }
        }
        public Stats Stats { get; private set; }

        public double PlayerOverallRating => (Stats.Endurance + Stats.Sprint + Stats.Dribble
            + Stats.Passing + Stats.Shooting) / 5.0;
    }
}
