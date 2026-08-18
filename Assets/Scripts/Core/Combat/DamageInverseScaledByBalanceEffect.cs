namespace Pantheon.Core.Combat
{
    public class DamageInverseScaledByBalanceEffect : CardEffect
    {
        public int Multiplier { get; }
        public int MaxMagnitude { get; }

        public DamageInverseScaledByBalanceEffect(int multiplier, int maxMagnitude)
        {
            Multiplier = multiplier;
            MaxMagnitude = maxMagnitude;
        }

        public override void Apply(CardEffectContext context)
        {
            var magnitude = System.Math.Abs(context.Player.Scale);
            var totalBase = Multiplier * (MaxMagnitude - magnitude);
            var damage = CombatMath.ApplyDamageModifiers(totalBase, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
            context.Enemy.TakeDamage(damage);
        }
    }
}
