using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class VolleyScaledDamageToAllEnemiesEffectTests
    {
        [Test]
        public void Apply_NoVolley_DealsBaseDamageToEachEnemy()
        {
            var player = new Player(startingEnergy: 3);
            var enemyA = new Enemy(maxHP: 42);
            var enemyB = new Enemy(maxHP: 30);
            var context = new CardEffectContext(player, enemyA, new[] { enemyA, enemyB });
            var effect = new VolleyScaledDamageToAllEnemiesEffect(baseAmount: 3, bonusPerVolley: 3);

            effect.Apply(context);

            Assert.That(enemyA.CurrentHP, Is.EqualTo(39));
            Assert.That(enemyB.CurrentHP, Is.EqualTo(27));
        }

        [Test]
        public void Apply_WithVolley_AddsBonusDamageToEachEnemy()
        {
            var player = new Player(startingEnergy: 3);
            player.GainVolley(2);
            var enemyA = new Enemy(maxHP: 42);
            var enemyB = new Enemy(maxHP: 30);
            var context = new CardEffectContext(player, enemyA, new[] { enemyA, enemyB });
            var effect = new VolleyScaledDamageToAllEnemiesEffect(baseAmount: 3, bonusPerVolley: 3);

            effect.Apply(context);

            Assert.That(enemyA.CurrentHP, Is.EqualTo(33));
            Assert.That(enemyB.CurrentHP, Is.EqualTo(21));
        }

        [Test]
        public void Apply_DoesNotConsumeVolley()
        {
            var player = new Player(startingEnergy: 3);
            player.GainVolley(2);
            var enemyA = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemyA, new[] { enemyA });
            var effect = new VolleyScaledDamageToAllEnemiesEffect(baseAmount: 3, bonusPerVolley: 3);

            effect.Apply(context);

            Assert.That(player.Volley, Is.EqualTo(2));
        }
    }
}
