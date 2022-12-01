namespace Book.Tests
{
    using System;

    using NUnit.Framework;

    public class Tests
    {
        private const string BOOK_NAME = "testName";
        private const string AUTHOR = "testAuthor";

        Book book;

        [SetUp]
        public void SetUp()
        {
            book = new Book(BOOK_NAME, AUTHOR);
        }

        [Test]
        public void IfConstructorSetsUpThePropsCorrectly()
        {
            Assert.IsTrue(book.BookName == "testName" && book.Author == "testAuthor"
                && book.FootnoteCount == 0);
        }
        [TestCase(null)]
        [TestCase("")]
        public void Name_ThrowsEXceptionIfNullOrEmpty(string name)
        {
            Assert.That(() =>
            {
                Book book = new Book(name, AUTHOR);

            }, Throws.TypeOf<ArgumentException>()
            .With.Message.EqualTo($"Invalid {nameof(book.BookName)}!"));
        }
        [TestCase(null)]
        [TestCase("")]
        public void Author_ThrowsEXceptionIfNullOrEmpty(string author)
        {
            Assert.That(() =>
            {
                Book book = new Book(BOOK_NAME, author);

            }, Throws.TypeOf<ArgumentException>()
            .With.Message.EqualTo($"Invalid {nameof(book.Author)}!"));
        }
        [Test]
        public void AddFootnote_ExceptionWhenFootnoteExists()
        {
            book.AddFootnote(1, "Nehubava kniga");

            Assert.That(() =>
            {
                book.AddFootnote(1, "Nehubava kniga");

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo("Footnote already exists!"));
        }
        [Test]
        public void AddFootnote_AddToTheDictionaryAsIntended()
        {
            book.AddFootnote(1, "Nehubava kniga");
            book.AddFootnote(2, "Nehubava kniga, potvyrdeno");

            string actualFootnote = book.FindFootnote(1);
            string expectedFootnote = $"Footnote #1: Nehubava kniga";

            Assert.AreEqual(expectedFootnote, actualFootnote);
        }
        [Test]
        public void FIndFootnote_ThrowExceptionIfFootnoteDoesNotExist()
        {
            book.AddFootnote(1, "Nehubava kniga");
            book.AddFootnote(2, "Nehubava kniga, potvyrdeno");

            Assert.That(() =>
            {
                book.FindFootnote(3);

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo("Footnote doesn't exists!"));
        }
        [Test]
        public void AfterFootnote_ThrowExceptionIfFootnoteDoesNotExist()
        {
            book.AddFootnote(1, "Nehubava kniga");
            book.AddFootnote(2, "Nehubava kniga, potvyrdeno");

            Assert.That(() =>
            {
                book.AlterFootnote(3, "Malko mi haresa!");

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo("Footnote does not exists!"));
        }
        [Test]
        public void AfterFootnote_ReplacesTheMessageProperly()
        {
            book.AddFootnote(1, "Nehubava kniga");
            book.AddFootnote(2, "Nehubava kniga, potvyrdeno");

            book.AlterFootnote(2, "Malko mi haresa!");

            string actualFootnote = book.FindFootnote(2);
            string expectedFootnote = $"Footnote #2: Malko mi haresa!";

            Assert.AreEqual(expectedFootnote, actualFootnote);
        }
    }
}