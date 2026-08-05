using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class GainVolleyEffectTests
    {
        [Test]
        public void Apply_GrantsVolleyToPlayer()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new GainVolleyEffect(2);

            effect.Apply(context);

            Assert.That(player.Volley, Is.EqualTo(2));
        }
    }
}
