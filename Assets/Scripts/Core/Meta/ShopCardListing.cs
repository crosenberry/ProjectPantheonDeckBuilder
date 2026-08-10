using Pantheon.Core.Combat;

namespace Pantheon.Core.Meta
{
    public class ShopCardListing
    {
        public Card Card { get; }
        public int Cost { get; }

        public ShopCardListing(Card card, int cost)
        {
            Card = card;
            Cost = cost;
        }
    }
}
