namespace Pantheon.Core.Combat
{
    public class ConsumeUpToStormBonusDamageEffect : CardEffect
    {
        public int BaseAmount { get; }
        public int MaxConsume { get; }
        public int BonusPerPoint { get; }

        public ConsumeUpToStormBonusDamageEffect(int baseAmount, int maxConsume, int bonusPerPoint)
        {
            BaseAmount = baseAmount;
            MaxConsume = maxConsume;
            BonusPerPoint = bonusPerPoint;
        }

        public override void Apply(CardEffectContext context)
        {
            var consumed = context.Player.ConsumeStorm(MaxConsume);
            var totalBase = BaseAmount + (BonusPerPoint * consumed);
            var damage = CombatMath.ApplyDamageModifiers(totalBase, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
            context.Enemy.TakeDamage(damage);
        }
    }
}
