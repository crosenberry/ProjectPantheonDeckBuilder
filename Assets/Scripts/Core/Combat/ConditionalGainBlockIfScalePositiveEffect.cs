namespace Pantheon.Core.Combat
{
    public class ConditionalGainBlockIfScalePositiveEffect : CardEffect
    {
        public int BaseAmount { get; }
        public int BonusAmount { get; }

        public ConditionalGainBlockIfScalePositiveEffect(int baseAmount, int bonusAmount)
        {
            BaseAmount = baseAmount;
            BonusAmount = bonusAmount;
        }

        public override void Apply(CardEffectContext context)
        {
            var totalBase = context.Player.Scale > 0 ? BaseAmount + BonusAmount : BaseAmount;
            new GainBlockEffect(totalBase).Apply(context);
        }
    }
}
