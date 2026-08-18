namespace Pantheon.Core.Combat
{
    public class ConditionalDamageIfScaleThresholdEffect : CardEffect
    {
        public int BaseAmount { get; }
        public int BonusAmount { get; }
        public int Threshold { get; }

        public ConditionalDamageIfScaleThresholdEffect(int baseAmount, int bonusAmount, int threshold)
        {
            BaseAmount = baseAmount;
            BonusAmount = bonusAmount;
            Threshold = threshold;
        }

        public override void Apply(CardEffectContext context)
        {
            var totalBase = context.Player.Scale <= Threshold ? BaseAmount + BonusAmount : BaseAmount;
            var damage = CombatMath.ApplyDamageModifiers(totalBase, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
            context.Enemy.TakeDamage(damage);
        }
    }
}
