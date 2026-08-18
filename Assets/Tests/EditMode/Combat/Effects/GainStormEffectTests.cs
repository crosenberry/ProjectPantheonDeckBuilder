using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class GainStormEffectTests
    {
        [Test]
        public void Apply_GrantsStormToPlayer()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new GainStormEffect(2);

            effect.Apply(context);

            Assert.That(player.Storm, Is.EqualTo(2));
        }
    }
}
