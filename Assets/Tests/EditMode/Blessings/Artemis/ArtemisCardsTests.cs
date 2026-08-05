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
