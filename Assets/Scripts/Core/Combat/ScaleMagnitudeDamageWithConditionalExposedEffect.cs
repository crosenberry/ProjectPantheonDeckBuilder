namespace Pantheon.Core.Combat
{
    public class ScaleMagnitudeDamageWithConditionalExposedEffect : CardEffect
    {
        public int DamagePerPoint { get; }

        public ScaleMagnitudeDamageWithConditionalExposedEffect(int damagePerPoint)
        {
            DamagePerPoint = damagePerPoint;
        }

        public override void Apply(CardEffectContext context)
        {
            var scale = context.Player.Scale;
            var magnitude = System.Math.Abs(scale);
            var totalBase = DamagePerPoint * magnitude;
            var damage = CombatMath.ApplyDamageModifiers(totalBase, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
            context.Enemy.TakeDamage(damage);

            if (scale < 0)
            {
                context.Enemy.ApplyExposed(magnitude);
            }
        }
    }
}
