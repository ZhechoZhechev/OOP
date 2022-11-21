namespace CarManager.Tests
{
    using System;
    using System.Reflection;
    using NUnit.Framework;

    [TestFixture]
    public class CarManagerTests
    {
        private const string CAR_MAKE = "Audi";
        private const string CAR_MODEL = "S6";
        private const double CAR_FUELCONSUMPTION = 8.0;
        private const double CAR_FUELCAPACITY = 75.0;
        private Car car;

        [SetUp]
        public void SetUp()
        {
            car = new Car(CAR_MAKE, CAR_MODEL, CAR_FUELCONSUMPTION, CAR_FUELCAPACITY);
        }
        [Test]
        public void ConstructorsShouldReturnProperValues()
        {
            Assert.That(0.0, Is.EqualTo(car.FuelAmount),
                "Cosntructor not setting FuelAmount to zero.");
            Assert.That(CAR_MAKE, Is.EqualTo(car.Make),
                "Cosntructor not setting Make prop corectly");
            Assert.That(CAR_MODEL, Is.EqualTo(car.Model),
                "Cosntructor not setting Model prop corectly");
            Assert.That(CAR_FUELCONSUMPTION, Is.EqualTo(car.FuelConsumption),
                "Cosntructor not setting FuelConsumption prop corectly");
            Assert.That(CAR_FUELCAPACITY, Is.EqualTo(car.FuelCapacity),
                "Cosntructor not setting FuelCapacity prop corectly");
        }
        [TestCase(null)]
        [TestCase("")]
        public void Make_CannotBeNullOrEmpty(string make)
        {
            Assert.That(() =>
            {

                Car car = new Car(make, CAR_MODEL, CAR_FUELCONSUMPTION, CAR_FUELCAPACITY);
            }, Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Make cannot be null or empty!"));
        }
        [TestCase(null)]
        [TestCase("")]
        public void Model_CannotBeNullOrEmpty(string model)
        {
            Assert.That(() =>
            {

                Car car = new Car(CAR_MAKE, model, CAR_FUELCONSUMPTION, CAR_FUELCAPACITY);
            }, Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Model cannot be null or empty!"));
        }
        [TestCase(0)]
        [TestCase(-22)]
        public void FuelConsumption_CannotBeZeroOrNegative(double consumption)
        {
            Assert.That(() =>
            {

                Car car = new Car(CAR_MAKE, CAR_MODEL, consumption, CAR_FUELCAPACITY);
            }, Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Fuel consumption cannot be zero or negative!"));
        }
        [Test]
        public void FuelAmount_CannotBecameNegative()
        {
            Assert.That(() =>
            {
                car.GetType().GetProperty(nameof(car.FuelAmount)).SetValue(car.FuelAmount, -22);

            }, Throws.TypeOf<TargetException>());
        }
        [TestCase(0)]
        [TestCase(-22)]
        public void FuelCapacity_CannotBeZeroOrNegative(double capacity)
        {
            Assert.That(() =>
            {

                Car car = new Car(CAR_MAKE, CAR_MODEL, CAR_FUELCONSUMPTION, capacity);
            }, Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Fuel capacity cannot be zero or negative!"));
        }
        [TestCase(0)]
        [TestCase(-22)]
        public void Cannot_Refuel_WithZeroOrNegativeAmount(double amount)
        {
            Assert.That(() =>
            {
                car.Refuel(amount);
            }, Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Fuel amount cannot be zero or negative!"));
        }
        [Test]
        public void Refuel_IfWrokingProperly()
        {
            double fuel = 80;
            car.Refuel(fuel);

            Assert.That(car.FuelAmount, Is.EqualTo(CAR_FUELCAPACITY), "Added fuel cannot exceed the fuel capacity.");

        }
        [Test]
        public void Drive_ExceptionIfNotEnoughFuel() 
        {
            Assert.That(() =>
            {
                car.Drive(1000);
            }, Throws.TypeOf<InvalidOperationException>().With.Message.EqualTo("You don't have enough fuel to drive!"));
        }
        [Test]
        public void Drive_IfItIsWorkingProperly() 
        {
            double amountToRefuel = 75;
            car.Refuel(amountToRefuel);

            double distanceToDrive = 50;
            double expectedFuelAmount = car.FuelAmount - ((distanceToDrive / 100) * car.FuelConsumption);
            car.Drive(distanceToDrive);

            Assert.That(car.FuelAmount, Is.EqualTo(expectedFuelAmount), "Fuel decreses with correct amount through Drive method.");
        }
    }
}