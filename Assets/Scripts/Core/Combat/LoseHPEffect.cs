namespace Pantheon.Core.Combat
{
    public class LoseHPEffect : CardEffect
    {
        public int Amount { get; }

        public LoseHPEffect(int amount)
        {
            Amount = amount;
        }

        public override void Apply(CardEffectContext context)
        {
            context.Player.LoseHP(Amount);
        }
    }
}
