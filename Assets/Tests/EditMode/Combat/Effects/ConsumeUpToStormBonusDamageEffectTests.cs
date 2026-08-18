using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConsumeUpToStormBonusDamageEffectTests
    {
        [Test]
        public void Apply_StormMeetsCap_ConsumesFullCapAndAddsBonus()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(2);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeUpToStormBonusDamageEffect(baseAmount: 5, maxConsume: 2, bonusPerPoint: 3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(31));
            Assert.That(player.Storm, Is.EqualTo(0));
        }

        [Test]
        public void Apply_StormExceedsCap_ConsumesOnlyCapAndLeavesRemainder()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(5);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeUpToStormBonusDamageEffect(baseAmount: 5, maxConsume: 2, bonusPerPoint: 3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(31));
            Assert.That(player.Storm, Is.EqualTo(3));
        }

        [Test]
        public void Apply_NoStorm_DealsOnlyBaseDamage()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeUpToStormBonusDamageEffect(baseAmount: 5, maxConsume: 2, bonusPerPoint: 3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(37));
        }
    }
}
