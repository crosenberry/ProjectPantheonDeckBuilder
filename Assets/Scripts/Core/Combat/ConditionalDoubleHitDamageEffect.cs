namespace Pantheon.Core.Combat
{
    public class ConditionalDoubleHitDamageEffect : CardEffect
    {
        public int Amount { get; }
        public int VolleyThreshold { get; }

        public ConditionalDoubleHitDamageEffect(int amount, int volleyThreshold)
        {
            Amount = amount;
            VolleyThreshold = volleyThreshold;
        }

        public override void Apply(CardEffectContext context)
        {
            var hits = context.Player.Volley >= VolleyThreshold ? 2 : 1;

            for (var i = 0; i < hits; i++)
            {
                var damage = CombatMath.ApplyDamageModifiers(Amount, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
                context.Enemy.TakeDamage(damage);
            }
        }
    }
}
