
namespace RepairShop.Tests
{
    using System;
    using NUnit.Framework;

    public class Tests
    {
        public class RepairsShopTests
        {
            [Test]
            public void ConstructorSetsValuesProperly()
            {
                Garage garage = new Garage("Reno", 2);
                Assert.IsTrue(garage.Name == "Reno" && garage.MechanicsAvailable == 2
                    && garage.CarsInGarage == 0);
            }
            [TestCase(null)]
            [TestCase("")]
            public void NamePropertyReturnsErrorWithInvalidName(string name)
            {
                Assert.That(() =>
                {
                    Garage garage = new Garage(name, 2);
                }, Throws.TypeOf<ArgumentNullException>());
            }
            [TestCase(0)]
            [TestCase(-2)]
            public void NamePropertyReturnsErrorWithZeroAndNegativeMechsCount(int mechsAvailable)
            {
                Assert.That(() =>
                {
                    Garage garage = new Garage("name", mechsAvailable);
                }, Throws.TypeOf<ArgumentException>()
                .With.Message.EqualTo("At least one mechanic must work in the garage."));
            }
            [Test]
            public void AddCar_ThrowsExceptionWhenNoAvailableMechanic()
            {
                Car car1 = new Car("Reno", 2);
                Car car2 = new Car("Citroen", 3);
                Car car3 = new Car("Pegeout", 33);
                Garage garage = new Garage("garajche", 2);

                garage.AddCar(car1);
                garage.AddCar(car2);

                Assert.AreEqual(garage.CarsInGarage, 2);

                Assert.That(() =>
                {
                    garage.AddCar(car3);
                }, Throws.TypeOf<InvalidOperationException>()
                .With.Message.EqualTo("No mechanic available."));
            }
            [Test]
            public void FiXCar_IfCarNotExistInGarageThrowsException()
            {
                Garage garage = new Garage("garajche", 2);
                Car car1 = new Car("Reno", 2);
                string invalidCarModel = "BMW";

                garage.AddCar(car1);

                Assert.That(() =>
                {
                    garage.FixCar(invalidCarModel);
                }, Throws.TypeOf<InvalidOperationException>()
                .With.Message.EqualTo($"The car {invalidCarModel} doesn't exist."));
            }
            [Test]
            public void FiXCar_CarIsFixedProperly()
            {
                Garage garage = new Garage("garajche", 2);
                Car car1 = new Car("Reno", 2);
                string carToFixName = "Reno";

                garage.AddCar(car1);
                garage.FixCar(carToFixName);

                Assert.AreEqual(car1.NumberOfIssues, 0);
            }
            [Test]
            public void RemovedFixedCar_ExceptionWhenNoFixedCarsInGarage() 
            {
                Garage garage = new Garage("garajche", 2);
                Car car1 = new Car("Reno", 2);

                garage.AddCar(car1);

                Assert.That(() =>
                {
                    garage.RemoveFixedCar();
                }, Throws.TypeOf<InvalidOperationException>()
                .With.Message.EqualTo($"No fixed cars available."));
            }
            [Test]
            public void RemovedFixedCar_RemovesFixedCarsProperly()
            {
                Garage garage = new Garage("garajche", 2);
                Car car1 = new Car("Reno", 2);

                garage.AddCar(car1);
                garage.FixCar("Reno");
                garage.RemoveFixedCar();

                Assert.AreEqual(garage.CarsInGarage, 0);
            }
            [Test]
            public void Report_ReturnsTheCorrectMessage() 
            {
                Car car1 = new Car("Reno", 2);
                Car car2 = new Car("Citroen", 3);
                Garage garage = new Garage("garajche", 2);

                garage.AddCar(car1);
                garage.AddCar(car2);

                string expectedMessage = $"There are 2 which are not fixed: Reno, Citroen.";

                Assert.AreEqual(expectedMessage, garage.Report());
            }
        }
    }
}