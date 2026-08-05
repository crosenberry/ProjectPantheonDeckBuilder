namespace Pantheon.Core.Combat
{
    public class DealDamageEffect : CardEffect
    {
        public int Amount { get; }

        public DealDamageEffect(int amount)
        {
            Amount = amount;
        }

        public override void Apply(CardEffectContext context)
        {
            var damage = CombatMath.ApplyDamageModifiers(Amount, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
            context.Enemy.TakeDamage(damage);
        }
    }
}
