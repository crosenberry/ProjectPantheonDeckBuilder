namespace Pantheon.Core.Combat
{
    public class ConsumeAllStormOrBaseDamageEffect : CardEffect
    {
        public int BaseAmount { get; }
        public int DamagePerPointIfConsumed { get; }

        public ConsumeAllStormOrBaseDamageEffect(int baseAmount, int damagePerPointIfConsumed)
        {
            BaseAmount = baseAmount;
            DamagePerPointIfConsumed = damagePerPointIfConsumed;
        }

        public override void Apply(CardEffectContext context)
        {
            var consumed = context.Player.ConsumeStorm();
            var totalBase = consumed > 0 ? DamagePerPointIfConsumed * consumed : BaseAmount;
            var damage = CombatMath.ApplyDamageModifiers(totalBase, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
            context.Enemy.TakeDamage(damage);
        }
    }
}
