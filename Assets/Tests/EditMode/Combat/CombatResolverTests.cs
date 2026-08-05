using System;
using System.Linq;
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
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void PlayCard_SufficientEnergy_SpendsEnergyEqualToCardCost()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = new Card("Quick Shot", energyCost: 1, damageAmount: 6);
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(player.CurrentEnergy, Is.EqualTo(2));
        }

        [Test]
        public void PlayCard_InsufficientEnergy_ThrowsAndTargetHPUnchanged()
        {
            var player = new Player(startingEnergy: 0);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = new Card("Quick Shot", energyCost: 1, damageAmount: 6);
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            Assert.Throws<InvalidOperationException>(() => CombatResolver.PlayCard(player, quickShot, enemy));
            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
        }

        [Test]
        public void PlayCard_CardInHand_RemovesCardFromHand()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = new Card("Quick Shot", energyCost: 1, damageAmount: 6);
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(player.Hand.Contains(quickShot), Is.False);
        }

        [Test]
        public void PlayCard_CardInHand_MovesCardToDiscardPile()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = new Card("Quick Shot", energyCost: 1, damageAmount: 6);
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(player.DiscardPile.Contains(quickShot), Is.True);
        }

        [Test]
        public void PlayCard_CardNotInHand_ThrowsAndStateUnchanged()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var phantomCard = new Card("Phantom Card", energyCost: 1, damageAmount: 6);

            Assert.Throws<InvalidOperationException>(() => CombatResolver.PlayCard(player, phantomCard, enemy));
            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
            Assert.That(player.CurrentEnergy, Is.EqualTo(3));
        }

        [Test]
        public void PlayCard_CardGrantsBlock_IncreasesPlayerBlock()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var sideStep = new Card("Side Step", energyCost: 1, damageAmount: 0, blockAmount: 5);
            player.AddToDrawPile(new[] { sideStep });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, sideStep, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
        }

        [Test]
        public void PlayCard_BlockOnlyCard_DoesNotDamageTarget()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var sideStep = new Card("Side Step", energyCost: 1, damageAmount: 0, blockAmount: 5);
            player.AddToDrawPile(new[] { sideStep });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, sideStep, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
        }

        [Test]
        public void EnemyAttack_DealsAttackDamageToPlayer()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);

            CombatResolver.EnemyAttack(enemy, player);

            Assert.That(player.CurrentHP, Is.EqualTo(60));
        }

        [Test]
        public void EnemyAttack_DamageMitigatedByPlayerBlock()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            player.GainBlock(5);
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);

            CombatResolver.EnemyAttack(enemy, player);

            Assert.That(player.CurrentBlock, Is.EqualTo(0));
            Assert.That(player.CurrentHP, Is.EqualTo(65));
        }

        [Test]
        public void PlayCard_PlayerHasStrength_AddsFlatDamageToAttackCard()
        {
            var player = new Player(startingEnergy: 1);
            player.GainStrength(3);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = new Card("Quick Shot", energyCost: 1, damageAmount: 6);
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
        }

        [Test]
        public void PlayCard_PlayerDrained_ReducesAttackDamageByQuarterRoundedDown()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var strike = new Card("Strike", energyCost: 1, damageAmount: 8);
            player.AddToDrawPile(new[] { strike });
            player.StartTurn(cardsToDraw: 1);
            player.ApplyDrained(1);

            CombatResolver.PlayCard(player, strike, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void PlayCard_TargetExposed_IncreasesDamageTakenByHalfRoundedDown()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            enemy.ApplyExposed(1);
            var strike = new Card("Strike", energyCost: 1, damageAmount: 8);
            player.AddToDrawPile(new[] { strike });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, strike, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void PlayCard_PlayerHasStrength_DoesNotAffectBlockOnlyCard()
        {
            var player = new Player(startingEnergy: 1);
            player.GainStrength(3);
            var enemy = new Enemy(maxHP: 42);
            var sideStep = new Card("Side Step", energyCost: 1, damageAmount: 0, blockAmount: 5);
            player.AddToDrawPile(new[] { sideStep });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, sideStep, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
        }

        [Test]
        public void PlayCard_PlayerSundered_ReducesBlockGainedByQuarterRoundedDown()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var sideStep = new Card("Side Step", energyCost: 1, damageAmount: 0, blockAmount: 8);
            player.AddToDrawPile(new[] { sideStep });
            player.StartTurn(cardsToDraw: 1);
            player.ApplySundered(1);

            CombatResolver.PlayCard(player, sideStep, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(6));
        }

        [Test]
        public void PlayCard_PlayerSundered_DoesNotAffectAttackDamage()
        {
            var player = new Player(startingEnergy: 1);
            player.ApplySundered(2);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = new Card("Quick Shot", energyCost: 1, damageAmount: 6);
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void EnemyAttack_EnemyHasStrength_AddsFlatDamage()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);
            enemy.GainStrength(2);

            CombatResolver.EnemyAttack(enemy, player);

            Assert.That(player.CurrentHP, Is.EqualTo(58));
        }

        [Test]
        public void EnemyAttack_EnemyDrained_ReducesDamageByQuarterRoundedDown()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 8);
            enemy.ApplyDrained(1);

            CombatResolver.EnemyAttack(enemy, player);

            Assert.That(player.CurrentHP, Is.EqualTo(64));
        }

        [Test]
        public void EnemyAttack_PlayerExposed_IncreasesDamageByHalfRoundedDown()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            player.ApplyExposed(1);
            var enemy = new Enemy(maxHP: 42, attackDamage: 8);

            CombatResolver.EnemyAttack(enemy, player);

            Assert.That(player.CurrentHP, Is.EqualTo(58));
        }
    }
}
