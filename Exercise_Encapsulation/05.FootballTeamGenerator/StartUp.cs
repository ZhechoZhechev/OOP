
namespace _05.FootballTeamGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        private static List<Team> listOfTeams;
        static void Main()
        {
            listOfTeams = new List<Team>();

            string input = string.Empty;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] commands = input.Split(";");
                string command = commands[0];
                string teamName = commands[1];

                try
                {
                    if (command == "Team")
                    {
                        Team newTeam = new Team(teamName);
                        listOfTeams.Add(newTeam);
                    }
                    else if (command == "Add")
                    {
                        var teamToJoin = listOfTeams.FirstOrDefault(x => x.Name == teamName);
                        if (teamToJoin == null)
                        {
                            throw new InvalidOperationException(string.Format(ExceptionMessages.NoSuchTeam, teamName));
                        }
                        var newPlayer = CreateNewPlayer(commands);
                        teamToJoin.AddPlayer(newPlayer);
                    }
                    else if (command == "Remove")
                    {
                        string playerName = commands[2];
                        var removingTeam = listOfTeams.FirstOrDefault(x => x.Name == teamName);
                        if (removingTeam == null)
                        {
                            throw new InvalidOperationException(string.Format(ExceptionMessages.NoSuchTeam, teamName));
                        }
                        removingTeam.RemovePlayer(playerName);
                    }
                    else if (command == "Rating")
                    {
                        Team teamToRate = listOfTeams.FirstOrDefault(x => x.Name == teamName);
                        if (teamToRate == null)
                        {
                            throw new InvalidOperationException(string.Format(
                        ExceptionMessages.NoSuchTeam, teamName));
                        }
                        Console.WriteLine(teamToRate);
                    }
                }
                catch (ArgumentException ae)
                {

                    Console.WriteLine(ae.Message);
                }
                catch (InvalidOperationException io)
                {
                    Console.WriteLine(io.Message);
                }

            }
        }
        static Player CreateNewPlayer(string[] commands)
        {
            string playerName = commands[2];
            int endurance = int.Parse(commands[3]);
            int sprint = int.Parse(commands[4]);
            int dribble = int.Parse(commands[5]);
            int passing = int.Parse(commands[6]);
            int shooting = int.Parse(commands[7]);
            //We will first validate stats and then player name
            //Stats stats = new Stats(endurance, sprint, dribble, passing, shooting);

            //We will first validate player name and then stats
            Player newPlayer = new Player(playerName, endurance, sprint, dribble, passing, shooting);
            return newPlayer;
        }
    }
}
