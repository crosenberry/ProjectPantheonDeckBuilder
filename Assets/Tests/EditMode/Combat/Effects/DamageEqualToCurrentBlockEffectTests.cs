using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class DamageEqualToCurrentBlockEffectTests
    {
        [Test]
        public void Apply_DealsDamageEqualToCurrentBlock()
        {
            var player = new Player(startingEnergy: 3);
            player.GainBlock(12);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DamageEqualToCurrentBlockEffect();

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void Apply_NoBlock_DealsNoDamage()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DamageEqualToCurrentBlockEffect();

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
        }
    }
}
