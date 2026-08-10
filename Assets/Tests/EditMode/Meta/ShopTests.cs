using System.Collections.Generic;
using NUnit.Framework;
using Pantheon.Core.Combat;
using Pantheon.Core.Meta;

namespace Pantheon.Core.Tests.Meta
{
    public class ShopTests
    {
        [Test]
        public void BuyCard_Affordable_AddsCardSpendsEssenceAndRemovesListing()
        {
            var card = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            var shop = new Shop(new[] { new ShopCardListing(card, cost: 15) }, new ShopRelicListing[0], cardRemovalCost: 20);
            var wallet = new EssenceWallet();
            wallet.Gain(20);
            var deck = new Deck();

            shop.BuyCard(wallet, deck, card);

            Assert.That(deck.Cards, Contains.Item(card));
            Assert.That(wallet.Balance, Is.EqualTo(5));
            Assert.That(shop.CardListings, Is.Empty);
        }

        [Test]
        public void BuyCard_NotListed_Throws()
        {
            var listedCard = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            var unlistedCard = Card.Attack("Precise Shot", energyCost: 1, damage: 9);
            var shop = new Shop(new[] { new ShopCardListing(listedCard, cost: 15) }, new ShopRelicListing[0], cardRemovalCost: 20);
            var wallet = new EssenceWallet();
            wallet.Gain(20);
            var deck = new Deck();

            Assert.Throws<System.InvalidOperationException>(() => shop.BuyCard(wallet, deck, unlistedCard));
        }

        [Test]
        public void BuyCard_InsufficientEssence_ThrowsAndDeckUnchanged()
        {
            var card = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            var shop = new Shop(new[] { new ShopCardListing(card, cost: 15) }, new ShopRelicListing[0], cardRemovalCost: 20);
            var wallet = new EssenceWallet();
            wallet.Gain(5);
            var deck = new Deck();

            Assert.Throws<System.InvalidOperationException>(() => shop.BuyCard(wallet, deck, card));
            Assert.That(deck.Cards, Has.No.Member(card));
        }

        [Test]
        public void BuyRelic_Affordable_AddsRelicSpendsEssenceAndRemovesListing()
        {
            var relic = new Relic("Ares' First Blood", "Deals bonus damage.");
            var shop = new Shop(new ShopCardListing[0], new[] { new ShopRelicListing(relic, cost: 25) }, cardRemovalCost: 20);
            var wallet = new EssenceWallet();
            wallet.Gain(30);
            var ownedRelics = new List<Relic>();

            shop.BuyRelic(wallet, ownedRelics, relic);

            Assert.That(ownedRelics, Contains.Item(relic));
            Assert.That(wallet.Balance, Is.EqualTo(5));
            Assert.That(shop.RelicListings, Is.Empty);
        }

        [Test]
        public void BuyRelic_NotListed_Throws()
        {
            var listedRelic = new Relic("Ares' First Blood", "Deals bonus damage.");
            var unlistedRelic = new Relic("Hermes' Winged Sandals", "First card costs less.");
            var shop = new Shop(new ShopCardListing[0], new[] { new ShopRelicListing(listedRelic, cost: 25) }, cardRemovalCost: 20);
            var wallet = new EssenceWallet();
            wallet.Gain(30);
            var ownedRelics = new List<Relic>();

            Assert.Throws<System.InvalidOperationException>(() => shop.BuyRelic(wallet, ownedRelics, unlistedRelic));
        }

        [Test]
        public void BuyRelic_InsufficientEssence_ThrowsAndOwnedRelicsUnchanged()
        {
            var relic = new Relic("Ares' First Blood", "Deals bonus damage.");
            var shop = new Shop(new ShopCardListing[0], new[] { new ShopRelicListing(relic, cost: 25) }, cardRemovalCost: 20);
            var wallet = new EssenceWallet();
            wallet.Gain(10);
            var ownedRelics = new List<Relic>();

            Assert.Throws<System.InvalidOperationException>(() => shop.BuyRelic(wallet, ownedRelics, relic));
            Assert.That(ownedRelics, Has.No.Member(relic));
        }

        [Test]
        public void RemoveCard_Affordable_RemovesCardAndSpendsEssence()
        {
            var card = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            var shop = new Shop(new ShopCardListing[0], new ShopRelicListing[0], cardRemovalCost: 20);
            var wallet = new EssenceWallet();
            wallet.Gain(20);
            var deck = new Deck();
            deck.Add(card);

            shop.RemoveCard(wallet, deck, card);

            Assert.That(deck.Cards, Has.No.Member(card));
            Assert.That(wallet.Balance, Is.EqualTo(0));
        }

        [Test]
        public void RemoveCard_InsufficientEssence_ThrowsAndCardRemainsInDeck()
        {
            var card = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            var shop = new Shop(new ShopCardListing[0], new ShopRelicListing[0], cardRemovalCost: 20);
            var wallet = new EssenceWallet();
            wallet.Gain(5);
            var deck = new Deck();
            deck.Add(card);

            Assert.Throws<System.InvalidOperationException>(() => shop.RemoveCard(wallet, deck, card));
            Assert.That(deck.Cards, Contains.Item(card));
        }
    }
}
