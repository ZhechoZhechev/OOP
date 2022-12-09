using NUnit.Framework;
using System;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private const string CarModel = "testModel";
        private const int CarHorsePower = 300;
        private const double CarCubicCMS = 3000;
        private const string DriverName = "testDriver";
        private const int MinParticipants = 2;

        private UnitCar car;
        private UnitDriver driver;
        private RaceEntry race;


        [SetUp]
        public void Setup()
        {
            car = new UnitCar(CarModel, CarHorsePower, CarCubicCMS);
            driver = new UnitDriver(DriverName, car);
            race = new RaceEntry();
        }

        [Test]
        public void Constructor_WorsProperly()
        {
            race.AddDriver(driver);

            Assert.AreEqual(race.Counter, 1);
        }
        [Test]
        public void Add_ErrorIfDriverNull()
        {
            UnitDriver driver = null;

            Assert.That(() =>
            {
                race.AddDriver(driver);
            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo("Driver cannot be null."));
        }
        [Test]
        public void Add_ErrorIfDriverExisting() 
        {
            race.AddDriver(driver);

            Assert.That(() =>
            {
                race.AddDriver(driver);
            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo($"Driver {driver.Name} is already added."));
        }
        [Test]
        public void Add_WorksProperly()
        {
            string actualOutput = race.AddDriver(driver);
            string expectedOutput = string.Format("Driver {0} added in race.", DriverName);

            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }
        [Test]
        public void CallculateHP_ExceptionIfParticipantsBellowRequired()
        {
            race.AddDriver(driver);

            Assert.That(() =>
            {
                race.CalculateAverageHorsePower();
            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo($"The race cannot start with less than {MinParticipants} participants."));

        }
        [Test]
        public void CallculateHP_ReturnsTheCorrectNuber()
        {
            UnitCar carTwo = new UnitCar("VAZ", 300, 4500);
            UnitDriver driverTwo = new UnitDriver("VaskoLudiq", carTwo);

            race.AddDriver(driver);
            race.AddDriver(driverTwo);

            double expectedAverage = (driver.Car.HorsePower + driverTwo.Car.HorsePower) / 2;
            double actualAverage = race.CalculateAverageHorsePower();

            Assert.AreEqual(expectedAverage, actualAverage);
        }

    }
}