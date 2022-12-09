using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Computers.Tests
{
    public class Tests
    {
        private const string CompManufacturer = "test";
        private const string CompModel = "testModel";
        private const int CompPrice = 300;

        Computer computer;
        ComputerManager computerManager;

        [SetUp]
        public void Setup()
        {
            computer = new Computer(CompManufacturer, CompModel, CompPrice);
            computerManager = new ComputerManager();
        }

        [Test]
        public void Constructor_SetsPropertiesCorrectly()
        {
            computerManager.AddComputer(computer);

            Assert.IsTrue(computerManager.Computers != null
                && computerManager.Count == 1);
        }
        [Test]
        public void AddComputer_ThrowsForAlreadyExistingComputer()
        {
            computerManager.AddComputer(computer);

            Assert.Throws<ArgumentException>(() => computerManager.AddComputer(computer));
        }

        [Test]
        public void AddComputer_CompToAddCannotBeNull() 
        {
            Computer compToAdd = null;

            Assert.That(() =>
            {
                computerManager.AddComputer(compToAdd);
            }, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void AddComputer_AddsObjectToTheCollection()
        {
            computerManager.AddComputer(computer);

            Assert.AreEqual(computerManager.Count, 1);
            Assert.AreEqual(computerManager.Computers.Count, 1);
            Assert.AreSame(computerManager.Computers.First(), computer);
        }
        [Test]
        public void RemoveComputer_ErrorIfModelIsNull() 
        {

            Assert.That(() =>
            {
                computerManager.RemoveComputer(CompManufacturer, null);
            }, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void RemoveComputer_ErrorIfManifacturerIsNull() 
        {
            Assert.That(() =>
            {
                computerManager.RemoveComputer(null, CompModel);
            }, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void RemoveComputer_WorksCorrectly() 
        {
            computerManager.AddComputer(computer);

            Computer expected = computerManager.RemoveComputer(CompManufacturer, CompModel);

            Assert.AreSame(computer, expected);
            Assert.IsTrue(computerManager.Count == 0);
            Assert.IsTrue(computerManager.Computers.Count == 0);
        }

        [Test]
        public void GetComuter_ErrorIfModelIsNull() 
        {
            Assert.That(() =>
            {
                computerManager.GetComputer(null, CompModel);
            }, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GetComuter_ErrorIfManifacturerIsNull() 
        {
            Assert.That(() =>
            {
                computerManager.RemoveComputer(null, CompModel);
            }, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GetComuter_ErrorIfNonExistingComputer() 
        {
            Computer compToGet = new Computer("someMan", "someModel", 1000);
            computerManager.AddComputer(computer);

            Assert.That(() =>
            {
                computerManager.GetComputer(compToGet.Manufacturer, compToGet.Model);
            }, Throws.TypeOf<ArgumentException>()
            .With.Message.EqualTo("There is no computer with this manufacturer and model."));
        }

        [Test]
        public void GetComuter_WorksAsIntended() 
        {
            computerManager.AddComputer(computer);

            Computer expectedValue = computerManager.GetComputer(CompManufacturer, CompModel);

            Assert.AreEqual(expectedValue, computer);
        }

        [Test]
        public void GetComputersByManufacturer_ErrorIfManifacturerNull() 
        {
            Assert.That(() =>
            {
                computerManager.GetComputersByManufacturer(null);
            }, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GetComputersByManufacturer_ReturnsEmptyCollection()
        {
            computerManager.AddComputer(new Computer("second manufacturer", "second model", 1500));
            computerManager.AddComputer(new Computer("third manufacturer", "third model", 2000));
            List<Computer> computers = computerManager.GetComputersByManufacturer("non-existing manufacturer").ToList();
            Assert.IsEmpty(computers);
        }

        [Test]
        public void GetComputersByManufacturer_WorksProperly() 
        {
            Computer computer = new Computer(CompManufacturer, "Pravec", 10000);
            computerManager.AddComputer(this.computer);
            computerManager.AddComputer(computer);

            ICollection<Computer> expectedOutput = computerManager.GetComputersByManufacturer(CompManufacturer);
            ICollection<Computer> trueOutput = new List<Computer>() { this.computer, computer };

            CollectionAssert.AreEqual(expectedOutput, trueOutput);
        }
    }
}