using System;
using NUnit.Framework;

namespace Game.Tests
{
    public class HealthTests
    {
        [Test]
        public void TakeDamage_ReducesCurrentHealth()
        {
            var health = new Health(100);

            health.TakeDamage(30);

            Assert.AreEqual(70, health.Current);
            Assert.IsFalse(health.IsDead);
        }

        [Test]
        public void TakeDamage_MoreThanCurrent_ClampsToZeroAndRaisesDied()
        {
            var health = new Health(50);
            var diedCount = 0;
            health.Died += () => diedCount++;

            health.TakeDamage(80);

            Assert.AreEqual(0, health.Current);
            Assert.IsTrue(health.IsDead);
            Assert.AreEqual(1, diedCount);
        }

        [Test]
        public void Heal_NeverExceedsMax()
        {
            var health = new Health(100);
            health.TakeDamage(10);

            health.Heal(999);

            Assert.AreEqual(100, health.Current);
        }

        [Test]
        public void Heal_OnDeadTarget_DoesNothing()
        {
            var health = new Health(20);
            health.TakeDamage(20);

            health.Heal(10);

            Assert.AreEqual(0, health.Current);
            Assert.IsTrue(health.IsDead);
        }

        [Test]
        public void TakeDamage_NegativeAmount_Throws()
        {
            var health = new Health(10);

            Assert.Throws<ArgumentOutOfRangeException>(() => health.TakeDamage(-1));
        }
    }
}
