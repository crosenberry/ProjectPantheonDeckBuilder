namespace Pantheon.Core.Combat
{
    public class ConsumeScaleIfPositiveDamageToAllEnemiesEffect : CardEffect
    {
        public int DamagePerPoint { get; }

        public ConsumeScaleIfPositiveDamageToAllEnemiesEffect(int damagePerPoint)
        {
            DamagePerPoint = damagePerPoint;
        }

        public override void Apply(CardEffectContext context)
        {
            if (context.Player.Scale <= 0)
            {
                return;
            }

            var totalBase = DamagePerPoint * context.Player.Scale;
            context.Player.SetScale(0);

            foreach (var enemy in context.Enemies)
            {
                var damage = CombatMath.ApplyDamageModifiers(totalBase, context.Player.Strength, context.Player.Drained, enemy.Exposed);
                enemy.TakeDamage(damage);
            }
        }
    }
}
