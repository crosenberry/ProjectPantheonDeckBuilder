namespace Pantheon.Core.Combat
{
    public class DrawCardIfScaleBalancedEffect : CardEffect
    {
        public int BaseAmount { get; }
        public int BonusAmount { get; }

        public DrawCardIfScaleBalancedEffect(int baseAmount, int bonusAmount)
        {
            BaseAmount = baseAmount;
            BonusAmount = bonusAmount;
        }

        public override void Apply(CardEffectContext context)
        {
            var amount = context.Player.Scale >= -1 && context.Player.Scale <= 1 ? BaseAmount + BonusAmount : BaseAmount;
            context.Player.DrawCards(amount);
        }
    }
}
