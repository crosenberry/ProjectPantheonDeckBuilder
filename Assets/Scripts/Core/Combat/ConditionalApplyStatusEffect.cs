namespace Pantheon.Core.Combat
{
    public class ConditionalApplyStatusEffect : CardEffect
    {
        public StatusType Condition { get; }
        public StatusType StatusToApply { get; }
        public int Amount { get; }
        public EffectTarget Target { get; }

        public ConditionalApplyStatusEffect(StatusType condition, StatusType statusToApply, int amount, EffectTarget target)
        {
            Condition = condition;
            StatusToApply = statusToApply;
            Amount = amount;
            Target = target;
        }

        public override void Apply(CardEffectContext context)
        {
            var combatant = context.Resolve(Target);

            if (ReadStatus(combatant) <= 0)
            {
                return;
            }

            new ApplyStatusEffect(StatusToApply, Amount, Target).Apply(context);
        }

        private int ReadStatus(ICombatant combatant)
        {
            switch (Condition)
            {
                case StatusType.Strength:
                    return combatant.Strength;
                case StatusType.Exposed:
                    return combatant.Exposed;
                case StatusType.Drained:
                    return combatant.Drained;
                default:
                    return 0;
            }
        }
    }
}
