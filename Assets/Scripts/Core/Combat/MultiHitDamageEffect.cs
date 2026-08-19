namespace Pantheon.Core.Combat
{
    public class MultiHitDamageEffect : CardEffect
    {
        public int Amount { get; }
        public int HitCount { get; }

        public MultiHitDamageEffect(int amount, int hitCount)
        {
            Amount = amount;
            HitCount = hitCount;
        }

        // Fixed hit count, printed on the card - deliberately does NOT double in
        // Beast Form (GDD §3.4 / SunWukong-FullCardDraft.md ruling: Beast Form
        // only adds a hit to Attacks that normally hit once, so a 3-hit card
        // always stays a 3-hit card). Immortal's -3-per-hit still applies, same
        // as DealDamageEffect.
        public override void Apply(CardEffectContext context)
        {
            for (var i = 0; i < HitCount; i++)
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
