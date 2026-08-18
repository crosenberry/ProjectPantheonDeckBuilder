using System.Collections.Generic;

namespace Pantheon.Core.Combat
{
    public class Player : ICombatant
    {
        private readonly List<Card> _drawPile = new List<Card>();
        private readonly List<Card> _discardPile = new List<Card>();
        private readonly List<Card> _hand = new List<Card>();
        private readonly List<Card> _exhaustPile = new List<Card>();
        private readonly List<TriggeredEffect> _triggers = new List<TriggeredEffect>();
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
        public int Storm { get; private set; }
        public int Scale { get; private set; }
        public int ShotsPlayedThisTurn { get; private set; }
        public int ShotCostReductionThisTurn { get; private set; }
        public IReadOnlyList<Card> DrawPile => _drawPile;
        public IReadOnlyList<Card> DiscardPile => _discardPile;
        public IReadOnlyList<Card> Hand => _hand;
        public IReadOnlyList<Card> ExhaustPile => _exhaustPile;
        public IReadOnlyList<TriggeredEffect> Triggers => _triggers;

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
            ShotsPlayedThisTurn = 0;
            ShotCostReductionThisTurn = 0;
            Exposed = System.Math.Max(0, Exposed - 1);
            Drained = System.Math.Max(0, Drained - 1);
            Sundered = System.Math.Max(0, Sundered - 1);

            DrawCards(cardsToDraw);
        }

        public void DrawCards(int count)
        {
            for (var i = 0; i < count; i++)
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

        public void ExhaustFromHand(Card card)
        {
            _hand.Remove(card);
            _exhaustPile.Add(card);
        }

        public void AddTrigger(TriggeredEffect trigger)
        {
            _triggers.Add(trigger);
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

        public void GainStorm(int amount)
        {
            Storm += amount;
        }

        public int ConsumeStorm()
        {
            return ConsumeStorm(Storm);
        }

        public int ConsumeStorm(int maxAmount)
        {
            var consumed = System.Math.Min(Storm, maxAmount);
            Storm -= consumed;
            return consumed;
        }

        // Scale is bounded to [-5, 5] (GDD §3.3) - the first bounded resource;
        // Volley/Storm are unbounded upward. Both AdjustScale (relative push,
        // e.g. "Scale -1") and SetScale (absolute, e.g. "Set Scale to 0") clamp.
        public void AdjustScale(int amount)
        {
            Scale = System.Math.Max(-5, System.Math.Min(5, Scale + amount));
        }

        public void SetScale(int value)
        {
            Scale = System.Math.Max(-5, System.Math.Min(5, value));
        }

        public void RecordShotPlayed()
        {
            ShotsPlayedThisTurn += 1;
        }

        public void ReduceShotCostThisTurn(int amount)
        {
            ShotCostReductionThisTurn += amount;
        }

        public void TakeDamage(int amount)
        {
            var blocked = System.Math.Min(amount, CurrentBlock);
            CurrentBlock -= blocked;

            var remaining = amount - blocked;
            CurrentHP = System.Math.Max(0, CurrentHP - remaining);
        }

        public void Heal(int amount)
        {
            CurrentHP = System.Math.Min(MaxHP, CurrentHP + amount);
        }

        // Direct HP loss, bypassing Block entirely - for self-inflicted costs
        // (e.g. Bloodrage's "Lose 3 HP"), distinct from TakeDamage which is for
        // incoming enemy damage and always applies Block first.
        public void LoseHP(int amount)
        {
            CurrentHP = System.Math.Max(0, CurrentHP - amount);
        }

        // Resets all combat-scoped state for a fresh encounter; HP persists across
        // the run. Clears every trigger, which is correct for card-granted triggers
        // (Power cards re-register by being played again) - once Enhancements enter
        // the run flow, run-persistent triggers will need to be re-granted after
        // this call or split into their own list.
        public void PrepareForCombat(IEnumerable<Card> drawPile)
        {
            _hand.Clear();
            _drawPile.Clear();
            _discardPile.Clear();
            _exhaustPile.Clear();
            _triggers.Clear();
            _drawPile.AddRange(drawPile);

            CurrentBlock = 0;
            Strength = 0;
            Exposed = 0;
            Drained = 0;
            Sundered = 0;
            Volley = 0;
            Storm = 0;
            Scale = 0;
            ShotsPlayedThisTurn = 0;
            ShotCostReductionThisTurn = 0;
        }
    }
}
