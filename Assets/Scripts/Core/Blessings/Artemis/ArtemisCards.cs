using System.Collections.Generic;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Blessings.Artemis
{
    public static class ArtemisCards
    {
        public static Card QuickShot()
        {
            return Card.Attack("Quick Shot", energyCost: 1, damage: 6, tags: new[] { CardTag.Shot });
        }

        public static Card SideStep()
        {
            return Card.Skill("Side Step", energyCost: 1, block: 5);
        }

        public static Card HuntersMark()
        {
            return new Card("Hunter's Mark", energyCost: 2, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(8),
                new ApplyStatusEffect(StatusType.Exposed, 2, EffectTarget.Enemy)
            }, tags: new[] { CardTag.Shot });
        }

        public static IEnumerable<Card> StarterDeck()
        {
            for (var i = 0; i < 5; i++)
            {
                yield return QuickShot();
            }

            for (var i = 0; i < 4; i++)
            {
                yield return SideStep();
            }

            yield return HuntersMark();
        }
    }
}
