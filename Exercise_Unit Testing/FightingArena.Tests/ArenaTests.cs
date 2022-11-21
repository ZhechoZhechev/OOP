namespace FightingArena.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class ArenaTests
    {
        Warrior one;
        Warrior two;
        Arena arena;

        [SetUp]
        public void SetUp() 
        {
            one = new Warrior("Kokos", 25, 70);
            two = new Warrior("Losh_MC", 20, 65);
        }
        [Test]
        public void ConstructorSetCollection() 
        {
            List<Warrior> list = new List<Warrior>();
            list.Add(one);

            Arena arena = new Arena();
            arena.Enroll(one);

            CollectionAssert.AreEqual(arena.Warriors, list);
            Assert.AreEqual(arena.Count, list.Count);
        }
        [Test]
        public void Enroll_TrowsExceptionIfNameExists() 
        {
            arena = new Arena();
            arena.Enroll(one);

            Assert.That(() =>
            {
                arena.Enroll(one);
            }, Throws.TypeOf<InvalidOperationException>().With.Message
            .EqualTo("Warrior is already enrolled for the fights!"));
        }
        [Test]
        public void Fight_IfSecondWarriorNameMissing() 
        {
            arena = new Arena();
            arena.Enroll(one);
            arena.Enroll(two);

            string missingName = "NonExistant";

            Assert.That(() =>
            {
                arena.Fight("Kokos", missingName);
            }, Throws.TypeOf<InvalidOperationException>().With.Message
            .EqualTo($"There is no fighter with name {missingName} enrolled for the fights!"));
        }
        [Test]
        public void Fight_IfFirstWarriorNameMissing()
        {
            arena = new Arena();
            arena.Enroll(one);
            arena.Enroll(two);

            string missingName = "NonExistant";

            Assert.That(() =>
            {
                arena.Fight(missingName, "Losh_MC");
            }, Throws.TypeOf<InvalidOperationException>().With.Message
            .EqualTo($"There is no fighter with name {missingName} enrolled for the fights!"));
        }
        [Test]
        public void Fight_WorksProperly()
        {
            arena = new Arena();
            arena.Enroll(one);
            arena.Enroll(two);

            int kokosExpecetedHP = one.HP - two.Damage;
            int loshExpectedHP = two.HP - one.Damage;

            arena.Fight("Kokos", "Losh_MC");

            Assert.AreEqual(one.HP, kokosExpecetedHP, "First warrior takes dmg as expected");
            Assert.AreEqual(two.HP, loshExpectedHP, "Second warrior takes dmg as expected.");
        }
    }
}
