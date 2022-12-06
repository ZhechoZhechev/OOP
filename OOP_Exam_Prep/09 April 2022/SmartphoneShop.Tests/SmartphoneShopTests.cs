using System;

using NUnit.Framework;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        private const string MODEL_NAME = "test";
        private const int MAX_BATTERY_CHARGE = 100;
        private const int SHOP_CAPACITY = 2;

        Smartphone smartphone;
        Shop shop;

        [SetUp]
        public void SetUp()
        {
            smartphone = new Smartphone(MODEL_NAME, MAX_BATTERY_CHARGE);
            shop = new Shop(SHOP_CAPACITY);
        }
        [Test]
        public void IfConstructorWorksProperly()
        {
            Assert.IsTrue(smartphone.ModelName == MODEL_NAME
                && smartphone.MaximumBatteryCharge == MAX_BATTERY_CHARGE
                && smartphone.CurrentBateryCharge == MAX_BATTERY_CHARGE
                && shop.Capacity == SHOP_CAPACITY);
        }
        [Test]
        public void Capacity_ThrowsErrorWhenBellowZero()
        {
            Assert.That(() =>
            {
                Shop shop = new Shop(-1);
            }, Throws.TypeOf<ArgumentException>()
            .With.Message.EqualTo("Invalid capacity."));
        }
        [Test]
        public void Count_WorkdProperly()
        {
            shop.Add(smartphone);

            Assert.AreEqual(shop.Count, 1);
        }
        [Test]
        public void Add_ThrowsExceptionWhenPhoneNameExists()
        {
            shop.Add(smartphone);

            Assert.That(() =>
            {
                shop.Add(smartphone);
            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo($"The phone model {smartphone.ModelName} already exist."));
        }
        [Test]
        public void Add_ThrowsExceptionCapacityIsFull()
        {
            Shop shop = new Shop(1);
            Smartphone phone = new Smartphone("test2", 99);

            shop.Add(smartphone);

            Assert.That(() =>
            {
                shop.Add(phone);
            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo("The shop is full."));
        }
        [Test]

        public void Remove_ThrowsExceptionIfPhoneModelIsNonexistant() 
        {
            shop.Add(smartphone);

            Assert.That(() =>
            {
                shop.Remove("Nokia3310");
            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo($"The phone model Nokia3310 doesn't exist."));
        }
        [Test]

        public void Remove_DecresesCountWhenPhoneIsRemoved()
        {
            shop.Add(smartphone);

            shop.Remove(MODEL_NAME);

            Assert.AreEqual(shop.Count, 0);

        }
        [Test]
        public void TestPhone_ThrowsExceptionWhenModelNameNonexistant() 
        {
            shop.Add(smartphone);

            Assert.That(() =>
            {
                shop.TestPhone("Nokia3310", 50);
            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo($"The phone model Nokia3310 doesn't exist."));
        }
        [Test]
        public void TestPhone_ThrowsExceptionWhenNotEnoughCharge()
        {
            Smartphone phone = new Smartphone("Nokia3310", 40);

            shop.Add(phone);

            Assert.That(() =>
            {
                shop.TestPhone("Nokia3310", 50);
            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo($"The phone model Nokia3310 is low on batery."));
        }
        [Test]
        public void TestPhone_ReducesCurrentCharge()
        {

            shop.Add(smartphone);

            shop.TestPhone(smartphone.ModelName, 49);

            Assert.AreEqual(smartphone.CurrentBateryCharge, 51);
        }
        [Test]
        public void ChargePhone_ThrowsExceptionWhenModelNameNonexistant()
        {
            shop.Add(smartphone);

            Assert.That(() =>
            {
                shop.ChargePhone("Nokia3310");

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo($"The phone model Nokia3310 doesn't exist."));
        }
        [Test]
        public void ChargePhone_ChargesPhoneToMaxLevel()
        {
            Smartphone phone = new Smartphone("Nokia3310", 100);

            shop.Add(phone);
            shop.TestPhone(phone.ModelName, 55);
            shop.ChargePhone(phone.ModelName);

            Assert.AreEqual(phone.CurrentBateryCharge, 100);
        }
    }
}
