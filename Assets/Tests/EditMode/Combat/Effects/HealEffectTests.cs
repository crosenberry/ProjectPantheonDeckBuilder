using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class HealEffectTests
    {
        [Test]
        public void Apply_HealsPlayer()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            player.TakeDamage(20);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new HealEffect(8);

            effect.Apply(context);

            Assert.That(player.CurrentHP, Is.EqualTo(58));
        }

        [Test]
        public void Apply_DoesNotExceedMaxHP()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new HealEffect(8);

            effect.Apply(context);

            Assert.That(player.CurrentHP, Is.EqualTo(70));
        }
    }
}
