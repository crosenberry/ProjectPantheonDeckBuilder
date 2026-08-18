using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ScaleMagnitudeDamageWithConditionalExposedEffectTests
    {
        [Test]
        public void Apply_ScalePositive_DealsScaledDamageWithoutExposed()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ScaleMagnitudeDamageWithConditionalExposedEffect(damagePerPoint: 3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
            Assert.That(enemy.Exposed, Is.EqualTo(0));
        }

        [Test]
        public void Apply_ScaleNegative_DealsScaledDamageAndAppliesExposedEqualToMagnitude()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(-3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ScaleMagnitudeDamageWithConditionalExposedEffect(damagePerPoint: 3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
            Assert.That(enemy.Exposed, Is.EqualTo(3));
        }

        [Test]
        public void Apply_ScaleZero_DealsNoDamage()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ScaleMagnitudeDamageWithConditionalExposedEffect(damagePerPoint: 3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
        }
    }
}
