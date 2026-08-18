namespace Pantheon.Core.Combat
{
    public class ConsumeStormOrFallbackDamageEffect : CardEffect
    {
        public int ConsumeAmount { get; }
        public int DamageIfConsumed { get; }
        public int DamageIfNotConsumed { get; }

        public ConsumeStormOrFallbackDamageEffect(int consumeAmount, int damageIfConsumed, int damageIfNotConsumed)
        {
            ConsumeAmount = consumeAmount;
            DamageIfConsumed = damageIfConsumed;
            DamageIfNotConsumed = damageIfNotConsumed;
        }

        public override void Apply(CardEffectContext context)
        {
            var consumed = context.Player.ConsumeStorm(ConsumeAmount);
            var baseDamage = consumed > 0 ? DamageIfConsumed : DamageIfNotConsumed;
            var damage = CombatMath.ApplyDamageModifiers(baseDamage, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
            context.Enemy.TakeDamage(damage);
        }
    }
}
