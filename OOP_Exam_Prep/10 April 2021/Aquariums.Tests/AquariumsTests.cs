namespace Aquariums.Tests
{
    using System;
    using NUnit.Framework;

    public class AquariumsTests
    {
        [Test]
        public void Fish_Constructor_CreatesObjectProperly()
        {
            Fish fish = new Fish("plavnik");

            Assert.AreEqual(fish.Name, "plavnik");
            Assert.AreEqual(fish.Available, true);
        }
        [Test]
        public void ConstructorSetPropsCorrectly() 
        {
            Aquarium aquarium = new Aquarium("test", 100);
            Assert.IsTrue(aquarium.Name == "test" && aquarium.Capacity == 100 
                && aquarium.Count == 0);
        }
        [TestCase(null)]
        [TestCase("")]
        public void NamePropReturnsErrorIfNullOrEmpty(string name) 
        {
            Assert.That(() =>
            {
                Aquarium aquarium = new Aquarium(name, 100);

            }, Throws.TypeOf<ArgumentNullException>());
            //.With.Message.EqualTo("Invalid aquarium name!"));
        }
        [Test]
        public void CapacityPropReturnsErrorIfNegative()
        {
            Assert.That(() =>
            {
                Aquarium aquarium = new Aquarium("test", -100);

            }, Throws.TypeOf<ArgumentException>()
            .With.Message.EqualTo("Invalid aquarium capacity!"));
        }
        [Test]
        public void Add_IncreasesCountByOne()
        {
            Fish fish1 = new Fish("ribcho");
            Aquarium aquarium = new Aquarium("test", 1);

            aquarium.Add(fish1);

            Assert.AreEqual(aquarium.Count, 1);
        }
        [Test]
        public void Add_ThrowExceptionIfAquariumFull() 
        {
            Fish fish1 = new Fish("ribcho");
            Fish fish2 = new Fish("pluvcho");
            Aquarium aquarium = new Aquarium("test", 1);

            aquarium.Add(fish1);

            Assert.That(() =>
            {
                aquarium.Add(fish2);

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo("Aquarium is full!"));

            Assert.AreEqual(aquarium.Count, 1);
        }

        [Test]
        public void RemoveFish_ThrowsExceptionIfFishDoesNotExist() 
        {
            Fish fish1 = new Fish("ribcho");
            Fish fish2 = new Fish("pluvcho");
            Aquarium aquarium = new Aquarium("test", 1);

            aquarium.Add(fish1);

            Assert.That(() =>
            {
                aquarium.RemoveFish("pluvcho");

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo($"Fish with the name {fish2.Name} doesn't exist!"));
        }
        [Test]
        public void RemoveFish_DecreasesCountByOne()
        {
            Fish fish1 = new Fish("ribcho");
            Fish fish2 = new Fish("pluvcho");
            Aquarium aquarium = new Aquarium("test", 50);

            aquarium.Add(fish1);
            aquarium.RemoveFish(fish1.Name);

            Assert.AreEqual(aquarium.Count, 0);
        }
        [Test]
        public void SellFish_ThrowsExceptionIfFishDoesNotExist()
        {
            Fish fish1 = new Fish("ribcho");
            Fish fish2 = new Fish("pluvcho");
            Aquarium aquarium = new Aquarium("test", 1);

            aquarium.Add(fish1);

            Assert.That(() =>
            {
                aquarium.SellFish(fish2.Name);

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo($"Fish with the name {fish2.Name} doesn't exist!"));

        }
        [Test]
        public void SellFish_WorksCorrectly() 
        {
            Fish fish1 = new Fish("ribcho");
            Fish fish2 = new Fish("pluvcho");
            Aquarium aquarium = new Aquarium("test", 1);

            aquarium.Add(fish1);

            aquarium.SellFish(fish1.Name);

            Assert.IsTrue(fish1.Available == false);
            Assert.AreSame(fish1, aquarium.SellFish(fish1.Name));
        }
        [Test]
        public void Report_IsWorkingCorrectly() 
        {
            Fish fish1 = new Fish("ribcho");
            Fish fish2 = new Fish("pluvcho");
            Aquarium aquarium = new Aquarium("test", 50);

            aquarium.Add(fish1);
            aquarium.Add(fish2);

            string expectedMessage = $"Fish available at {aquarium.Name}: ribcho, pluvcho";

            Assert.AreEqual(expectedMessage, aquarium.Report());
        }
    }
}
