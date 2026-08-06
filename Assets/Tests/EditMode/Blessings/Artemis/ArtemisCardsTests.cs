using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Blessings.Artemis
{
    public class ArtemisCardsTests
    {
        [Test]
        public void QuickShot_IsAttackTaggedShot()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.QuickShot();

            Assert.That(card.Type, Is.EqualTo(CardType.Attack));
            Assert.That(card.Tags.Contains(CardTag.Shot), Is.True);
        }

        [Test]
        public void QuickShot_DealsSixDamage()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Artemis.ArtemisCards.QuickShot();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void SideStep_IsSkillWithNoTags()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.SideStep();

            Assert.That(card.Type, Is.EqualTo(CardType.Skill));
            Assert.That(card.Tags.Count, Is.EqualTo(0));
        }

        [Test]
        public void SideStep_GrantsFiveBlock()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Artemis.ArtemisCards.SideStep();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
        }

        [Test]
        public void HuntersMark_IsAttackTaggedShot()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.HuntersMark();

            Assert.That(card.Type, Is.EqualTo(CardType.Attack));
            Assert.That(card.Tags.Contains(CardTag.Shot), Is.True);
        }

        [Test]
        public void HuntersMark_DealsDamageAndAppliesExposedToEnemy()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Artemis.ArtemisCards.HuntersMark();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(34));
            Assert.That(enemy.Exposed, Is.EqualTo(2));
        }

        [Test]
        public void Nock_IsSkillWithNoTags()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.Nock();

            Assert.That(card.Type, Is.EqualTo(CardType.Skill));
            Assert.That(card.Tags.Count, Is.EqualTo(0));
        }

        [Test]
        public void Nock_GrantsOneVolley()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Artemis.ArtemisCards.Nock();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(player.Volley, Is.EqualTo(1));
        }

        [Test]
        public void WarningShot_IsAttackTaggedShot()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.WarningShot();

            Assert.That(card.Type, Is.EqualTo(CardType.Attack));
            Assert.That(card.Tags.Contains(CardTag.Shot), Is.True);
        }

        [Test]
        public void WarningShot_DealsDamageAndGrantsVolley()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Artemis.ArtemisCards.WarningShot();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(38));
            Assert.That(player.Volley, Is.EqualTo(1));
        }

        [Test]
        public void SteadyAim_IsSkillWithNoTags()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.SteadyAim();

            Assert.That(card.Type, Is.EqualTo(CardType.Skill));
            Assert.That(card.Tags.Count, Is.EqualTo(0));
        }

        [Test]
        public void SteadyAim_GrantsVolleyAndBlock()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Artemis.ArtemisCards.SteadyAim();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(player.Volley, Is.EqualTo(2));
            Assert.That(player.CurrentBlock, Is.EqualTo(4));
        }

        [Test]
        public void CalledShot_IsAttackTaggedShot()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.CalledShot();

            Assert.That(card.Type, Is.EqualTo(CardType.Attack));
            Assert.That(card.Tags.Contains(CardTag.Shot), Is.True);
        }

        [Test]
        public void CalledShot_VolleyBelowFour_DealsSingleHit()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Artemis.ArtemisCards.CalledShot();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void CalledShot_VolleyFourOrHigher_DealsDoubleHit()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Artemis.ArtemisCards.CalledShot();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            player.GainVolley(4);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void FullDraw_IsAttackWithNoTags()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.FullDraw();

            Assert.That(card.Type, Is.EqualTo(CardType.Attack));
            Assert.That(card.Tags.Count, Is.EqualTo(0));
        }

        [Test]
        public void FullDraw_ConsumesVolleyForScaledDamage()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Artemis.ArtemisCards.FullDraw();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            player.GainVolley(3);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(27));
            Assert.That(player.Volley, Is.EqualTo(0));
        }

        [Test]
        public void LooseArrow_IsAttackTaggedShot()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.LooseArrow();

            Assert.That(card.Type, Is.EqualTo(CardType.Attack));
            Assert.That(card.Tags.Contains(CardTag.Shot), Is.True);
        }

        [Test]
        public void LooseArrow_DealsThreeDamage()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Artemis.ArtemisCards.LooseArrow();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(39));
        }

        [Test]
        public void PreciseShot_IsAttackTaggedShot()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.PreciseShot();

            Assert.That(card.Type, Is.EqualTo(CardType.Attack));
            Assert.That(card.Tags.Contains(CardTag.Shot), Is.True);
        }

        [Test]
        public void PreciseShot_DealsNineDamage()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Artemis.ArtemisCards.PreciseShot();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
        }

        [Test]
        public void Pathfinder_IsSkillWithNoTags()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.Pathfinder();

            Assert.That(card.Type, Is.EqualTo(CardType.Skill));
            Assert.That(card.Tags.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pathfinder_DrawsTwoAndDiscardsOneNonShotCard()
        {
            var player = new Player(startingEnergy: 3);
            var pathfinder = Core.Blessings.Artemis.ArtemisCards.Pathfinder();
            var quickShot = Core.Blessings.Artemis.ArtemisCards.QuickShot();
            var sideStep = Core.Blessings.Artemis.ArtemisCards.SideStep();
            player.AddToDrawPile(new[] { pathfinder });
            player.StartTurn(cardsToDraw: 1);
            player.AddToDrawPile(new[] { quickShot, sideStep });
            var enemy = new Enemy(maxHP: 42);

            CombatResolver.PlayCard(player, pathfinder, enemy);

            Assert.That(player.Hand.Contains(quickShot), Is.True);
            Assert.That(player.Hand.Contains(sideStep), Is.False);
            Assert.That(player.DiscardPile.Contains(sideStep), Is.True);
        }

        [Test]
        public void PointBlank_IsAttackTaggedShot()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.PointBlank();

            Assert.That(card.Type, Is.EqualTo(CardType.Attack));
            Assert.That(card.Tags.Contains(CardTag.Shot), Is.True);
        }

        [Test]
        public void PointBlank_EnemyNotExposed_DealsDamageOnly()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Artemis.ArtemisCards.PointBlank();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(35));
            Assert.That(enemy.Drained, Is.EqualTo(0));
        }

        [Test]
        public void PointBlank_EnemyExposed_DealsDamageAndAppliesDrained()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            enemy.ApplyExposed(1);
            var card = Core.Blessings.Artemis.ArtemisCards.PointBlank();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(32));
            Assert.That(enemy.Drained, Is.EqualTo(1));
        }

        [Test]
        public void Flurry_IsAttackTaggedShot()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.Flurry();

            Assert.That(card.Type, Is.EqualTo(CardType.Attack));
            Assert.That(card.Tags.Contains(CardTag.Shot), Is.True);
        }

        [Test]
        public void Flurry_NoShotsPlayedYet_DealsBaseDamage()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.Artemis.ArtemisCards.Flurry();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(38));
        }

        [Test]
        public void Flurry_AfterEarlierShotPlayedThisTurn_DealsBonusDamage()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = Core.Blessings.Artemis.ArtemisCards.QuickShot();
            var flurry = Core.Blessings.Artemis.ArtemisCards.Flurry();
            player.AddToDrawPile(new[] { quickShot, flurry });
            player.StartTurn(cardsToDraw: 2);
            CombatResolver.PlayCard(player, quickShot, enemy);

            CombatResolver.PlayCard(player, flurry, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void Quickdraw_IsSkillWithNoTags()
        {
            var card = Core.Blessings.Artemis.ArtemisCards.Quickdraw();

            Assert.That(card.Type, Is.EqualTo(CardType.Skill));
            Assert.That(card.Tags.Count, Is.EqualTo(0));
        }

        [Test]
        public void Quickdraw_DrawsOneCardAndReducesShotCostThisTurn()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var quickdraw = Core.Blessings.Artemis.ArtemisCards.Quickdraw();
            var filler = Core.Blessings.Artemis.ArtemisCards.SideStep();
            player.AddToDrawPile(new[] { quickdraw });
            player.StartTurn(cardsToDraw: 1);
            player.AddToDrawPile(new[] { filler });

            CombatResolver.PlayCard(player, quickdraw, enemy);

            Assert.That(player.Hand.Contains(filler), Is.True);
            Assert.That(player.ShotCostReductionThisTurn, Is.EqualTo(1));
        }

        [Test]
        public void Quickdraw_ThenShotCard_ReducesThatCardsCost()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var quickdraw = Core.Blessings.Artemis.ArtemisCards.Quickdraw();
            var quickShot = Core.Blessings.Artemis.ArtemisCards.QuickShot();
            player.AddToDrawPile(new[] { quickdraw, quickShot });
            player.StartTurn(cardsToDraw: 2);
            CombatResolver.PlayCard(player, quickdraw, enemy);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(player.CurrentEnergy, Is.EqualTo(1));
        }

        [Test]
        public void StarterDeck_ReturnsTenCards()
        {
            var deck = Core.Blessings.Artemis.ArtemisCards.StarterDeck().ToList();

            Assert.That(deck.Count, Is.EqualTo(10));
        }

        [Test]
        public void StarterDeck_HasCorrectComposition()
        {
            var deck = Core.Blessings.Artemis.ArtemisCards.StarterDeck().ToList();

            Assert.That(deck.Count(c => c.Name == "Quick Shot"), Is.EqualTo(5));
            Assert.That(deck.Count(c => c.Name == "Side Step"), Is.EqualTo(4));
            Assert.That(deck.Count(c => c.Name == "Hunter's Mark"), Is.EqualTo(1));
        }

        [Test]
        public void StarterDeck_ReturnsDistinctCardInstances()
        {
            var deck = Core.Blessings.Artemis.ArtemisCards.StarterDeck().ToList();

            var distinctCount = deck.Distinct().Count();

            Assert.That(distinctCount, Is.EqualTo(10));
        }
    }
}
