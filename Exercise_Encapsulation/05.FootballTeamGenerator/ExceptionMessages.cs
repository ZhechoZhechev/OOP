using System;
using System.Collections.Generic;
using System.Text;

namespace _05.FootballTeamGenerator
{
    public static class ExceptionMessages
    {
        public const string InvalidStatMessage = "{0} should be between 0 and 100.";
        public const string EmptyNameMessage = "A name should not be empty.";
        public const string MissingPlayerName = "Player {0} is not in {1} team.";
        public const string NoSuchTeam = "Team {0} does not exist.";
    }
}
