namespace Pantheon.Core.Combat
{
    public class ApplyStatusIfScaleBalancedEffect : CardEffect
    {
        public StatusType Status { get; }
        public int Amount { get; }
        public EffectTarget Target { get; }

        public ApplyStatusIfScaleBalancedEffect(StatusType status, int amount, EffectTarget target)
        {
            Status = status;
            Amount = amount;
            Target = target;
        }

        public override void Apply(CardEffectContext context)
        {
            if (context.Player.Scale < -1 || context.Player.Scale > 1)
            {
                return;
            }

            new ApplyStatusEffect(Status, Amount, Target).Apply(context);
        }
    }
}
