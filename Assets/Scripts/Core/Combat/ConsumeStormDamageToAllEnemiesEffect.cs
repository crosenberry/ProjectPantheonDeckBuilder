namespace Pantheon.Core.Combat
{
    public class ConsumeStormDamageToAllEnemiesEffect : CardEffect
    {
        public int DamagePerPoint { get; }

        public ConsumeStormDamageToAllEnemiesEffect(int damagePerPoint)
        {
            DamagePerPoint = damagePerPoint;
        }

        public override void Apply(CardEffectContext context)
        {
            var consumed = context.Player.ConsumeStorm();
            var totalBase = DamagePerPoint * consumed;

            foreach (var enemy in context.Enemies)
            {
                var damage = CombatMath.ApplyDamageModifiers(totalBase, context.Player.Strength, context.Player.Drained, enemy.Exposed);
                enemy.TakeDamage(damage);
            }
        }
    }
}
