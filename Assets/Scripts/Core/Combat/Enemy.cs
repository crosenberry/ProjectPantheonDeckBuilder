namespace Pantheon.Core.Combat
{
    public class Enemy
    {
        public int MaxHP { get; }
        public int CurrentHP { get; private set; }
        public int AttackDamage { get; }

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
    }
}
