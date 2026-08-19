using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConditionalGainBlockIfChangedFormEffectTests
    {
        [Test]
        public void Apply_ChangedFormThisTurn_GrantsBonusBlock()
        {
            var player = new Player(startingEnergy: 3);
            player.ChangeForm();
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalGainBlockIfChangedFormEffect(baseAmount: 5, bonusAmount: 3);

            effect.Apply(context);

            Assert.That(player.CurrentBlock, Is.EqualTo(8));
        }

        [Test]
        public void Apply_DidNotChangeFormThisTurn_GrantsOnlyBaseBlock()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalGainBlockIfChangedFormEffect(baseAmount: 5, bonusAmount: 3);

            effect.Apply(context);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
        }
    }
}
