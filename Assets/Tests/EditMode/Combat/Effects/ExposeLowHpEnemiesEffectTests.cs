using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ExposeLowHpEnemiesEffectTests
    {
        [Test]
        public void Apply_EnemyBelowThreshold_AppliesExposed()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 100);
            enemy.TakeDamage(80);
            var context = new CardEffectContext(player, enemy);
            var effect = new ExposeLowHpEnemiesEffect(hpThresholdPercent: 25, amount: 2);

            effect.Apply(context);

            Assert.That(enemy.Exposed, Is.EqualTo(2));
        }

        [Test]
        public void Apply_EnemyAtOrAboveThreshold_DoesNotApplyExposed()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 100);
            enemy.TakeDamage(70);
            var context = new CardEffectContext(player, enemy);
            var effect = new ExposeLowHpEnemiesEffect(hpThresholdPercent: 25, amount: 2);

            effect.Apply(context);

            Assert.That(enemy.Exposed, Is.EqualTo(0));
        }

        [Test]
        public void Apply_MultipleEnemies_OnlyAffectsThoseBelowThreshold()
        {
            var player = new Player(startingEnergy: 3);
            var lowHpEnemy = new Enemy(maxHP: 100);
            lowHpEnemy.TakeDamage(90);
            var healthyEnemy = new Enemy(maxHP: 100);
            var context = new CardEffectContext(player, lowHpEnemy, new[] { lowHpEnemy, healthyEnemy });
            var effect = new ExposeLowHpEnemiesEffect(hpThresholdPercent: 25, amount: 2);

            effect.Apply(context);

            Assert.That(lowHpEnemy.Exposed, Is.EqualTo(2));
            Assert.That(healthyEnemy.Exposed, Is.EqualTo(0));
        }
    }
}
