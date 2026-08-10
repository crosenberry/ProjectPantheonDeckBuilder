using System.Collections.Generic;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Meta
{
    public class Deck
    {
        private readonly List<Card> _cards = new List<Card>();

        public IReadOnlyList<Card> Cards => _cards;

        public void Add(Card card)
        {
            _cards.Add(card);
        }

        public void Remove(Card card)
        {
            if (!_cards.Remove(card))
            {
                throw new System.InvalidOperationException($"Card '{card.Name}' is not in the deck.");
            }
        }
    }
}
