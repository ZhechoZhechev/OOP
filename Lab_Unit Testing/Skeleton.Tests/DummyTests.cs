
namespace Skeleton.Tests
{
    using NUnit.Framework;
    using System;
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void DummyLoosesHPWhenAttacked()
        {
            Dummy dummy = new Dummy(100, 100);

            dummy.TakeAttack(10);

            Assert.That(dummy.Health, Is.EqualTo(90), "Dummy is not loosing HP when attacked.");
        }
        [Test]
        public void DeadDummyThrowsAnExceptionWhenAttacked()
        {
            Dummy dummy = new Dummy(0, 100);

            Assert.That(() =>
            {
                dummy.TakeAttack(10);
            }, Throws.TypeOf<InvalidOperationException>().With.Message.EqualTo("Dummy is dead."));
        }
        [Test]
        public void DeadDummyGivesEXP() 
        {
            Dummy dummy = new Dummy(-1, 100);

            int expAward = dummy.GiveExperience();

            Assert.That(expAward, Is.EqualTo(100), "Dead dummy awards no experience.");
        }
        [Test]
        public void AliveDummyDoesNotGiveEXP() 
        {
            Dummy dummy = new Dummy(100, 100);

            Assert.That(() =>
            {
                dummy.GiveExperience();
            }, Throws.TypeOf<InvalidOperationException>().With.Message.EqualTo("Target is not dead."));
        }
    }
}