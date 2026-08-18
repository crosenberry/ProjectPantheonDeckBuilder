namespace Pantheon.Core.Combat
{
    public class ConditionalDamageIfStormPresentEffect : CardEffect
    {
        public int BaseAmount { get; }
        public int BonusAmount { get; }

        public ConditionalDamageIfStormPresentEffect(int baseAmount, int bonusAmount)
        {
            BaseAmount = baseAmount;
            BonusAmount = bonusAmount;
        }

        public override void Apply(CardEffectContext context)
        {
            var totalBase = context.Player.Storm > 0 ? BaseAmount + BonusAmount : BaseAmount;
            var damage = CombatMath.ApplyDamageModifiers(totalBase, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
            context.Enemy.TakeDamage(damage);
        }
    }
}
