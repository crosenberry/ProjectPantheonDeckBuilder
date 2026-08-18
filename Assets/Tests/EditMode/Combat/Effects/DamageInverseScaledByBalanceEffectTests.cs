using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class DamageInverseScaledByBalanceEffectTests
    {
        [Test]
        public void Apply_ScaleZero_DealsMaxDamage()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DamageInverseScaledByBalanceEffect(multiplier: 4, maxMagnitude: 5);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(22));
        }

        [Test]
        public void Apply_ScaleAtMaxMagnitude_DealsNoDamage()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(5);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DamageInverseScaledByBalanceEffect(multiplier: 4, maxMagnitude: 5);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
        }

        [Test]
        public void Apply_NegativeScale_UsesMagnitudeNotSign()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(-2);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DamageInverseScaledByBalanceEffect(multiplier: 4, maxMagnitude: 5);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }
    }
}
