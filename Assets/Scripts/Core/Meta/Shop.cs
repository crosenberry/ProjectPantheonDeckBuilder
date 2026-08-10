using System.Collections.Generic;
using System.Linq;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Meta
{
    public class Shop
    {
        private readonly List<ShopCardListing> _cardListings;
        private readonly List<ShopRelicListing> _relicListings;

        public IReadOnlyList<ShopCardListing> CardListings => _cardListings;
        public IReadOnlyList<ShopRelicListing> RelicListings => _relicListings;
        public int CardRemovalCost { get; }

        public Shop(IEnumerable<ShopCardListing> cardListings, IEnumerable<ShopRelicListing> relicListings, int cardRemovalCost)
        {
            _cardListings = new List<ShopCardListing>(cardListings);
            _relicListings = new List<ShopRelicListing>(relicListings);
            CardRemovalCost = cardRemovalCost;
        }

        public void BuyCard(EssenceWallet wallet, Deck deck, Card card)
        {
            var listing = _cardListings.FirstOrDefault(l => l.Card == card);
            if (listing == null)
            {
                throw new System.InvalidOperationException($"Card '{card.Name}' is not listed in this shop.");
            }

            wallet.Spend(listing.Cost);
            deck.Add(card);
            _cardListings.Remove(listing);
        }

        public void BuyRelic(EssenceWallet wallet, ICollection<Relic> ownedRelics, Relic relic)
        {
            var listing = _relicListings.FirstOrDefault(l => l.Relic == relic);
            if (listing == null)
            {
                throw new System.InvalidOperationException($"Relic '{relic.Name}' is not listed in this shop.");
            }

            wallet.Spend(listing.Cost);
            ownedRelics.Add(relic);
            _relicListings.Remove(listing);
        }

        public void RemoveCard(EssenceWallet wallet, Deck deck, Card card)
        {
            wallet.Spend(CardRemovalCost);
            deck.Remove(card);
        }
    }
}
