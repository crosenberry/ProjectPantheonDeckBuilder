using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConsumeVolleyDamageEffectTests
    {
        [Test]
        public void Apply_ConsumesVolleyAndDealsScaledDamage()
        {
            var player = new Player(startingEnergy: 3);
            player.GainVolley(3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeVolleyDamageEffect(damagePerPoint: 5);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(27));
        }

        [Test]
        public void Apply_ResetsVolleyToZeroAfterConsuming()
        {
            var player = new Player(startingEnergy: 3);
            player.GainVolley(3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeVolleyDamageEffect(damagePerPoint: 5);

            effect.Apply(context);

            Assert.That(player.Volley, Is.EqualTo(0));
        }

        [Test]
        public void Apply_ZeroVolley_TreatsAsMinimumOnePoint()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeVolleyDamageEffect(damagePerPoint: 5);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(37));
        }

        [Test]
        public void Apply_RespectsStrengthModifier()
        {
            var player = new Player(startingEnergy: 3);
            player.GainVolley(2);
            player.GainStrength(3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeVolleyDamageEffect(damagePerPoint: 5);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(29));
        }
    }
}
