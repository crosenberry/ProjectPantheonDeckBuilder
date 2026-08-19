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
            // Beast Form doubles Attacks that normally hit once (GDD §3.4) - each hit
            // runs the full modifier pipeline independently, same precedent as
            // ConditionalDoubleHitDamageEffect, rather than doubling one final total.
            var hits = context.Player.Form == Form.Beast ? 2 : 1;

            for (var i = 0; i < hits; i++)
            {
                var damage = CombatMath.ApplyDamageModifiers(Amount, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);

                if (context.Player.Form == Form.Immortal)
                {
                    damage = System.Math.Max(1, damage - 3);
                }

                context.Enemy.TakeDamage(damage);
            }
        }
    }
}
