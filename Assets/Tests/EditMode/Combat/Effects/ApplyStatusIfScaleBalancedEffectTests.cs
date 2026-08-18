using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ApplyStatusIfScaleBalancedEffectTests
    {
        [Test]
        public void Apply_ScaleBalanced_AppliesStatusToEnemy()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ApplyStatusIfScaleBalancedEffect(StatusType.Exposed, 1, EffectTarget.Enemy);

            effect.Apply(context);

            Assert.That(enemy.Exposed, Is.EqualTo(1));
        }

        [Test]
        public void Apply_ScaleNotBalanced_DoesNotApplyStatus()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(2);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ApplyStatusIfScaleBalancedEffect(StatusType.Exposed, 1, EffectTarget.Enemy);

            effect.Apply(context);

            Assert.That(enemy.Exposed, Is.EqualTo(0));
        }

        [Test]
        public void Apply_ScaleAtBalancedBoundary_AppliesStatus()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(1);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ApplyStatusIfScaleBalancedEffect(StatusType.Exposed, 1, EffectTarget.Enemy);

            effect.Apply(context);

            Assert.That(enemy.Exposed, Is.EqualTo(1));
        }
    }
}
