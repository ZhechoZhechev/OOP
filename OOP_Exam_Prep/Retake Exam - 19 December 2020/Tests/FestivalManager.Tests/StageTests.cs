// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 
namespace FestivalManager.Tests
{
    using FestivalManager.Entities;
    using NUnit.Framework;
    using System;

    [TestFixture]
	public class StageTests
    {
		private const string SongName = "testSong";
		private TimeSpan SongDuration = new TimeSpan(0, 5, 33);
		private const string PerformerFirstName = "testPerfFirstName";
		private const string PerformerLastName = "testPerfLastName";
		private const int PerformerAge = 33;

		private Song song;
		private Performer performer;
		private Stage stage;

		[SetUp]
		public void SetUp()
        {
			song = new Song(SongName, SongDuration);
			performer = new Performer(PerformerFirstName, PerformerLastName, PerformerAge);
			stage = new Stage();
        }

		[Test]
	    public void Constructor_SetsTheMassivesCorrectly()
        {
			stage.AddPerformer(performer);

			Assert.AreEqual(stage.Performers.Count, 1);
        }

		[Test]
		public void TestingMethodsWithNullArgumentThrowError() 
		{
			Assert.Throws<ArgumentNullException>(() => stage.AddPerformer(null));
			Assert.Throws<ArgumentNullException>(() => stage.AddSong(null));
			Assert.Throws<ArgumentNullException>(() => stage.AddSongToPerformer(null, PerformerFirstName));
			Assert.Throws<ArgumentNullException>(() => stage.AddSongToPerformer(SongName, null));
		}

		[Test]
		public void AddPerformer_ThrowsIfAgeUnder18() 
		{
			Assert.Throws<ArgumentException>(() => stage.AddPerformer(new Performer(PerformerFirstName, PerformerLastName, 3)));
		}

		[Test]
		public void AddSong_ThrowsIfSongUnderOneMinute() 
		{
			Assert.Throws<ArgumentException>(() => stage.AddSong(new Song(SongName, new TimeSpan(0,0,30))));
		}

		[Test]
		public void AddSongToThePerformer_ReturnsCorrectString() 
		{
			stage.AddPerformer(performer);
			stage.AddSong(song);
			string actual = stage.AddSongToPerformer(SongName, performer.FullName);
			string expected = $"{song} will be performed by {performer}";

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Play_ReturnsCorrectString() 
		{
			stage.AddSong(song);
			stage.AddPerformer(performer);
			stage.AddSongToPerformer(SongName, performer.FullName);

			string expected = $"{stage.Performers.Count} performers played {1} songs";
			string actual = stage.Play();

			Assert.AreEqual(expected, actual);
		}

	}
}