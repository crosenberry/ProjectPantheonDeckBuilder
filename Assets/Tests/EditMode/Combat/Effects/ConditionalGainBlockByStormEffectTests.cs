using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConditionalGainBlockByStormEffectTests
    {
        [Test]
        public void Apply_StormBelowThreshold_GrantsBaseAmount()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalGainBlockByStormEffect(baseAmount: 5, thresholdAmount: 8, stormThreshold: 5);

            effect.Apply(context);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
        }

        [Test]
        public void Apply_StormAtThreshold_GrantsThresholdAmount()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(5);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalGainBlockByStormEffect(baseAmount: 5, thresholdAmount: 8, stormThreshold: 5);

            effect.Apply(context);

            Assert.That(player.CurrentBlock, Is.EqualTo(8));
        }

        [Test]
        public void Apply_StormAboveThreshold_GrantsThresholdAmount()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(9);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalGainBlockByStormEffect(baseAmount: 5, thresholdAmount: 8, stormThreshold: 5);

            effect.Apply(context);

            Assert.That(player.CurrentBlock, Is.EqualTo(8));
        }
    }
}
