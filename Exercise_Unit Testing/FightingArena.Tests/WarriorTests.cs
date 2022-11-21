namespace FightingArena.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class WarriorTests
    {
        private const int MIN_ATTACK_HP = 30;

        private const string HERO_NAME = "Kokos";
        private const int HERO_DMG = 15;
        private const int HERO_HP = 60;

        private const string OPP_NAME = "Losh_MC";
        private const int OPP_DMG = 15;
        private const int OPP_HP = 60;

        Warrior hero;
        Warrior opponent;

        [SetUp]
        public void SetUp() 
        {
            hero = new Warrior(HERO_NAME, HERO_DMG, HERO_HP);
            opponent = new Warrior(OPP_NAME, OPP_DMG, OPP_HP);
        }
        [Test]
        public void ConstructorWorksProperly() 
        {
            Assert.That(hero.Name, Is.EqualTo(HERO_NAME));
            Assert.That(hero.Damage, Is.EqualTo(HERO_DMG));
            Assert.That(hero.HP, Is.EqualTo(HERO_HP));
        }
        [TestCase(null)]
        [TestCase("")]
        public void TesstIfNameSetterWorks(string name) 
        {
            Assert.That(() =>
            {
                hero = new Warrior(name, HERO_DMG, HERO_HP);
            }, Throws.TypeOf<ArgumentException>().With.Message
            .EqualTo("Name should not be empty or whitespace!"));
        }
        [TestCase(0)]
        [TestCase(-23)]
        public void TesstIfDamageSetterWorks(int damage)
        {
            Assert.That(() =>
            {
                hero = new Warrior(HERO_NAME, damage, HERO_HP);
            }, Throws.TypeOf<ArgumentException>().With.Message
            .EqualTo("Damage value should be positive!"));
        }
        [TestCase(-23)]
        public void TesstIfHPSetterWorks(int HP)
        {
            Assert.That(() =>
            {
                hero = new Warrior(HERO_NAME, HERO_DMG, HP);
            }, Throws.TypeOf<ArgumentException>().With.Message
            .EqualTo("HP should not be negative!"));
        }
        [Test]
        public void CannotAttackIfHPIsLow() 
        {
            hero = new Warrior(HERO_NAME, HERO_DMG, 10);

            Assert.That(() =>
            {
                hero.Attack(opponent);
            }, Throws.TypeOf<InvalidOperationException>().With.Message
            .EqualTo("Your HP is too low in order to attack other warriors!"));
        }
        [Test]
        public void CannotAttackWeakEnemy()
        {
            opponent = new Warrior(OPP_NAME, OPP_DMG, 10);

            Assert.That(() =>
            {
                hero.Attack(opponent);
            }, Throws.TypeOf<InvalidOperationException>().With.Message
            .EqualTo($"Enemy HP must be greater than {MIN_ATTACK_HP} in order to attack him!"));
        }
        [Test]
        public void CannotAttackIfYourHPIsLow()
        {
            hero = new Warrior(HERO_NAME, HERO_DMG, 40);
            opponent = new Warrior(OPP_NAME, 41, OPP_HP);

            Assert.That(() =>
            {
                hero.Attack(opponent);
            }, Throws.TypeOf<InvalidOperationException>().With.Message
            .EqualTo("You are trying to attack too strong enemy"));
        }
        [Test]
        public void EnemyHPCannotBeNegative() 
        {
            hero = new Warrior(HERO_NAME, HERO_DMG + 46, HERO_HP);

            hero.Attack(opponent);

            Assert.That(opponent.HP, Is.EqualTo(0));
        }
    }
}