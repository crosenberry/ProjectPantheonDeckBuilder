namespace Pantheon.Core.Combat
{
    public class ApplyStatusEffect : CardEffect
    {
        public StatusType Status { get; }
        public int Amount { get; }
        public EffectTarget Target { get; }

        public ApplyStatusEffect(StatusType status, int amount, EffectTarget target)
        {
            Status = status;
            Amount = amount;
            Target = target;
        }

        public override void Apply(CardEffectContext context)
        {
            if (Status == StatusType.Sundered)
            {
                context.Player.ApplySundered(Amount);
                return;
            }

            var combatant = context.Resolve(Target);

            switch (Status)
            {
                case StatusType.Strength:
                    combatant.GainStrength(Amount);
                    break;
                case StatusType.Exposed:
                    combatant.ApplyExposed(Amount);
                    break;
                case StatusType.Drained:
                    combatant.ApplyDrained(Amount);
                    break;
            }
        }
    }
}
