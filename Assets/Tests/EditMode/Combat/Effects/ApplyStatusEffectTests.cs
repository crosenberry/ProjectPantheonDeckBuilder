using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ApplyStatusEffectTests
    {
        [Test]
        public void Apply_StrengthSelf_IncreasesPlayerStrength()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ApplyStatusEffect(StatusType.Strength, 2, EffectTarget.Self);

            effect.Apply(context);

            Assert.That(player.Strength, Is.EqualTo(2));
        }

        [Test]
        public void Apply_StrengthEnemy_IncreasesEnemyStrength()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ApplyStatusEffect(StatusType.Strength, 2, EffectTarget.Enemy);

            effect.Apply(context);

            Assert.That(enemy.Strength, Is.EqualTo(2));
        }

        [Test]
        public void Apply_ExposedEnemy_IncreasesEnemyExposed()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ApplyStatusEffect(StatusType.Exposed, 2, EffectTarget.Enemy);

            effect.Apply(context);

            Assert.That(enemy.Exposed, Is.EqualTo(2));
        }

        [Test]
        public void Apply_ExposedSelf_IncreasesPlayerExposed()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ApplyStatusEffect(StatusType.Exposed, 2, EffectTarget.Self);

            effect.Apply(context);

            Assert.That(player.Exposed, Is.EqualTo(2));
        }

        [Test]
        public void Apply_DrainedEnemy_IncreasesEnemyDrained()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ApplyStatusEffect(StatusType.Drained, 2, EffectTarget.Enemy);

            effect.Apply(context);

            Assert.That(enemy.Drained, Is.EqualTo(2));
        }

        [Test]
        public void Apply_Sundered_IncreasesPlayerSundered()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ApplyStatusEffect(StatusType.Sundered, 2, EffectTarget.Self);

            effect.Apply(context);

            Assert.That(player.Sundered, Is.EqualTo(2));
        }
    }
}
