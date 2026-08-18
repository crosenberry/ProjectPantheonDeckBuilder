using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConditionalDamageIfStormPresentEffectTests
    {
        [Test]
        public void Apply_NoStorm_DealsOnlyBaseDamage()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalDamageIfStormPresentEffect(baseAmount: 7, bonusAmount: 4);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(35));
        }

        [Test]
        public void Apply_HasStorm_DealsBaseAndBonusDamage()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(2);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalDamageIfStormPresentEffect(baseAmount: 7, bonusAmount: 4);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(31));
        }
    }
}
