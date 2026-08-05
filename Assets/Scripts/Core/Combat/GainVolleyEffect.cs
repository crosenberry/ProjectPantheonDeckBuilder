namespace Pantheon.Core.Combat
{
    public class GainVolleyEffect : CardEffect
    {
        public int Amount { get; }

        public GainVolleyEffect(int amount)
        {
            Amount = amount;
        }

        public override void Apply(CardEffectContext context)
        {
            context.Player.GainVolley(Amount);
        }
    }
}
