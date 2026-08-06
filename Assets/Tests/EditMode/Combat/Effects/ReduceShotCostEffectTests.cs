using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ReduceShotCostEffectTests
    {
        [Test]
        public void Apply_IncreasesPlayerShotCostReductionThisTurn()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ReduceShotCostEffect(1);

            effect.Apply(context);

            Assert.That(player.ShotCostReductionThisTurn, Is.EqualTo(1));
        }
    }
}
