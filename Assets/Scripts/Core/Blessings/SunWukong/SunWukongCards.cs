using System.Collections.Generic;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Blessings.SunWukong
{
    public static class SunWukongCards
    {
        public static Card ApeFist()
        {
            return Card.Attack("Ape Fist", energyCost: 1, damage: 5);
        }

        public static Card CloudStep()
        {
            return Card.Skill("Cloud Step", energyCost: 1, block: 5);
        }

        public static Card RuyiStrike()
        {
            return new Card("Ruyi Strike", energyCost: 2, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(9),
                new ChangeFormEffect()
            });
        }

        public static Card BeastAwakening()
        {
            return new Card("Beast Awakening", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new ChangeToFormEffect(Form.Beast),
                new GainBlockEffect(3)
            });
        }

        public static Card PrimalRoar()
        {
            return new Card("Primal Roar", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new ApplyStatusEffect(StatusType.Strength, 1, EffectTarget.Self)
            });
        }

        public static Card RecklessApe()
        {
            return new Card("Reckless Ape", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(5),
                new LoseHPEffect(1)
            });
        }

        public static Card Rampage()
        {
            return Card.Attack("Rampage", energyCost: 2, damage: 8);
        }

        public static Card HavocInHeaven()
        {
            return new Card("Havoc in Heaven", energyCost: 3, CardType.Attack, new CardEffect[]
            {
                new MultiHitDamageEffect(amount: 8, hitCount: 3)
            });
        }

        public static Card ImmortalAscension()
        {
            return new Card("Immortal Ascension", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new ChangeToFormEffect(Form.Immortal),
                new ApplyStatusEffect(StatusType.Strength, 2, EffectTarget.Self)
            });
        }

        public static Card SacredPeach()
        {
            return new Card("Sacred Peach", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new HealEffect(4)
            });
        }

        public static Card CelestialWard()
        {
            return Card.Skill("Celestial Ward", energyCost: 1, block: 6);
        }

        public static Card PeachOfLongevity()
        {
            return new Card("Peach of Longevity", energyCost: 2, CardType.Skill, new CardEffect[]
            {
                new HealEffect(10),
                new ApplyStatusEffect(StatusType.Strength, 1, EffectTarget.Self)
            });
        }

        public static Card AscensionOfTheSage()
        {
            return new Card("Ascension of the Sage", energyCost: 3, CardType.Skill, new CardEffect[]
            {
                new ChangeToFormEffect(Form.Immortal),
                new ApplyStatusEffect(StatusType.Strength, 5, EffectTarget.Self)
            }, tags: new[] { CardTag.Exhaust });
        }

        public static Card ShiftingStance()
        {
            return new Card("Shifting Stance", energyCost: 0, CardType.Skill, new CardEffect[]
            {
                new ChangeFormEffect()
            });
        }

        public static Card FickleStrike()
        {
            return new Card("Fickle Strike", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(5),
                new ChangeFormEffect()
            });
        }

        public static Card AdaptiveGuard()
        {
            return new Card("Adaptive Guard", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new ConditionalGainBlockIfChangedFormEffect(baseAmount: 5, bonusAmount: 3)
            });
        }

        public static Card WhirlingTransformation()
        {
            return new Card("Whirling Transformation", energyCost: 2, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(10),
                new ChangeFormEffect()
            });
        }

        public static Card SeventyTwoChanges()
        {
            return new Card("72 Changes", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new ChangeFormEffect(),
                new DrawCardEffect(2)
            });
        }

        public static IEnumerable<Card> StarterDeck()
        {
            for (var i = 0; i < 5; i++)
            {
                yield return ApeFist();
            }

            for (var i = 0; i < 4; i++)
            {
                yield return CloudStep();
            }

            yield return RuyiStrike();
        }
    }
}
