namespace Pantheon.Core.Combat
{
    public class AdjustScaleEffect : CardEffect
    {
        public int Amount { get; }

        public AdjustScaleEffect(int amount)
        {
            Amount = amount;
        }

        public override void Apply(CardEffectContext context)
        {
            context.Player.AdjustScale(Amount);
        }
    }
}
