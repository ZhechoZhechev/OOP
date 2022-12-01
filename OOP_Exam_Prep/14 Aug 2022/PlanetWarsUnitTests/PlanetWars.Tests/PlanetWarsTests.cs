using NUnit.Framework;
using System;
using System.Linq;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            [Test]
            public void Weapon_COnstructorWorksCorrectly()
            {
                Weapon weapon = new Weapon("test", 300, 9);

                Assert.IsTrue(weapon.Name == "test" && weapon.Price == 300
                    && weapon.DestructionLevel == 9);
            }
            [Test]
            public void Price_ThrowsExceptionWhenBellowZero()
            {
                Assert.That(() =>
                {
                    Weapon weapon = new Weapon("test", -300, 9);
                }, Throws.TypeOf<ArgumentException>()
                .With.Message.EqualTo("Price cannot be negative."));
            }
            [Test]
            public void IncreaseDestructionLevel_IncresesByOne()
            {
                Weapon weapon = new Weapon("test", 300, 4);

                weapon.IncreaseDestructionLevel();

                Assert.AreEqual(weapon.DestructionLevel, 5);
            }
            [TestCase(9)]
            [TestCase(100)]
            public void IsNuclear_IfItworksCorrectly(int destructionLevel)
            {
                Weapon weapon = new Weapon("Nuc", 100, destructionLevel);

                bool testNuclear = destructionLevel >= 10;

                Assert.AreEqual(weapon.IsNuclear, testNuclear);
            }
            [Test]
            public void Planet_ConstructorWorksCorrectly()
            {
                Planet planet = new Planet("testP", 400);
                Weapon weapon = new Weapon("testW", 100, 9);

                Assert.True(planet.Name == "testP" && planet.Budget == 400
                    && planet.Weapons.Count == 0);
            }
            [TestCase(null)]
            [TestCase("")]
            public void Name_ThrowsCorrectError(string name)
            {
                Assert.That(() =>
                {
                    Planet planet = new Planet(name, 400);

                }, Throws.TypeOf<ArgumentException>()
                .With.Message.EqualTo("Invalid planet Name"));
            }
            [Test]
            public void Budget_ThrowsCorrectError()
            {
                Assert.That(() =>
                {
                    Planet planet = new Planet("testP", -400);

                }, Throws.TypeOf<ArgumentException>()
              .With.Message.EqualTo("Budget cannot drop below Zero!"));
            }
            [Test]
            public void MilitaryPowerRatio_WorksCorrectly()
            {
                Planet planet = new Planet("testP", 400);
                Weapon weapon1 = new Weapon("testW1", 100, 9);
                Weapon weapon2 = new Weapon("testW2", 100, 10);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                int expectedValue = weapon1.DestructionLevel + weapon2.DestructionLevel;

                Assert.AreEqual(planet.MilitaryPowerRatio, expectedValue);
            }
            [TestCase(300)]
            [TestCase(400)]
            public void Profit_WorksAsIntended(double budget)
            {
                Planet planet = new Planet("testP", budget);

                planet.Profit(30);

                double expectedBudget = budget + 30;

                Assert.AreEqual(planet.Budget, expectedBudget);
            }
            [Test]
            public void SpendFunds_ThrowsExceptionWhenValueBiggerThanBudget()
            {
                Planet planet = new Planet("testP", 400);
                double amountToSpend = 500;

                Assert.That(() =>
                {
                    planet.SpendFunds(amountToSpend);

                }, Throws.TypeOf<InvalidOperationException>()
                .With.Message.EqualTo("Not enough funds to finalize the deal."));

            }
            [Test]
            public void SpendFunds_WorksAsIntended()
            {
                Planet planet = new Planet("testP", 400);
                double amountToSpend = 50;
                double expectedValue = planet.Budget - amountToSpend;

                planet.SpendFunds(amountToSpend);

                Assert.AreEqual(planet.Budget, expectedValue);
            }
            [Test]
            public void AddWeapon_CanNotAddTheSameWeapon()
            {
                Planet planet = new Planet("testP", 400);
                Weapon weapon1 = new Weapon("testW1", 100, 9);

                planet.AddWeapon(weapon1);

                Assert.That(() =>
                {
                    planet.AddWeapon(weapon1);

                }, Throws.TypeOf<InvalidOperationException>()
                .With.Message.EqualTo($"There is already a {weapon1.Name} weapon."));
            }
            [Test]
            public void AddWeapon_WorksAsIntended()
            {
                Planet planet = new Planet("testP", 400);
                Weapon weapon1 = new Weapon("testW1", 100, 9);

                planet.AddWeapon(weapon1);

                string addedWeaponName = planet.Weapons.First().Name;

                Assert.AreEqual(weapon1.Name, addedWeaponName);
            }
            [Test]
            public void RemoveWeapon_WorksAsIntended()
            {
                Planet planet = new Planet("testP", 400);
                Weapon weapon1 = new Weapon("testW1", 100, 9);

                planet.AddWeapon(weapon1);
                Assert.AreEqual(planet.Weapons.Count, 1);
                planet.RemoveWeapon(weapon1.Name);
                Assert.AreEqual(planet.Weapons.Count, 0);
            }
            [Test]
            public void UpgradeWeapon_ThrowsExceptionWhenNoSuchWeapon()
            {
                Planet planet = new Planet("testP", 400);
                Weapon weapon1 = new Weapon("testW1", 100, 9);
                string weponToSearch = "noexistant";

                planet.AddWeapon(weapon1);

                Assert.That(() =>
                {
                    planet.UpgradeWeapon(weponToSearch);

                }, Throws.TypeOf<InvalidOperationException>()
                .With.Message.EqualTo($"{weponToSearch} does not exist in the weapon repository of {planet.Name}"));
            }
            [Test]
            public void UpgradeWeapon_WorksAsIntended()
            {
                int weapon1DestLevel = 9;
                Planet planet = new Planet("testP", 400);
                Weapon weapon1 = new Weapon("testW1", 100, weapon1DestLevel);

                planet.AddWeapon(weapon1);
                planet.UpgradeWeapon(weapon1.Name);

                Assert.AreEqual(weapon1.DestructionLevel, weapon1DestLevel + 1);
            }
            [Test]
            public void DestructOpponent_ThrowsExceptionIfOpponentMPRBigger()
            {
                Planet planet = new Planet("testP", 400);
                Planet planetOpponent = new Planet("opponent", 300);
                Weapon weapon1 = new Weapon("testW1", 100, 9);

                planetOpponent.AddWeapon(weapon1);

                Assert.That(() =>
                {
                    planet.DestructOpponent(planetOpponent);

                }, Throws.TypeOf<InvalidOperationException>()
                .With.Message.EqualTo($"{planetOpponent.Name} is too strong to declare war to!"));
            }
            [Test]
            public void DestructOpponent_WorksAsIntended()
            {
                Planet planet = new Planet("testP", 400);
                Planet planetOpponent = new Planet("opponent", 300);
                Weapon weapon1 = new Weapon("testW1", 100, 9);
                string expectedMessage = $"{planetOpponent.Name} is destructed!";

                planet.AddWeapon(weapon1);

                Assert.AreEqual(planet.DestructOpponent(planetOpponent), expectedMessage);
            }
        }
    }
}
