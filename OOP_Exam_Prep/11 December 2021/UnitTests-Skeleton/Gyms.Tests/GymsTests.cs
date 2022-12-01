namespace Gyms.Tests
{
    using System;
    using NUnit.Framework;
    public class GymsTests
    {
        private const string ATHLETE_NAME = "Venata";
        private const string GYM_NAME = "Anabolika";
        private const int GYM_CAPACITY = 35;

        private Athlete athlete;
        private Gym gym;
        [Test]
        public void Athlete_Constructor_CreatesObjectProperly()
        {
            Assert.True(athlete.FullName == ATHLETE_NAME);
            Assert.True(!athlete.IsInjured);
        }

        [SetUp]
        public void SetUp()
        {
            athlete = new Athlete(ATHLETE_NAME);
            gym = new Gym(GYM_NAME, GYM_CAPACITY);
        }
        [Test]
        public void ConstructorSetUpPropertiesCorrectly()
        {
            gym.AddAthlete(athlete);

            Assert.IsTrue(gym.Name == GYM_NAME && gym.Capacity == GYM_CAPACITY);
            Assert.IsTrue(gym.Count == 1);
        }
        [TestCase(null)]
        [TestCase("")]
        public void GymNameThrowsExeptionWhenNullOrEmpty(string name)
        {
            Assert.That(() =>
            {
                Gym gym = new Gym(name, GYM_CAPACITY);

            }, Throws.TypeOf<ArgumentNullException>());
        }
        [Test]
        public void CapcityThrowsExceptionWhenBellowZero()
        {
            Assert.That(() =>
            {
                Gym gym = new Gym(GYM_NAME, -2);

            }, Throws.TypeOf<ArgumentException>()
            .With.Message.EqualTo("Invalid gym capacity."));
        }
        [Test]
        public void AddAthlete_ExceptionWhenGymIsFull()
        {
            Gym gym = new Gym(GYM_NAME, 1);
            Athlete athlete2 = new Athlete("Pesho-Zoba");
            gym.AddAthlete(athlete);

            Assert.That(() =>
            {
                gym.AddAthlete(athlete2);

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo("The gym is full."));
        }
        [Test]
        public void AddAthlete_IncreasesGetterCount()
        {
            gym.AddAthlete(athlete);
            Assert.AreEqual(gym.Count, 1);
        }
        [Test]
        public void RemoveAthlete_ExceptionIfAthleteDoesNotExist()
        {
            Assert.That(() =>
            {
                gym.RemoveAthlete(athlete.FullName);

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo($"The athlete {athlete.FullName} doesn't exist."));
        }
        [Test]
        public void RemoveAthlete_WorksAsIntended()
        {
            gym.AddAthlete(athlete);
            Assert.IsTrue(gym.Count == 1);
            gym.RemoveAthlete(athlete.FullName);
            Assert.IsTrue(gym.Count == 0);
        }
        [Test]
        public void InjureAthlete_ExceptionWhenAthleteDoesNotExist()
        {
            gym.AddAthlete(athlete);

            Assert.That(() =>
            {
                gym.InjureAthlete("Gosho");

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo($"The athlete Gosho doesn't exist."));
        }
        [Test]
        public void InjureAthlete_WorksAsIntended()
        {
            gym.AddAthlete(athlete);
            
            Athlete injAth = gym.InjureAthlete(athlete.FullName);

            Assert.IsTrue(athlete.IsInjured && injAth.FullName == ATHLETE_NAME);
        }
        [Test]
        public void Report_ReturnsCorrectMessage()
        {
            Athlete athlete1 = new Athlete("Pesho-Dymbela");

            gym.AddAthlete(athlete);
            gym.AddAthlete(athlete1);

            string expectedMessage = $"Active athletes at {gym.Name}: {athlete.FullName}, {athlete1.FullName}";

            Assert.AreEqual(gym.Report(), expectedMessage);
        }
    }
}
