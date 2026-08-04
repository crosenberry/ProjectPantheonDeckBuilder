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
    }
}
