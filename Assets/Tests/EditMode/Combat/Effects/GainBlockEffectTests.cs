using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class GainBlockEffectTests
    {
        [Test]
        public void Apply_GrantsBlockToPlayer()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new GainBlockEffect(5);

            effect.Apply(context);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
        }

        [Test]
        public void Apply_PlayerSundered_ReducesBlockByQuarterRoundedDown()
        {
            var player = new Player(startingEnergy: 3);
            player.ApplySundered(1);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new GainBlockEffect(8);

            effect.Apply(context);

            Assert.That(player.CurrentBlock, Is.EqualTo(6));
        }

        [Test]
        public void Apply_PlayerInImmortalForm_GrantsOneAdditionalBlock()
        {
            var player = new Player(startingEnergy: 3);
            player.ChangeForm(Form.Immortal);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new GainBlockEffect(5);

            effect.Apply(context);

            Assert.That(player.CurrentBlock, Is.EqualTo(6));
        }
    }
}
