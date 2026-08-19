using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class MultiHitDamageEffectTests
    {
        [Test]
        public void Apply_DealsDamageOncePerHit()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new MultiHitDamageEffect(amount: 8, hitCount: 3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(18));
        }

        [Test]
        public void Apply_PlayerInBeastForm_StaysAtPrintedHitCount()
        {
            var player = new Player(startingEnergy: 3);
            player.ChangeForm(Form.Beast);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new MultiHitDamageEffect(amount: 8, hitCount: 3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(18));
        }

        [Test]
        public void Apply_PlayerInImmortalForm_ReducesDamageByThreePerHit()
        {
            var player = new Player(startingEnergy: 3);
            player.ChangeForm(Form.Immortal);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new MultiHitDamageEffect(amount: 8, hitCount: 3);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(27));
        }
    }
}
