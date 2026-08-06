using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConditionalApplyStatusEffectTests
    {
        [Test]
        public void Apply_ConditionMet_AppliesStatus()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            enemy.ApplyExposed(1);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalApplyStatusEffect(StatusType.Exposed, StatusType.Drained, 1, EffectTarget.Enemy);

            effect.Apply(context);

            Assert.That(enemy.Drained, Is.EqualTo(1));
        }

        [Test]
        public void Apply_ConditionNotMet_DoesNotApplyStatus()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConditionalApplyStatusEffect(StatusType.Exposed, StatusType.Drained, 1, EffectTarget.Enemy);

            effect.Apply(context);

            Assert.That(enemy.Drained, Is.EqualTo(0));
        }
    }
}
