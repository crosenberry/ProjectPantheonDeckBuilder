using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConsumeScaleIfPositiveDamageToAllEnemiesEffectTests
    {
        [Test]
        public void Apply_ScalePositive_DealsScaledDamageToAllEnemiesAndResetsScale()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(3);
            var enemyA = new Enemy(maxHP: 42);
            var enemyB = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemyA, new[] { enemyA, enemyB });
            var effect = new ConsumeScaleIfPositiveDamageToAllEnemiesEffect(damagePerPoint: 5);

            effect.Apply(context);

            Assert.That(enemyA.CurrentHP, Is.EqualTo(27));
            Assert.That(enemyB.CurrentHP, Is.EqualTo(27));
            Assert.That(player.Scale, Is.EqualTo(0));
        }

        [Test]
        public void Apply_ScaleZeroOrNegative_DealsNoDamageAndLeavesScaleUnchanged()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(-2);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeScaleIfPositiveDamageToAllEnemiesEffect(damagePerPoint: 5);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
            Assert.That(player.Scale, Is.EqualTo(-2));
        }
    }
}
