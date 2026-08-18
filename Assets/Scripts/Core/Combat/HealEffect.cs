namespace Pantheon.Core.Combat
{
    public class HealEffect : CardEffect
    {
        public int Amount { get; }

        public HealEffect(int amount)
        {
            Amount = amount;
        }

        public override void Apply(CardEffectContext context)
        {
            context.Player.Heal(Amount);
        }
    }
}
