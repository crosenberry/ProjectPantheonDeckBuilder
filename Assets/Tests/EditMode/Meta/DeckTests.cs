using NUnit.Framework;
using Pantheon.Core.Combat;
using Pantheon.Core.Meta;

namespace Pantheon.Core.Tests.Meta
{
    public class DeckTests
    {
        [Test]
        public void Add_AddsCardToDeck()
        {
            var deck = new Deck();
            var card = Card.Attack("Quick Shot", energyCost: 1, damage: 6);

            deck.Add(card);

            Assert.That(deck.Cards, Contains.Item(card));
        }

        [Test]
        public void Remove_CardInDeck_RemovesIt()
        {
            var deck = new Deck();
            var card = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            deck.Add(card);

            deck.Remove(card);

            Assert.That(deck.Cards, Has.No.Member(card));
        }

        [Test]
        public void Remove_CardNotInDeck_Throws()
        {
            var deck = new Deck();
            var card = Card.Attack("Quick Shot", energyCost: 1, damage: 6);

            Assert.Throws<System.InvalidOperationException>(() => deck.Remove(card));
        }
    }
}
