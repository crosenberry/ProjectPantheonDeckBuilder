using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConsumeAllStormOrBaseDamageEffectTests
    {
        [Test]
        public void Apply_HasStorm_ConsumesAllAndDealsScaledDamageInsteadOfBase()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeAllStormOrBaseDamageEffect(baseAmount: 3, damagePerPointIfConsumed: 5);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(27));
            Assert.That(player.Storm, Is.EqualTo(0));
        }

        [Test]
        public void Apply_NoStorm_DealsBaseDamage()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeAllStormOrBaseDamageEffect(baseAmount: 3, damagePerPointIfConsumed: 5);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(39));
        }
    }
}
