namespace Pantheon.Core.Combat
{
    public class Enemy
    {
        public int MaxHP { get; }
        public int CurrentHP { get; private set; }

        public Enemy(int maxHP)
        {
            MaxHP = maxHP;
            CurrentHP = maxHP;
        }

        public void TakeDamage(int amount)
        {
            CurrentHP = System.Math.Max(0, CurrentHP - amount);
        }
    }
}
