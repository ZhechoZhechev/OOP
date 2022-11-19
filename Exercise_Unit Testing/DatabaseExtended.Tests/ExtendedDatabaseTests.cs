namespace DatabaseExtended.Tests
{
    using NUnit.Framework;
    using System;

    using ExtendedDatabase;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database database;

        [SetUp]
        public void SetUp()
        {
            Person[] people = new Person[14];

            for (int i = 0; i < people.Length; i++)
            {
                people[i] = new Person(i, ((char)('A' + i)).ToString());
            }

            database = new Database(people);
        }
        [Test]
        public void ConstructorShouldNotTakeMoreThan16()
        {
            Person[] people = new Person[17];

            for (int i = 0; i < people.Length; i++)
            {
                people[i] = new Person(i, ((char)('A' + i)).ToString());
            }

            Assert.That(() =>
            {
                Database database = new Database(people);
            }, Throws.TypeOf<ArgumentException>().With.Message
            .EqualTo("Provided data length should be in range [0..16]!"));

        }
        [Test]
        public void Add_ThrowsExceptionWhenCountIs16()
        {
            database.Add(new Person(14, "14"));
            database.Add(new Person(15, "15"));

            Assert.That(() =>
            {
                database.Add(new Person(16, "16"));
            }, Throws.TypeOf<InvalidOperationException>().With.Message
            .EqualTo("Array's capacity must be exactly 16 integers!"));
        }
        [Test]
        public void Add_ThrowsExceptionIfExistingUsername()
        {
            Assert.That(() =>
            {
                database.Add(new Person(3, ((char)('A' + 3)).ToString()));
            }, Throws.TypeOf<InvalidOperationException>().With.Message
            .EqualTo("There is already user with this username!"));
        }
        [Test]
        public void Add_ThrowsExceptionIfExistingID()
        {
            Assert.That(() =>
            {
                database.Add(new Person(3, "KOKOS"));
            }, Throws.TypeOf<InvalidOperationException>().With.Message
            .EqualTo("There is already user with this Id!"));
        }
        [Test]
        public void Remove_ThrowsExceptionIfCountIsZero()
        {
            Database database = new Database(new Person(123, "Kokostche"));

            Assert.That(() =>
            {
                database.Remove();
                database.Remove();
            }, Throws.TypeOf<InvalidOperationException>());
        }
        [Test]
        public void Remove_RemovesLastPerson()
        {
            Person person = new Person(15, "Kokosiniu");
            database.Add(person);
            database.Remove();
            Assert.That(() =>
            {
                database.FindByUsername("Kokosiniu");
            }, Throws.TypeOf<InvalidOperationException>().With.Message
            .EqualTo("No user is present by this username!"));
        }
        [Test]
        public void Remove_DecreasesTheCollectionCount()
        {
            int expectedCount = database.Count - 1;
            database.Remove();
            Assert.AreEqual(database.Count, expectedCount);
        }
        [TestCase(null)]
        [TestCase("")]
        public void FindByUsername_NullOrEmptyException(string name)
        {
            //database.Add(new Person(123, name));

            Assert.That(() =>
            {
                database.FindByUsername(name);
            }, Throws.TypeOf<ArgumentNullException>());
        }
        [Test]
        public void FindUsername_NotExistingNname()
        {
            Assert.That(() =>
            {
                database.FindByUsername("AlKokone");
            }, Throws.TypeOf<InvalidOperationException>().With.Message
            .EqualTo("No user is present by this username!"));
        }
        [Test]
        public void FindUsername_IsCaseSensitive()
        {
            database.Add(new Person(342, "Kokos"));

            Assert.That(() =>
            {
                database.FindByUsername("KOKOS");
            }, Throws.TypeOf<InvalidOperationException>().With.Message
            .EqualTo("No user is present by this username!"));
        }
        [Test]
        public void FindByUsername_ReturnsTheCorrectPerson()
        {
            Person personToFind = database.FindByUsername("A");
            Assert.AreEqual(personToFind.UserName, "A");
        }
        [Test]
        public void FindById_ExceptionWhenNoSuchId()
        {
            Assert.That(() =>
            {
                database.FindById(1992);
            }, Throws.TypeOf<InvalidOperationException>().With.Message
            .EqualTo("No user is present by this ID!"));
        }
        [Test]
        public void FindById_ExceptionWhenNegativeId()
        {
            Assert.That(() =>
            {
                database.FindById(-1992);
            }, Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        [Test]
        public void FindById_ReturnsTheCorrectPerson()
        {
            Person personToFind = database.FindById(4);
            Assert.AreEqual(personToFind.Id, 4);
        }
    }
}