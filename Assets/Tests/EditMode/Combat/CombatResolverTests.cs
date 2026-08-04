using System;
using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat
{
    public class CombatResolverTests
    {
        [Test]
        public void PlayCard_SufficientEnergy_DealsCardDamageToTarget()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = new Card("Quick Shot", energyCost: 1, damageAmount: 6);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void PlayCard_SufficientEnergy_SpendsEnergyEqualToCardCost()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = new Card("Quick Shot", energyCost: 1, damageAmount: 6);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(player.CurrentEnergy, Is.EqualTo(2));
        }

        [Test]
        public void PlayCard_InsufficientEnergy_ThrowsAndTargetHPUnchanged()
        {
            var player = new Player(startingEnergy: 0);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = new Card("Quick Shot", energyCost: 1, damageAmount: 6);

            Assert.Throws<InvalidOperationException>(() => CombatResolver.PlayCard(player, quickShot, enemy));
            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
        }
    }
}
