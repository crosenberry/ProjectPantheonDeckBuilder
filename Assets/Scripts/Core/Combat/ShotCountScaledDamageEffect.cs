namespace Pantheon.Core.Combat
{
    public class ShotCountScaledDamageEffect : CardEffect
    {
        public int BaseAmount { get; }
        public int BonusPerShot { get; }

        public ShotCountScaledDamageEffect(int baseAmount, int bonusPerShot)
        {
            BaseAmount = baseAmount;
            BonusPerShot = bonusPerShot;
        }

        public override void Apply(CardEffectContext context)
        {
            var totalBase = BaseAmount + (BonusPerShot * context.Player.ShotsPlayedThisTurn);
            var damage = CombatMath.ApplyDamageModifiers(totalBase, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
            context.Enemy.TakeDamage(damage);
        }
    }
}
