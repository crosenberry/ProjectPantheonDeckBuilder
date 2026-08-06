namespace Pantheon.Core.Combat
{
    public class ConsumeVolleyDamageEffect : CardEffect
    {
        public int DamagePerPoint { get; }

        public ConsumeVolleyDamageEffect(int damagePerPoint)
        {
            DamagePerPoint = damagePerPoint;
        }

        public override void Apply(CardEffectContext context)
        {
            var consumed = context.Player.ConsumeVolley();
            var hits = System.Math.Max(1, consumed);
            var baseDamage = DamagePerPoint * hits;
            var damage = CombatMath.ApplyDamageModifiers(baseDamage, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
            context.Enemy.TakeDamage(damage);
        }
    }
}
