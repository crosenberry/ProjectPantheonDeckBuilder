namespace Pantheon.Core.Combat
{
    public class GainBlockIfScaleBalancedEffect : CardEffect
    {
        public int Amount { get; }

        public GainBlockIfScaleBalancedEffect(int amount)
        {
            Amount = amount;
        }

        public override void Apply(CardEffectContext context)
        {
            if (context.Player.Scale < -1 || context.Player.Scale > 1)
            {
                return;
            }

            new GainBlockEffect(Amount).Apply(context);
        }
    }
}
