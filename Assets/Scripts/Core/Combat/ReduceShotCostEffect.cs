namespace Pantheon.Core.Combat
{
    public class ReduceShotCostEffect : CardEffect
    {
        public int Amount { get; }

        public ReduceShotCostEffect(int amount)
        {
            Amount = amount;
        }

        public override void Apply(CardEffectContext context)
        {
            context.Player.ReduceShotCostThisTurn(Amount);
        }
    }
}
