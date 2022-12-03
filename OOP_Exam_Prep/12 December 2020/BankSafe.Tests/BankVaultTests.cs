using NUnit.Framework;
using System;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private const string ITEM_OWNER = "TestOwner";
        private const string ITEM_ID = "TestID";

        private Item item;
        private BankVault bankVault;

        [SetUp]
        public void Setup()
        {
            item = new Item(ITEM_OWNER, ITEM_ID);
            bankVault = new BankVault();
        }

        [Test]
        public void Constrictor_WorksAsIntended()
        {
            Assert.IsTrue(bankVault.VaultCells.ContainsKey("B1"));
        }
        [Test]
        public void AddItem_ExceptionWhenCellDoesNotExist() 
        {
            Assert.That(() =>
            {
                bankVault.AddItem("F5", item);
            }, Throws.TypeOf<ArgumentException>()
            .With.Message.EqualTo("Cell doesn't exists!"));
        }
        [Test]
        public void AddItem_ExceptionWhenCellIsFull() 
        {
            Item itemNew = new Item("BajZhi", "bbq");

            bankVault.AddItem("A1", item);

            Assert.That(() =>
            {
                bankVault.AddItem("A1", itemNew);
            }, Throws.TypeOf<ArgumentException>()
            .With.Message.EqualTo("Cell is already taken!"));
        }
        [Test]
        public void AddItem_ExceptionWhenItemAlreadyAdded()
        {
            bankVault.AddItem("A1", item);

            Assert.That(() =>
            {
                bankVault.AddItem("A2", item);
            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo("Item is already in cell!"));
        }
        [Test]
        public void AddItem_AddTheItemToTheDictionary() 
        {
            string actualString = bankVault.AddItem("A1", item);
            string expectedString = $"Item:{item.ItemId} saved successfully!";

            Assert.AreSame(bankVault.VaultCells["A1"], item);
            Assert.AreEqual(expectedString, actualString);
        }
        [Test]
        public void RemoveItem_ExceptionForNonExistantCell() 
        {
            Assert.That(() =>
            {
                bankVault.RemoveItem("GTochka", item);
            }, Throws.TypeOf<ArgumentException>()
            .With.Message.EqualTo("Cell doesn't exists!"));
        }
        [Test]
        public void RemoveItem_ExceptionItemInTheCellDoesNotExist() 
        {
            Item itemNew = new Item("BaiZhi", "kokal");
            bankVault.AddItem("A1", item);

            Assert.That(() =>
            {
                bankVault.RemoveItem("A1", itemNew);
            }, Throws.TypeOf<ArgumentException>()
            .With.Message.EqualTo("Item in that cell doesn't exists!"));
        }
        [Test]
        public void RemoveItem_WorksAsIntended() 
        {
            bankVault.AddItem("A1", item);
            string expectedMessage = $"Remove item:{item.ItemId} successfully!";
            string actualMessage = bankVault.RemoveItem("A1", item);

            Assert.AreSame(bankVault.VaultCells["A1"], null);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

    }
}