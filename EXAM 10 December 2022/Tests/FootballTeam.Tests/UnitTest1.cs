using NUnit.Framework;
using System;

namespace FootballTeam.Tests
{
    public class Tests
    {
        private const string PlayerName = "testName";
        private const int PlayerNumber = 8;
        private const string PlayerPossition = "Midfielder";

        private const string TeamName = "testTeamName";
        private const int TeamCapacity = 16;

        private FootballPlayer player;
        private FootballTeam team;

        [SetUp]
        public void Setup()
        {
            player = new FootballPlayer(PlayerName, PlayerNumber, PlayerPossition);
            team = new FootballTeam(TeamName, TeamCapacity);
            for (int i = 1; i <= 16; i++)
            {
                team.AddNewPlayer(new FootballPlayer("a", i, "Forward"));
            }
        }

        [Test]
        public void Constructor_SetValuesProperly()
        {
            Assert.IsTrue(team.Name == TeamName && team.Capacity == TeamCapacity && team.Players != null);
        }
        [TestCase(null)]
        [TestCase("")]
        public void Name_ThrowsIfNullOrEmpty(string name)
        {
            Assert.That(() =>
            {
                FootballTeam team = new FootballTeam(name, TeamCapacity);
            }, Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Name cannot be null or empty!"));

        }
        [Test]
        public void Capacity_ThrowsIfValueUnder15()
        {
            Assert.That(() =>
            {
                FootballTeam team = new FootballTeam(TeamName, 13);
            }, Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Capacity min value = 15"));
        }
        [Test]
        public void AddNewPlayer_MessageIfPlayersCountGreaterOrEqualToCapacity()
        {
            string expected = "No more positions available!";

            Assert.That(team.AddNewPlayer(player), Is.EqualTo(expected));
        }
        [Test]
        public void AddNewPlayer_ReturnsCorrectMessage()
        {
            FootballTeam team = new FootballTeam(TeamName, 15);
            string expected = $"Added player {player.Name} in position {player.Position} with number {player.PlayerNumber}";

            Assert.That(team.AddNewPlayer(player), Is.EqualTo(expected));
        }
        [Test]
        public void PickPlayer_ReturnsCorrctName()
        {
            FootballTeam team = new FootballTeam(TeamName, 15);
            team.AddNewPlayer(player);


            Assert.IsTrue(player == team.PickPlayer(player.Name));
        }
        [Test]
        public void PlayerScore_IncreasesPlayerGoalsWithOne()
        {
            FootballTeam team = new FootballTeam(TeamName, 15);
            team.AddNewPlayer(player);

            string actualOutput = team.PlayerScore(PlayerNumber);
            var expectedOutput = $"{player.Name} scored and now has 1 for this season!";

            Assert.AreEqual(actualOutput, expectedOutput);
        }



    }
}