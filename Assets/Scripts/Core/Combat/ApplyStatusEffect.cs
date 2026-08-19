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
            var amount = Amount;

            if (Status == StatusType.Strength && ReferenceEquals(combatant, context.Player) && context.Player.Form == Form.Immortal)
            {
                amount += 1;
            }

            switch (Status)
            {
                case StatusType.Strength:
                    combatant.GainStrength(amount);
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
