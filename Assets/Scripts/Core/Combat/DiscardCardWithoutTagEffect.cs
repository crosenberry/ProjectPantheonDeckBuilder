using System.Linq;

namespace Pantheon.Core.Combat
{
    public class DiscardCardWithoutTagEffect : CardEffect
    {
        public CardTag ExcludedTag { get; }

        public DiscardCardWithoutTagEffect(CardTag excludedTag)
        {
            ExcludedTag = excludedTag;
        }

        public override void Apply(CardEffectContext context)
        {
            var candidate = context.Player.Hand.FirstOrDefault(card => !card.Tags.Contains(ExcludedTag));

            if (candidate != null)
            {
                context.Player.DiscardFromHand(candidate);
            }
        }
    }
}
