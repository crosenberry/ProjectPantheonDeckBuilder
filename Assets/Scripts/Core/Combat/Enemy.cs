using System.Collections.Generic;
using System.Linq;

namespace Pantheon.Core.Combat
{
    public class Enemy : ICombatant
    {
        private readonly IRandom _random;

        public int MaxHP { get; }
        public int CurrentHP { get; private set; }
        public int AttackDamage { get; }
        public int Strength { get; private set; }
        public int Exposed { get; private set; }
        public int Drained { get; private set; }
        public int CurrentBlock { get; private set; }
        public int Storm { get; private set; }
        public int Scale { get; private set; }
        public Form Form { get; private set; }
        public IReadOnlyList<EnemyMove> Moves { get; }
        public EnemyMove CurrentIntent { get; private set; }

        public Enemy(int maxHP, int attackDamage = 0)
        {
            MaxHP = maxHP;
            CurrentHP = maxHP;
            AttackDamage = attackDamage;
            Moves = System.Array.Empty<EnemyMove>();
        }

        public Enemy(int maxHP, IReadOnlyList<EnemyMove> moves, IRandom random)
        {
            MaxHP = maxHP;
            CurrentHP = maxHP;
            Moves = moves;
            _random = random;
            ChooseNextIntent();
        }

        public void TakeDamage(int amount)
        {
            var blocked = System.Math.Min(amount, CurrentBlock);
            CurrentBlock -= blocked;

            var remaining = amount - blocked;
            CurrentHP = System.Math.Max(0, CurrentHP - remaining);
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

        public void GainStorm(int amount)
        {
            Storm += amount;
        }

        public int ConsumeStorm()
        {
            var consumed = Storm;
            Storm = 0;
            return consumed;
        }

        public void AdjustScale(int amount)
        {
            Scale = System.Math.Max(-5, System.Math.Min(5, Scale + amount));
        }

        public void ChangeForm(Form targetForm)
        {
            Form = targetForm;
        }

        public void ChooseNextIntent()
        {
            var eligibleMoves = Moves.Where(IsEligible).ToList();

            if (eligibleMoves.Count == 0)
            {
                CurrentIntent = null;
                return;
            }

            var totalWeight = 0;
            foreach (var move in eligibleMoves)
            {
                totalWeight += move.Weight;
            }

            var roll = _random.Next(totalWeight);
            var cumulative = 0;

            foreach (var move in eligibleMoves)
            {
                cumulative += move.Weight;
                if (roll < cumulative)
                {
                    CurrentIntent = move;
                    return;
                }
            }
        }

        private bool IsEligible(EnemyMove move)
        {
            if (move.MinStorm.HasValue && Storm < move.MinStorm.Value) { return false; }
            if (move.MaxStorm.HasValue && Storm > move.MaxStorm.Value) { return false; }
            if (move.MinScale.HasValue && Scale < move.MinScale.Value) { return false; }
            if (move.MaxScale.HasValue && Scale > move.MaxScale.Value) { return false; }
            if (move.RequiredForm.HasValue && Form != move.RequiredForm.Value) { return false; }
            return true;
        }

        public void StartTurn()
        {
            CurrentBlock = 0;
            Exposed = System.Math.Max(0, Exposed - 1);
            Drained = System.Math.Max(0, Drained - 1);
        }
    }
}
