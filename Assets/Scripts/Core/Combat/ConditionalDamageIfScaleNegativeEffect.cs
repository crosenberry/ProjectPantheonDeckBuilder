namespace Pantheon.Core.Combat
{
    public class ConditionalDamageIfScaleNegativeEffect : CardEffect
    {
        public int BaseAmount { get; }
        public int BonusAmount { get; }

        public ConditionalDamageIfScaleNegativeEffect(int baseAmount, int bonusAmount)
        {
            BaseAmount = baseAmount;
            BonusAmount = bonusAmount;
        }

        public override void Apply(CardEffectContext context)
        {
            var totalBase = context.Player.Scale < 0 ? BaseAmount + BonusAmount : BaseAmount;
            var damage = CombatMath.ApplyDamageModifiers(totalBase, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
            context.Enemy.TakeDamage(damage);
        }
    }
}
