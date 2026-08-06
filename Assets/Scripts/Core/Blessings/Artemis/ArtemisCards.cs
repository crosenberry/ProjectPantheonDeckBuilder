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

        public static Card Nock()
        {
            return new Card("Nock", energyCost: 0, CardType.Skill, new CardEffect[]
            {
                new GainVolleyEffect(1)
            });
        }

        public static Card WarningShot()
        {
            return new Card("Warning Shot", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(4),
                new GainVolleyEffect(1)
            }, tags: new[] { CardTag.Shot });
        }

        public static Card SteadyAim()
        {
            return new Card("Steady Aim", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new GainVolleyEffect(2),
                new GainBlockEffect(4)
            });
        }

        public static Card CalledShot()
        {
            return new Card("Called Shot", energyCost: 2, CardType.Attack, new CardEffect[]
            {
                new ConditionalDoubleHitDamageEffect(amount: 6, volleyThreshold: 4)
            }, tags: new[] { CardTag.Shot });
        }

        public static Card FullDraw()
        {
            return new Card("Full Draw", energyCost: 2, CardType.Attack, new CardEffect[]
            {
                new ConsumeVolleyDamageEffect(damagePerPoint: 5)
            });
        }

        public static Card LooseArrow()
        {
            return Card.Attack("Loose Arrow", energyCost: 0, damage: 3, tags: new[] { CardTag.Shot });
        }

        public static Card PreciseShot()
        {
            return Card.Attack("Precise Shot", energyCost: 1, damage: 9, tags: new[] { CardTag.Shot });
        }

        public static Card Pathfinder()
        {
            return new Card("Pathfinder", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new DrawCardEffect(2),
                new DiscardCardWithoutTagEffect(CardTag.Shot)
            });
        }

        public static Card PointBlank()
        {
            return new Card("Point-Blank", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(7),
                new ConditionalApplyStatusEffect(StatusType.Exposed, StatusType.Drained, 1, EffectTarget.Enemy)
            }, tags: new[] { CardTag.Shot });
        }

        public static Card Flurry()
        {
            return new Card("Flurry", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new ShotCountScaledDamageEffect(baseAmount: 4, bonusPerShot: 2)
            }, tags: new[] { CardTag.Shot });
        }

        public static Card Quickdraw()
        {
            return new Card("Quickdraw", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new DrawCardEffect(1),
                new ReduceShotCostEffect(1)
            });
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
