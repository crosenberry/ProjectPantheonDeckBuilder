using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConditionalGainBlockIfScalePositiveEffectTests
    {
        [Test]
        public void Apply_ScalePositive_GrantsBonusBlock()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(1);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalGainBlockIfScalePositiveEffect(baseAmount: 6, bonusAmount: 3);

            effect.Apply(context);

            Assert.That(player.CurrentBlock, Is.EqualTo(9));
        }

        [Test]
        public void Apply_ScaleZeroOrNegative_GrantsOnlyBaseBlock()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalGainBlockIfScalePositiveEffect(baseAmount: 6, bonusAmount: 3);

            effect.Apply(context);

            Assert.That(player.CurrentBlock, Is.EqualTo(6));
        }
    }
}
