namespace Pantheon.Core.Combat
{
    public class ExposeLowHpEnemiesEffect : CardEffect
    {
        public int HpThresholdPercent { get; }
        public int Amount { get; }

        public ExposeLowHpEnemiesEffect(int hpThresholdPercent, int amount)
        {
            HpThresholdPercent = hpThresholdPercent;
            Amount = amount;
        }

        public override void Apply(CardEffectContext context)
        {
            foreach (var enemy in context.Enemies)
            {
                if (enemy.CurrentHP * 100 < enemy.MaxHP * HpThresholdPercent)
                {
                    enemy.ApplyExposed(Amount);
                }
            }
        }
    }
}
