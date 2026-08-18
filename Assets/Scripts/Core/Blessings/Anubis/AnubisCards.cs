using System.Collections.Generic;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Blessings.Anubis
{
    public static class AnubisCards
    {
        public static Card JackalsBite()
        {
            return Card.Attack("Jackal's Bite", energyCost: 1, damage: 6);
        }

        public static Card CanopicWard()
        {
            return Card.Skill("Canopic Ward", energyCost: 1, block: 5);
        }

        public static Card ScalesOfJudgment()
        {
            return new Card("Scales of Judgment", energyCost: 2, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(8),
                new AdjustScaleEffect(1)
            });
        }

        public static Card EvenKeel()
        {
            return new Card("Even Keel", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new DrawCardIfScaleBalancedEffect(baseAmount: 1, bonusAmount: 1)
            });
        }

        public static Card MaatsFeather()
        {
            return new Card("Ma'at's Feather", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(4),
                new ApplyStatusIfScaleBalancedEffect(StatusType.Exposed, 1, EffectTarget.Enemy)
            });
        }

        public static Card Equilibrium()
        {
            return new Card("Equilibrium", energyCost: 1, CardType.Power, new CardEffect[0],
                tags: new[] { CardTag.Exhaust },
                triggers: new[] { new TriggeredEffect(TriggerEvent.TurnEnded, new GainBlockIfScaleBalancedEffect(3)) });
        }

        public static Card ScaleTipper()
        {
            return new Card("Scale-Tipper", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new SetScaleEffect(0),
                new DrawCardEffect(2)
            });
        }

        public static Card JudgmentIncarnate()
        {
            return new Card("Judgment Incarnate", energyCost: 3, CardType.Attack, new CardEffect[]
            {
                new DamageInverseScaledByBalanceEffect(multiplier: 4, maxMagnitude: 5)
            });
        }

        public static Card AmmitsHunger()
        {
            return new Card("Ammit's Hunger", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(7),
                new AdjustScaleEffect(-1)
            });
        }

        public static Card ChaosRite()
        {
            return new Card("Chaos Rite", energyCost: 0, CardType.Skill, new CardEffect[]
            {
                new LoseHPEffect(2),
                new AdjustScaleEffect(-2)
            });
        }

        public static Card SerpentsBite()
        {
            return new Card("Serpent's Bite", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new ConditionalDamageIfScaleNegativeEffect(baseAmount: 5, bonusAmount: 3)
            });
        }

        public static Card ChaosboundStrike()
        {
            return new Card("Chaosbound Strike", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new ConditionalDamageIfScaleThresholdEffect(baseAmount: 6, bonusAmount: 6, threshold: -3)
            });
        }

        public static Card DevourersToll()
        {
            return new Card("Devourer's Toll", energyCost: 2, CardType.Attack, new CardEffect[]
            {
                new ScaleMagnitudeDamageWithConditionalExposedEffect(damagePerPoint: 3)
            });
        }

        public static Card MaatsShield()
        {
            return new Card("Ma'at's Shield", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new GainBlockEffect(7),
                new AdjustScaleEffect(1)
            });
        }

        public static Card SacredRite()
        {
            return new Card("Sacred Rite", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new HealEffect(4),
                new AdjustScaleEffect(1)
            });
        }

        public static Card SunboundWard()
        {
            return new Card("Sunbound Ward", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new ConditionalGainBlockIfScalePositiveEffect(baseAmount: 6, bonusAmount: 3)
            });
        }

        public static Card OsirianRenewal()
        {
            return new Card("Osirian Renewal", energyCost: 2, CardType.Skill, new CardEffect[]
            {
                new HealEffect(8),
                new AdjustScaleEffect(3)
            });
        }

        public static Card MaatsAscension()
        {
            return new Card("Ma'at's Ascension", energyCost: 3, CardType.Attack, new CardEffect[]
            {
                new ConsumeScaleIfPositiveDamageToAllEnemiesEffect(damagePerPoint: 5)
            });
        }

        public static IEnumerable<Card> StarterDeck()
        {
            for (var i = 0; i < 5; i++)
            {
                yield return JackalsBite();
            }

            for (var i = 0; i < 4; i++)
            {
                yield return CanopicWard();
            }

            yield return ScalesOfJudgment();
        }
    }
}
