namespace Pantheon.Core.Combat
{
    public class GainStormEffect : CardEffect
    {
        public int Amount { get; }

        public GainStormEffect(int amount)
        {
            Amount = amount;
        }

        public override void Apply(CardEffectContext context)
        {
            context.Player.GainStorm(Amount);
        }
    }
}
