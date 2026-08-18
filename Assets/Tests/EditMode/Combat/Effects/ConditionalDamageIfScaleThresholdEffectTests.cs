using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConditionalDamageIfScaleThresholdEffectTests
    {
        [Test]
        public void Apply_ScaleAtThreshold_DealsBonusDamage()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(-3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalDamageIfScaleThresholdEffect(baseAmount: 6, bonusAmount: 6, threshold: -3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void Apply_ScaleBelowThreshold_DealsBonusDamage()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(-4);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalDamageIfScaleThresholdEffect(baseAmount: 6, bonusAmount: 6, threshold: -3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void Apply_ScaleAboveThreshold_DealsOnlyBaseDamage()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(-2);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalDamageIfScaleThresholdEffect(baseAmount: 6, bonusAmount: 6, threshold: -3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }
    }
}
