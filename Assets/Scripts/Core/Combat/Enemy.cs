namespace Pantheon.Core.Combat
{
    public class Enemy : ICombatant
    {
        public int MaxHP { get; }
        public int CurrentHP { get; private set; }
        public int AttackDamage { get; }
        public int Strength { get; private set; }
        public int Exposed { get; private set; }
        public int Drained { get; private set; }

        public Enemy(int maxHP, int attackDamage = 0)
        {
            MaxHP = maxHP;
            CurrentHP = maxHP;
            AttackDamage = attackDamage;
        }

        public void TakeDamage(int amount)
        {
            CurrentHP = System.Math.Max(0, CurrentHP - amount);
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

        public void StartTurn()
        {
            Exposed = System.Math.Max(0, Exposed - 1);
            Drained = System.Math.Max(0, Drained - 1);
        }
    }
}
