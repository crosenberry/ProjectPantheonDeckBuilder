using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Blessings.Anubis
{
    public class AnubisCardsTests
    {
        private static void Play(Player player, Card card, Enemy enemy)
        {
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            CombatResolver.PlayCard(player, card, enemy);
        }

        [Test]
        public void JackalsBite_DealsSixDamage()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.JackalsBite();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void CanopicWard_GrantsFiveBlock()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.CanopicWard();

            Play(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
        }

        [Test]
        public void ScalesOfJudgment_DealsDamageAndIncreasesScale()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.ScalesOfJudgment();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(34));
            Assert.That(player.Scale, Is.EqualTo(1));
        }

        [Test]
        public void EvenKeel_ScaleBalanced_DrawsTwoAdditionalCards()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.EvenKeel();
            var extraA = Core.Blessings.Anubis.AnubisCards.JackalsBite();
            var extraB = Core.Blessings.Anubis.AnubisCards.CanopicWard();
            player.AddToDrawPile(new[] { card, extraA, extraB });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(player.Hand.Contains(extraA), Is.True);
            Assert.That(player.Hand.Contains(extraB), Is.True);
        }

        [Test]
        public void EvenKeel_ScaleNotBalanced_DrawsOnlyOneAdditionalCard()
        {
            var player = new Player(startingEnergy: 1);
            player.AdjustScale(3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.EvenKeel();
            var extraA = Core.Blessings.Anubis.AnubisCards.JackalsBite();
            var extraB = Core.Blessings.Anubis.AnubisCards.CanopicWard();
            player.AddToDrawPile(new[] { card, extraA, extraB });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(player.Hand.Contains(extraA), Is.True);
            Assert.That(player.Hand.Contains(extraB), Is.False);
        }

        [Test]
        public void MaatsFeather_ScaleBalanced_DealsDamageAndAppliesExposed()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.MaatsFeather();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(38));
            Assert.That(enemy.Exposed, Is.EqualTo(1));
        }

        [Test]
        public void MaatsFeather_ScaleNotBalanced_DealsDamageOnly()
        {
            var player = new Player(startingEnergy: 1);
            player.AdjustScale(-3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.MaatsFeather();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(38));
            Assert.That(enemy.Exposed, Is.EqualTo(0));
        }

        [Test]
        public void Equilibrium_IsPowerTaggedExhaust()
        {
            var card = Core.Blessings.Anubis.AnubisCards.Equilibrium();

            Assert.That(card.Type, Is.EqualTo(CardType.Power));
            Assert.That(card.Tags.Contains(CardTag.Exhaust), Is.True);
        }

        [Test]
        public void Equilibrium_ScaleBalancedAtTurnEnd_GrantsBlock()
        {
            var player = new Player(startingEnergy: 1, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 0);
            var card = Core.Blessings.Anubis.AnubisCards.Equilibrium();
            player.AddToDrawPile(new[] { card });
            var combat = new CombatEncounter(player, enemy);
            combat.StartPlayerTurn(cardsToDraw: 1);
            combat.PlayCard(card, enemy);

            combat.EndPlayerTurn();

            Assert.That(player.CurrentBlock, Is.EqualTo(3));
        }

        [Test]
        public void ScaleTipper_ResetsScaleAndDrawsTwoCards()
        {
            var player = new Player(startingEnergy: 1);
            player.AdjustScale(3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.ScaleTipper();
            var extraA = Core.Blessings.Anubis.AnubisCards.JackalsBite();
            var extraB = Core.Blessings.Anubis.AnubisCards.CanopicWard();
            player.AddToDrawPile(new[] { card, extraA, extraB });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(player.Scale, Is.EqualTo(0));
            Assert.That(player.Hand.Contains(extraA), Is.True);
            Assert.That(player.Hand.Contains(extraB), Is.True);
        }

        [Test]
        public void JudgmentIncarnate_ScaleZero_DealsMaxDamage()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.JudgmentIncarnate();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(22));
        }

        [Test]
        public void AmmitsHunger_DealsDamageAndDecreasesScale()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.AmmitsHunger();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(35));
            Assert.That(player.Scale, Is.EqualTo(-1));
        }

        [Test]
        public void ChaosRite_CostsHPAndDecreasesScale()
        {
            var player = new Player(startingEnergy: 1, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.ChaosRite();

            Play(player, card, enemy);

            Assert.That(player.CurrentHP, Is.EqualTo(68));
            Assert.That(player.Scale, Is.EqualTo(-2));
        }

        [Test]
        public void SerpentsBite_ScaleNegative_DealsBonusDamage()
        {
            var player = new Player(startingEnergy: 1);
            player.AdjustScale(-1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.SerpentsBite();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(34));
        }

        [Test]
        public void SerpentsBite_ScaleNotNegative_DealsBaseDamage()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.SerpentsBite();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(37));
        }

        [Test]
        public void ChaosboundStrike_ScaleAtOrBelowThreshold_DealsBonusDamage()
        {
            var player = new Player(startingEnergy: 1);
            player.AdjustScale(-3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.ChaosboundStrike();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void ChaosboundStrike_ScaleAboveThreshold_DealsBaseDamage()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.ChaosboundStrike();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void DevourersToll_ScaleNegative_DealsScaledDamageAndAppliesExposed()
        {
            var player = new Player(startingEnergy: 2);
            player.AdjustScale(-3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.DevourersToll();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
            Assert.That(enemy.Exposed, Is.EqualTo(3));
        }

        [Test]
        public void DevourersToll_ScalePositive_DealsScaledDamageWithoutExposed()
        {
            var player = new Player(startingEnergy: 2);
            player.AdjustScale(3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.DevourersToll();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
            Assert.That(enemy.Exposed, Is.EqualTo(0));
        }

        [Test]
        public void MaatsShield_GrantsBlockAndIncreasesScale()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.MaatsShield();

            Play(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(7));
            Assert.That(player.Scale, Is.EqualTo(1));
        }

        [Test]
        public void SacredRite_HealsAndIncreasesScale()
        {
            var player = new Player(startingEnergy: 1, startingHP: 70, new SystemRandom());
            player.LoseHP(10);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.SacredRite();

            Play(player, card, enemy);

            Assert.That(player.CurrentHP, Is.EqualTo(64));
            Assert.That(player.Scale, Is.EqualTo(1));
        }

        [Test]
        public void SunboundWard_ScalePositive_GrantsBonusBlock()
        {
            var player = new Player(startingEnergy: 1);
            player.AdjustScale(1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.SunboundWard();

            Play(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(9));
        }

        [Test]
        public void SunboundWard_ScaleNotPositive_GrantsBaseBlock()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.SunboundWard();

            Play(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(6));
        }

        [Test]
        public void OsirianRenewal_HealsAndIncreasesScale()
        {
            var player = new Player(startingEnergy: 2, startingHP: 70, new SystemRandom());
            player.LoseHP(20);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.OsirianRenewal();

            Play(player, card, enemy);

            Assert.That(player.CurrentHP, Is.EqualTo(58));
            Assert.That(player.Scale, Is.EqualTo(3));
        }

        [Test]
        public void MaatsAscension_ScalePositive_DealsDamageToAllEnemiesAndResetsScale()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(3);
            var enemyA = new Enemy(maxHP: 42);
            var enemyB = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.MaatsAscension();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemyA, new[] { enemyA, enemyB });

            Assert.That(enemyA.CurrentHP, Is.EqualTo(27));
            Assert.That(enemyB.CurrentHP, Is.EqualTo(27));
            Assert.That(player.Scale, Is.EqualTo(0));
        }

        [Test]
        public void MaatsAscension_ScaleNotPositive_DealsNoDamage()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Anubis.AnubisCards.MaatsAscension();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
        }

        [Test]
        public void StarterDeck_ReturnsTenCards()
        {
            var deck = Core.Blessings.Anubis.AnubisCards.StarterDeck().ToList();

            Assert.That(deck.Count, Is.EqualTo(10));
        }

        [Test]
        public void StarterDeck_HasCorrectComposition()
        {
            var deck = Core.Blessings.Anubis.AnubisCards.StarterDeck().ToList();

            Assert.That(deck.Count(c => c.Name == "Jackal's Bite"), Is.EqualTo(5));
            Assert.That(deck.Count(c => c.Name == "Canopic Ward"), Is.EqualTo(4));
            Assert.That(deck.Count(c => c.Name == "Scales of Judgment"), Is.EqualTo(1));
        }

        [Test]
        public void StarterDeck_ReturnsDistinctCardInstances()
        {
            var deck = Core.Blessings.Anubis.AnubisCards.StarterDeck().ToList();

            var distinctCount = deck.Distinct().Count();

            Assert.That(distinctCount, Is.EqualTo(10));
        }
    }
}
