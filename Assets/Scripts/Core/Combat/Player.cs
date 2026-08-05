using System.Collections.Generic;

namespace Pantheon.Core.Combat
{
    public class Player : ICombatant
    {
        private readonly List<Card> _drawPile = new List<Card>();
        private readonly List<Card> _discardPile = new List<Card>();
        private readonly List<Card> _hand = new List<Card>();
        private readonly IRandom _random;

        public int CurrentEnergy { get; private set; }
        public int MaxEnergy { get; }
        public int MaxHP { get; }
        public int CurrentHP { get; private set; }
        public int CurrentBlock { get; private set; }
        public int Strength { get; private set; }
        public int Exposed { get; private set; }
        public int Drained { get; private set; }
        public int Sundered { get; private set; }
        public int Volley { get; private set; }
        public IReadOnlyList<Card> DrawPile => _drawPile;
        public IReadOnlyList<Card> DiscardPile => _discardPile;
        public IReadOnlyList<Card> Hand => _hand;

        public Player(int startingEnergy) : this(startingEnergy, new SystemRandom())
        {
        }

        public Player(int startingEnergy, IRandom random) : this(startingEnergy, startingHP: 999, random)
        {
        }

        public Player(int startingEnergy, int startingHP, IRandom random)
        {
            CurrentEnergy = startingEnergy;
            MaxEnergy = startingEnergy;
            MaxHP = startingHP;
            CurrentHP = startingHP;
            _random = random;
        }

        public void SpendEnergy(int amount)
        {
            if (amount > CurrentEnergy)
            {
                throw new System.InvalidOperationException(
                    $"Cannot spend {amount} energy with only {CurrentEnergy} available.");
            }

            CurrentEnergy -= amount;
        }

        public void AddToDrawPile(IEnumerable<Card> cards)
        {
            _drawPile.AddRange(cards);
        }

        public void StartTurn(int cardsToDraw)
        {
            CurrentEnergy = MaxEnergy;
            CurrentBlock = 0;
            Volley = 0;
            Exposed = System.Math.Max(0, Exposed - 1);
            Drained = System.Math.Max(0, Drained - 1);
            Sundered = System.Math.Max(0, Sundered - 1);

            for (var i = 0; i < cardsToDraw; i++)
            {
                if (_drawPile.Count == 0)
                {
                    if (_discardPile.Count == 0)
                    {
                        break;
                    }

                    ReshuffleDiscardIntoDrawPile();
                }

                _hand.Add(_drawPile[0]);
                _drawPile.RemoveAt(0);
            }
        }

        private void ReshuffleDiscardIntoDrawPile()
        {
            _drawPile.AddRange(_discardPile);
            _discardPile.Clear();

            for (var i = _drawPile.Count - 1; i > 0; i--)
            {
                var j = _random.Next(i + 1);
                (_drawPile[i], _drawPile[j]) = (_drawPile[j], _drawPile[i]);
            }
        }

        public void EndTurn()
        {
            _discardPile.AddRange(_hand);
            _hand.Clear();
        }

        public void DiscardFromHand(Card card)
        {
            _hand.Remove(card);
            _discardPile.Add(card);
        }

        public void GainBlock(int amount)
        {
            CurrentBlock += amount;
        }

        public void GainStrength(int amount)
        {
            Strength += amount;
        }

        public void ApplyExposed(int amount)
        {
            Exposed += amount;
        }

        public void ApplyDrained(int amount)
        {
            Drained += amount;
        }

        public void ApplySundered(int amount)
        {
            Sundered += amount;
        }

        public void GainVolley(int amount)
        {
            Volley += amount;
        }

        public int ConsumeVolley()
        {
            var consumed = Volley;
            Volley = 0;
            return consumed;
        }

        public void TakeDamage(int amount)
        {
            var blocked = System.Math.Min(amount, CurrentBlock);
            CurrentBlock -= blocked;

            var remaining = amount - blocked;
            CurrentHP = System.Math.Max(0, CurrentHP - remaining);
        }
    }
}
