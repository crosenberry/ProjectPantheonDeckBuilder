namespace Pantheon.Core.Combat
{
    public class ConditionalGainBlockByStormEffect : CardEffect
    {
        public int BaseAmount { get; }
        public int ThresholdAmount { get; }
        public int StormThreshold { get; }

        public ConditionalGainBlockByStormEffect(int baseAmount, int thresholdAmount, int stormThreshold)
        {
            BaseAmount = baseAmount;
            ThresholdAmount = thresholdAmount;
            StormThreshold = stormThreshold;
        }

        public override void Apply(CardEffectContext context)
        {
            var block = context.Player.Storm >= StormThreshold ? ThresholdAmount : BaseAmount;

            if (block > 0 && context.Player.Sundered > 0)
            {
                block = (int)(block * 0.75);
            }

            context.Player.GainBlock(block);
        }
    }
}
