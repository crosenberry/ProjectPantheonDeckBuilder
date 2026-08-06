namespace Pantheon.Core.Combat
{
    public class DrawCardEffect : CardEffect
    {
        public int Amount { get; }

        public DrawCardEffect(int amount)
        {
            Amount = amount;
        }

        public override void Apply(CardEffectContext context)
        {
            context.Player.DrawCards(Amount);
        }
    }
}
