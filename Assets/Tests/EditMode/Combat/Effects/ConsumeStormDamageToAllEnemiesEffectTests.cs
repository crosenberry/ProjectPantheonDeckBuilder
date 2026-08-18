using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ConsumeStormDamageToAllEnemiesEffectTests
    {
        [Test]
        public void Apply_DealsScaledDamageToAllEnemies()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(3);
            var enemyA = new Enemy(maxHP: 42);
            var enemyB = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemyA, new[] { enemyA, enemyB });
            var effect = new ConsumeStormDamageToAllEnemiesEffect(damagePerPoint: 4);

            effect.Apply(context);

            Assert.That(enemyA.CurrentHP, Is.EqualTo(30));
            Assert.That(enemyB.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void Apply_ConsumesAllStorm()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeStormDamageToAllEnemiesEffect(damagePerPoint: 4);

            effect.Apply(context);

            Assert.That(player.Storm, Is.EqualTo(0));
        }

        [Test]
        public void Apply_NoStorm_DealsNoDamage()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ConsumeStormDamageToAllEnemiesEffect(damagePerPoint: 4);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
        }
    }
}
