using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConsumeStormDamageDoubleIfVolleyThresholdEffectTests
    {
        [Test]
        public void Apply_VolleyBelowThreshold_HitsOnce()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(2);
            player.GainVolley(2);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeStormDamageDoubleIfVolleyThresholdEffect(damagePerPoint: 6, volleyThreshold: 3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void Apply_VolleyAtThreshold_HitsTwice()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(2);
            player.GainVolley(3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeStormDamageDoubleIfVolleyThresholdEffect(damagePerPoint: 6, volleyThreshold: 3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(18));
        }

        [Test]
        public void Apply_ConsumesAllStorm()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(2);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeStormDamageDoubleIfVolleyThresholdEffect(damagePerPoint: 6, volleyThreshold: 3);

            effect.Apply(context);

            Assert.That(player.Storm, Is.EqualTo(0));
        }
    }
}
