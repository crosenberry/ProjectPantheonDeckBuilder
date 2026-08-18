using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConsumeStormOrFallbackDamageEffectTests
    {
        [Test]
        public void Apply_HasEnoughStorm_ConsumesAndDealsMainDamage()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(2);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeStormOrFallbackDamageEffect(consumeAmount: 1, damageIfConsumed: 9, damageIfNotConsumed: 4);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
            Assert.That(player.Storm, Is.EqualTo(1));
        }

        [Test]
        public void Apply_NoStorm_DealsFallbackDamageWithoutConsuming()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeStormOrFallbackDamageEffect(consumeAmount: 1, damageIfConsumed: 9, damageIfNotConsumed: 4);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(38));
            Assert.That(player.Storm, Is.EqualTo(0));
        }
    }
}
