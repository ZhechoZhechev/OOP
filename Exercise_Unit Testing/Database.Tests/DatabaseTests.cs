namespace Database.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class DatabaseTests
    {
        [Test]
        public void DatabaseCapacityShouldBe16Integers()
        {

            Assert.That(() =>
            {
                Database database = new Database(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 });
            }, Throws.TypeOf<InvalidOperationException>().With.Message.EqualTo("Array's capacity must be exactly 16 integers!"));
        }
        [Test]
        public void IfAdding17thElementExceptionIsThrown()
        {
            Database database = new Database(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });

            Assert.That(() =>
            {
                database.Add(17);
            }, Throws.TypeOf<InvalidOperationException>().With.Message.EqualTo("Array's capacity must be exactly 16 integers!"));
        }
        [Test]
        public void IfToRemoveElementFromEmptyDatabaseExceptionIsThrown()
        {
            Database database = new Database(new int[] { });

            Assert.That(() =>
            {
                database.Remove();
            }, Throws.TypeOf<InvalidOperationException>().With.Message.EqualTo("The collection is empty!"));
        }
        [Test]
        public void IfFetchReturnsCorrectInfo()
        {
            Database database = new Database(new int[] { 1, 2, 4, 5, 6, 10 });
            int[] expectedData = new int[] { 1, 2, 4, 5, 6, 10 };
            int[] actualData = database.Fetch();

            CollectionAssert.AreEqual(expectedData, actualData, "Fetch method shoud return the initial array");
        }
        [TestCase(new int[] { })]
        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void ConstructorShouldWorkProperly(int[] elementsToAdd)
        {
            Database database = new Database(elementsToAdd);

            int[] expectedElements = elementsToAdd;
            int[] actualElements = database.Fetch();

            int actualCount = database.Count;
            int exepectedCount = elementsToAdd.Length;

            CollectionAssert.AreEqual(expectedElements, actualElements, "Constructor sould add exaclty what is in parameters array");
            Assert.That(exepectedCount, Is.EqualTo(actualCount), "Constructor should set the count field properly");
        }
        [Test]
        public void AddOperationIncreasesCountByOne()
        {
            Database database = new Database(1, 2, 3, 4, 5);
            database.Add(6);
            Assert.AreEqual(6, database.Count, "Add operation doesn't increase count by 1.");
        }

        [Test]
        public void RemoveOperationDecreasesCountByOne()
        {
            Database database = new Database(1, 2, 3, 4, 5);
            database.Remove();
            Assert.AreEqual(database.Count, 4, "Remove operation doesn't decrease count by 1.");
        }
    }
}
