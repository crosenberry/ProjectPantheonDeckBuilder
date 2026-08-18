namespace Pantheon.Core.Combat
{
    public class ConsumeStormPlusVolleyBonusDamageToAllEnemiesEffect : CardEffect
    {
        public int DamagePerStorm { get; }
        public int BonusPerVolley { get; }

        public ConsumeStormPlusVolleyBonusDamageToAllEnemiesEffect(int damagePerStorm, int bonusPerVolley)
        {
            DamagePerStorm = damagePerStorm;
            BonusPerVolley = bonusPerVolley;
        }

        public override void Apply(CardEffectContext context)
        {
            var consumed = context.Player.ConsumeStorm();
            var totalBase = (DamagePerStorm * consumed) + (BonusPerVolley * context.Player.Volley);

            foreach (var enemy in context.Enemies)
            {
                var damage = CombatMath.ApplyDamageModifiers(totalBase, context.Player.Strength, context.Player.Drained, enemy.Exposed);
                enemy.TakeDamage(damage);
            }
        }
    }
}
