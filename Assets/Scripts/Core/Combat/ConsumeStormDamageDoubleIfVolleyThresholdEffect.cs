namespace Pantheon.Core.Combat
{
    public class ConsumeStormDamageDoubleIfVolleyThresholdEffect : CardEffect
    {
        public int DamagePerPoint { get; }
        public int VolleyThreshold { get; }

        public ConsumeStormDamageDoubleIfVolleyThresholdEffect(int damagePerPoint, int volleyThreshold)
        {
            DamagePerPoint = damagePerPoint;
            VolleyThreshold = volleyThreshold;
        }

        public override void Apply(CardEffectContext context)
        {
            var consumed = context.Player.ConsumeStorm();
            var totalBase = DamagePerPoint * consumed;
            var hits = context.Player.Volley >= VolleyThreshold ? 2 : 1;

            for (var i = 0; i < hits; i++)
            {
                var damage = CombatMath.ApplyDamageModifiers(totalBase, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
                context.Enemy.TakeDamage(damage);
            }
        }
    }
}
