namespace Pantheon.Core.Combat
{
    public class VolleyScaledDamageToAllEnemiesEffect : CardEffect
    {
        public int BaseAmount { get; }
        public int BonusPerVolley { get; }

        public VolleyScaledDamageToAllEnemiesEffect(int baseAmount, int bonusPerVolley)
        {
            BaseAmount = baseAmount;
            BonusPerVolley = bonusPerVolley;
        }

        public override void Apply(CardEffectContext context)
        {
            var totalBase = BaseAmount + (BonusPerVolley * context.Player.Volley);

            foreach (var enemy in context.Enemies)
            {
                var damage = CombatMath.ApplyDamageModifiers(totalBase, context.Player.Strength, context.Player.Drained, enemy.Exposed);
                enemy.TakeDamage(damage);
            }
        }
    }
}
