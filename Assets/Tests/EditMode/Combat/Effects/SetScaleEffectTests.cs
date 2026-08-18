using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class SetScaleEffectTests
    {
        [Test]
        public void Apply_SetsPlayerScaleToGivenValue()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new SetScaleEffect(0);

            effect.Apply(context);

            Assert.That(player.Scale, Is.EqualTo(0));
        }
    }
}
