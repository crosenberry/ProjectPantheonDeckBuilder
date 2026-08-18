using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class LoseHPEffectTests
    {
        [Test]
        public void Apply_ReducesPlayerCurrentHP()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new LoseHPEffect(5);

            effect.Apply(context);

            Assert.That(player.CurrentHP, Is.EqualTo(65));
        }

        [Test]
        public void Apply_IgnoresBlock()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            player.GainBlock(10);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new LoseHPEffect(5);

            effect.Apply(context);

            Assert.That(player.CurrentHP, Is.EqualTo(65));
            Assert.That(player.CurrentBlock, Is.EqualTo(10));
        }
    }
}
