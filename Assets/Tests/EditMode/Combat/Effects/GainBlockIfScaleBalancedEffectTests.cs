using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class GainBlockIfScaleBalancedEffectTests
    {
        [Test]
        public void Apply_ScaleBalanced_GrantsBlock()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new GainBlockIfScaleBalancedEffect(3);

            effect.Apply(context);

            Assert.That(player.CurrentBlock, Is.EqualTo(3));
        }

        [Test]
        public void Apply_ScaleNotBalanced_GrantsNoBlock()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(-2);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new GainBlockIfScaleBalancedEffect(3);

            effect.Apply(context);

            Assert.That(player.CurrentBlock, Is.EqualTo(0));
        }

        [Test]
        public void Apply_PlayerSundered_ReducesBlockByQuarterRoundedDown()
        {
            var player = new Player(startingEnergy: 3);
            player.ApplySundered(1);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new GainBlockIfScaleBalancedEffect(8);

            effect.Apply(context);

            Assert.That(player.CurrentBlock, Is.EqualTo(6));
        }
    }
}
