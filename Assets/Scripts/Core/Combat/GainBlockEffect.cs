namespace Pantheon.Core.Combat
{
    public class GainBlockEffect : CardEffect
    {
        public int Amount { get; }

        public GainBlockEffect(int amount)
        {
            Amount = amount;
        }

        public override void Apply(CardEffectContext context)
        {
            var block = Amount;

            if (block > 0 && context.Player.Sundered > 0)
            {
                block = (int)(block * 0.75);
            }

            context.Player.GainBlock(block);
        }
    }
}
