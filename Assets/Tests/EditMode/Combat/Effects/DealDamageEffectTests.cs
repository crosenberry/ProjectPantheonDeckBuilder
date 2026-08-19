using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class DealDamageEffectTests
    {
        [Test]
        public void Apply_DealsBaseDamageToEnemy()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DealDamageEffect(6);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void Apply_PlayerHasStrength_AddsFlatDamage()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStrength(3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DealDamageEffect(6);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
        }

        [Test]
        public void Apply_PlayerDrained_ReducesDamageByQuarterRoundedDown()
        {
            var player = new Player(startingEnergy: 3);
            player.ApplyDrained(1);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DealDamageEffect(8);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void Apply_EnemyExposed_IncreasesDamageByHalfRoundedDown()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            enemy.ApplyExposed(1);
            var context = new CardEffectContext(player, enemy);
            var effect = new DealDamageEffect(8);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void Apply_PlayerInBeastForm_HitsTwice()
        {
            var player = new Player(startingEnergy: 3);
            player.ChangeForm(Form.Beast);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DealDamageEffect(6);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void Apply_PlayerInImmortalForm_ReducesDamageByThree()
        {
            var player = new Player(startingEnergy: 3);
            player.ChangeForm(Form.Immortal);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DealDamageEffect(6);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(39));
        }

        [Test]
        public void Apply_PlayerInImmortalFormWithLowDamage_FlooredAtOne()
        {
            var player = new Player(startingEnergy: 3);
            player.ChangeForm(Form.Immortal);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DealDamageEffect(2);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(41));
        }
    }
}
