namespace Pantheon.Core.Combat
{
    public class ConditionalGainBlockIfChangedFormEffect : CardEffect
    {
        public int BaseAmount { get; }
        public int BonusAmount { get; }

        public ConditionalGainBlockIfChangedFormEffect(int baseAmount, int bonusAmount)
        {
            BaseAmount = baseAmount;
            BonusAmount = bonusAmount;
        }

        public override void Apply(CardEffectContext context)
        {
            var totalBase = context.Player.ChangedFormThisTurn ? BaseAmount + BonusAmount : BaseAmount;
            new GainBlockEffect(totalBase).Apply(context);
        }
    }
}
