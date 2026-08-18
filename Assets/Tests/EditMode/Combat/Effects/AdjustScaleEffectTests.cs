using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class AdjustScaleEffectTests
    {
        [Test]
        public void Apply_PositiveAmount_IncreasesPlayerScale()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new AdjustScaleEffect(1);

            effect.Apply(context);

            Assert.That(player.Scale, Is.EqualTo(1));
        }

        [Test]
        public void Apply_NegativeAmount_DecreasesPlayerScale()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new AdjustScaleEffect(-1);

            effect.Apply(context);

            Assert.That(player.Scale, Is.EqualTo(-1));
        }
    }
}
