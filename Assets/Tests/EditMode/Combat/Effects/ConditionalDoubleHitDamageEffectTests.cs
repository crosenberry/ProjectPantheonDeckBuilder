using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConditionalDoubleHitDamageEffectTests
    {
        [Test]
        public void Apply_VolleyBelowThreshold_DealsSingleHitDamage()
        {
            var player = new Player(startingEnergy: 3);
            player.GainVolley(3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalDoubleHitDamageEffect(amount: 6, volleyThreshold: 4);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void Apply_VolleyAtThreshold_DealsDoubleHitDamage()
        {
            var player = new Player(startingEnergy: 3);
            player.GainVolley(4);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalDoubleHitDamageEffect(amount: 6, volleyThreshold: 4);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void Apply_VolleyAboveThreshold_DealsDoubleHitDamage()
        {
            var player = new Player(startingEnergy: 3);
            player.GainVolley(9);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalDoubleHitDamageEffect(amount: 6, volleyThreshold: 4);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void Apply_DoubleHit_AppliesStrengthToEachHitIndependently()
        {
            var player = new Player(startingEnergy: 3);
            player.GainVolley(4);
            player.GainStrength(2);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalDoubleHitDamageEffect(amount: 6, volleyThreshold: 4);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(26));
        }
    }
}
