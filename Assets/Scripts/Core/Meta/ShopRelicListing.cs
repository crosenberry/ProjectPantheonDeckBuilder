namespace Pantheon.Core.Meta
{
    public class ShopRelicListing
    {
        public Relic Relic { get; }
        public int Cost { get; }

        public ShopRelicListing(Relic relic, int cost)
        {
            Relic = relic;
            Cost = cost;
        }
    }
}
