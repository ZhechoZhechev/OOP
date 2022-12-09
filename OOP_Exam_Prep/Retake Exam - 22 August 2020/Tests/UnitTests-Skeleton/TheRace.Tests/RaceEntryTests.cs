using NUnit.Framework;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private const string CarModel = "CarName";
        private const int CarHorsePower = 200;
        private const double CarCubicCentimeters = 3000;
        private const string DriverName = "DriverName";

        private UnitCar car;
        private UnitDriver driver;
        private RaceEntry race;

        [SetUp]
        public void Setup()
        {
            car = new UnitCar(CarModel, CarHorsePower, CarCubicCentimeters);
            driver = new UnitDriver(DriverName, car);
            race = new RaceEntry();
        }

        [Test]
        public void TestOne()
        {
            Assert.Pass();
        }
    }
}