
namespace Skeleton.Tests
{
    using System;
    using NUnit.Framework;
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void AxeLoosesDurabilityAfterAttack()
        {
            Axe axe = new Axe(10, 10);
            Dummy dummy = new Dummy(100, 100);

            axe.Attack(dummy);

            Assert.That(axe.DurabilityPoints, Is.EqualTo(9), "Durrability does not change after attack.");
        }
        [Test]
        public void AttackingWithBrokenWeapon()
        {
            Axe axe = new Axe(10, 0);
            Dummy dummy = new Dummy(100, 100);

            Assert.That(() =>
            {
                axe.Attack(dummy);
            }, Throws.TypeOf<InvalidOperationException>().With.Message.EqualTo("Axe is broken."));
        }
    }
}