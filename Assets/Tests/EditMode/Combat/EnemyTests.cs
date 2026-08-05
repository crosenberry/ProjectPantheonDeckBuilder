using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat
{
    public class EnemyTests
    {
        [Test]
        public void TakeDamage_ReducesCurrentHP()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.TakeDamage(9);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
        }

        [Test]
        public void TakeDamage_AmountExceedsCurrentHP_ClampsAtZero()
        {
            var enemy = new Enemy(maxHP: 10);

            enemy.TakeDamage(999);

            Assert.That(enemy.CurrentHP, Is.EqualTo(0));
        }

        [Test]
        public void GainStrength_IncreasesStrength()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.GainStrength(2);

            Assert.That(enemy.Strength, Is.EqualTo(2));
        }

        [Test]
        public void ApplyExposed_IncreasesExposed()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.ApplyExposed(2);

            Assert.That(enemy.Exposed, Is.EqualTo(2));
        }

        [Test]
        public void ApplyDrained_IncreasesDrained()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.ApplyDrained(2);

            Assert.That(enemy.Drained, Is.EqualTo(2));
        }

        [Test]
        public void StartTurn_ExposedAboveZero_DecrementsByOne()
        {
            var enemy = new Enemy(maxHP: 42);
            enemy.ApplyExposed(2);

            enemy.StartTurn();

            Assert.That(enemy.Exposed, Is.EqualTo(1));
        }

        [Test]
        public void StartTurn_ExposedAtZero_StaysAtZero()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.StartTurn();

            Assert.That(enemy.Exposed, Is.EqualTo(0));
        }

        [Test]
        public void StartTurn_DrainedAboveZero_DecrementsByOne()
        {
            var enemy = new Enemy(maxHP: 42);
            enemy.ApplyDrained(2);

            enemy.StartTurn();

            Assert.That(enemy.Drained, Is.EqualTo(1));
        }

        [Test]
        public void StartTurn_StrengthDoesNotDecay()
        {
            var enemy = new Enemy(maxHP: 42);
            enemy.GainStrength(3);

            enemy.StartTurn();

            Assert.That(enemy.Strength, Is.EqualTo(3));
        }
    }
}
