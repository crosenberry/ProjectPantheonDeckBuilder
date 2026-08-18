using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Blessings.Thor
{
    public class ThorCardsTests
    {
        private static void Play(Player player, Card card, Enemy enemy)
        {
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            CombatResolver.PlayCard(player, card, enemy);
        }

        [Test]
        public void HammerStrike_DealsSixDamage()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.HammerStrike();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void StormWard_GrantsBlockAndStormAndIsTaggedStormGenerating()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.StormWard();

            Play(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
            Assert.That(player.Storm, Is.EqualTo(1));
            Assert.That(card.Tags.Contains(CardTag.StormGenerating), Is.True);
        }

        [Test]
        public void MjolnirsCall_DealsDamageAndGrantsStorm()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.MjolnirsCall();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
            Assert.That(player.Storm, Is.EqualTo(2));
        }

        [Test]
        public void ShieldWall_GrantsBlockAndStorm()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.ShieldWall();

            Play(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(9));
            Assert.That(player.Storm, Is.EqualTo(2));
        }

        [Test]
        public void Deflect_LowStorm_GrantsBaseBlock()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.Deflect();

            Play(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
        }

        [Test]
        public void Deflect_HighStorm_GrantsBonusBlock()
        {
            var player = new Player(startingEnergy: 1);
            player.GainStorm(5);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.Deflect();

            Play(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(8));
        }

        [Test]
        public void ThunderousRetort_DealsDamageEqualToCurrentBlock()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.ThunderousRetort();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            player.GainBlock(10);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(32));
        }

        [Test]
        public void TitansBulwark_GrantsBlockAndStormAndIsTaggedExhaust()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.TitansBulwark();

            Play(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(15));
            Assert.That(player.Storm, Is.EqualTo(4));
            Assert.That(card.Tags.Contains(CardTag.Exhaust), Is.True);
        }

        [Test]
        public void Thunderclap_ConsumesStormAndDamagesAllEnemies()
        {
            var player = new Player(startingEnergy: 2);
            player.GainStorm(3);
            var enemyA = new Enemy(maxHP: 42);
            var enemyB = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.Thunderclap();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemyA, new[] { enemyA, enemyB });

            Assert.That(enemyA.CurrentHP, Is.EqualTo(30));
            Assert.That(enemyB.CurrentHP, Is.EqualTo(30));
            Assert.That(player.Storm, Is.EqualTo(0));
        }

        [Test]
        public void ChargedStrike_DealsDamageAndGrantsStorm()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.ChargedStrike();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
            Assert.That(player.Storm, Is.EqualTo(1));
        }

        [Test]
        public void DischargeBolt_HasStorm_ConsumesAndDealsMainDamage()
        {
            var player = new Player(startingEnergy: 1);
            player.GainStorm(1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.DischargeBolt();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
            Assert.That(player.Storm, Is.EqualTo(0));
        }

        [Test]
        public void DischargeBolt_NoStorm_DealsFallbackDamage()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.DischargeBolt();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(38));
        }

        [Test]
        public void StaticGrip_GrantsStormAndDrawsCard()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.StaticGrip();
            var extraCard = Core.Blessings.Thor.ThorCards.HammerStrike();
            player.AddToDrawPile(new[] { card, extraCard });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(player.Storm, Is.EqualTo(1));
            Assert.That(player.Hand.Contains(extraCard), Is.True);
        }

        [Test]
        public void Galvanize_GrantsThreeStormAndDrawsCard()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.Galvanize();
            var extraCard = Core.Blessings.Thor.ThorCards.HammerStrike();
            player.AddToDrawPile(new[] { card, extraCard });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(player.Storm, Is.EqualTo(3));
            Assert.That(player.Hand.Contains(extraCard), Is.True);
        }

        [Test]
        public void Stormbrand_HasStorm_ConsumesAllForScaledDamageInsteadOfBase()
        {
            var player = new Player(startingEnergy: 1);
            player.GainStorm(3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.Stormbrand();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(27));
            Assert.That(player.Storm, Is.EqualTo(0));
        }

        [Test]
        public void Stormbrand_NoStorm_DealsBaseDamage()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.Stormbrand();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(39));
        }

        [Test]
        public void RecklessSwing_DealsDamageAndCostsPlayerHP()
        {
            var player = new Player(startingEnergy: 1, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.RecklessSwing();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(32));
            Assert.That(player.CurrentHP, Is.EqualTo(67));
        }

        [Test]
        public void Bloodrage_GrantsStrengthAndCostsPlayerHP()
        {
            var player = new Player(startingEnergy: 1, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.Bloodrage();

            Play(player, card, enemy);

            Assert.That(player.Strength, Is.EqualTo(2));
            Assert.That(player.CurrentHP, Is.EqualTo(67));
        }

        [Test]
        public void HeadlongCharge_DealsFourteenDamage()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.HeadlongCharge();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(28));
        }

        [Test]
        public void FeralRoar_DealsDamageAndGrantsStrength()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.FeralRoar();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
            Assert.That(player.Strength, Is.EqualTo(2));
        }

        [Test]
        public void YmirsWrath_DealsDamageAndCostsPlayerHP()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Thor.ThorCards.YmirsWrath();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(22));
            Assert.That(player.CurrentHP, Is.EqualTo(65));
        }

        [Test]
        public void StarterDeck_ReturnsTenCards()
        {
            var deck = Core.Blessings.Thor.ThorCards.StarterDeck().ToList();

            Assert.That(deck.Count, Is.EqualTo(10));
        }

        [Test]
        public void StarterDeck_HasCorrectComposition()
        {
            var deck = Core.Blessings.Thor.ThorCards.StarterDeck().ToList();

            Assert.That(deck.Count(c => c.Name == "Hammer Strike"), Is.EqualTo(5));
            Assert.That(deck.Count(c => c.Name == "Storm Ward"), Is.EqualTo(4));
            Assert.That(deck.Count(c => c.Name == "Mjolnir's Call"), Is.EqualTo(1));
        }
    }
}
