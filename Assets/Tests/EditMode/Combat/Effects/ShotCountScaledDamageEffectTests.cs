using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ShotCountScaledDamageEffectTests
    {
        [Test]
        public void Apply_NoShotsPlayedYet_DealsBaseDamage()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ShotCountScaledDamageEffect(baseAmount: 4, bonusPerShot: 2);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(38));
        }

        [Test]
        public void Apply_ShotsAlreadyPlayedThisTurn_AddsBonusDamage()
        {
            var player = new Player(startingEnergy: 3);
            player.RecordShotPlayed();
            player.RecordShotPlayed();
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ShotCountScaledDamageEffect(baseAmount: 4, bonusPerShot: 2);

            effect.Apply(context);

            Assert.That(enemy.CurrentHP, Is.EqualTo(34));
        }
    }
}
