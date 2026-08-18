using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConditionalDamageIfScaleNegativeEffectTests
    {
        [Test]
        public void Apply_ScaleNegative_DealsBonusDamage()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(-1);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalDamageIfScaleNegativeEffect(baseAmount: 5, bonusAmount: 3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(34));
        }

        [Test]
        public void Apply_ScaleZeroOrPositive_DealsOnlyBaseDamage()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalDamageIfScaleNegativeEffect(baseAmount: 5, bonusAmount: 3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(37));
        }
    }
}
