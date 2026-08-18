using System.Collections.Generic;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Blessings.Thor
{
    public static class ThorCards
    {
        public static Card HammerStrike()
        {
            return Card.Attack("Hammer Strike", energyCost: 1, damage: 6);
        }

        public static Card StormWard()
        {
            return new Card("Storm Ward", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new GainBlockEffect(5),
                new GainStormEffect(1)
            }, tags: new[] { CardTag.StormGenerating });
        }

        public static Card MjolnirsCall()
        {
            return new Card("Mjolnir's Call", energyCost: 2, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(9),
                new GainStormEffect(2)
            }, tags: new[] { CardTag.StormGenerating });
        }

        public static Card ShieldWall()
        {
            return new Card("Shield Wall", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new GainBlockEffect(9),
                new GainStormEffect(2)
            }, tags: new[] { CardTag.StormGenerating });
        }

        public static Card Deflect()
        {
            return new Card("Deflect", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new ConditionalGainBlockByStormEffect(baseAmount: 5, thresholdAmount: 8, stormThreshold: 5)
            });
        }

        public static Card ThunderousRetort()
        {
            return new Card("Thunderous Retort", energyCost: 2, CardType.Attack, new CardEffect[]
            {
                new DamageEqualToCurrentBlockEffect()
            });
        }

        public static Card TitansBulwark()
        {
            return new Card("Titan's Bulwark", energyCost: 2, CardType.Skill, new CardEffect[]
            {
                new GainBlockEffect(15),
                new GainStormEffect(4)
            }, tags: new[] { CardTag.Exhaust });
        }

        public static Card Thunderclap()
        {
            return new Card("Thunderclap", energyCost: 2, CardType.Attack, new CardEffect[]
            {
                new ConsumeStormDamageToAllEnemiesEffect(damagePerPoint: 4)
            });
        }

        public static Card ChargedStrike()
        {
            return new Card("Charged Strike", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(6),
                new GainStormEffect(1)
            }, tags: new[] { CardTag.StormGenerating });
        }

        public static Card DischargeBolt()
        {
            return new Card("Discharge Bolt", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new ConsumeStormOrFallbackDamageEffect(consumeAmount: 1, damageIfConsumed: 9, damageIfNotConsumed: 4)
            });
        }

        public static Card StaticGrip()
        {
            return new Card("Static Grip", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new GainStormEffect(1),
                new DrawCardEffect(1)
            }, tags: new[] { CardTag.StormGenerating });
        }

        public static Card Galvanize()
        {
            return new Card("Galvanize", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new GainStormEffect(3),
                new DrawCardEffect(1)
            }, tags: new[] { CardTag.StormGenerating });
        }

        public static Card Stormbrand()
        {
            return new Card("Stormbrand", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new ConsumeAllStormOrBaseDamageEffect(baseAmount: 3, damagePerPointIfConsumed: 5)
            });
        }

        public static Card RecklessSwing()
        {
            return new Card("Reckless Swing", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(10),
                new LoseHPEffect(3)
            });
        }

        public static Card Bloodrage()
        {
            return new Card("Bloodrage", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new ApplyStatusEffect(StatusType.Strength, 2, EffectTarget.Self),
                new LoseHPEffect(3)
            });
        }

        public static Card HeadlongCharge()
        {
            return Card.Attack("Headlong Charge", energyCost: 2, damage: 14);
        }

        public static Card FeralRoar()
        {
            return new Card("Feral Roar", energyCost: 2, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(12),
                new ApplyStatusEffect(StatusType.Strength, 2, EffectTarget.Self)
            });
        }

        public static Card YmirsWrath()
        {
            return new Card("Ymir's Wrath", energyCost: 3, CardType.Attack, new CardEffect[]
            {
                new DealDamageEffect(20),
                new LoseHPEffect(5)
            });
        }

        public static IEnumerable<Card> StarterDeck()
        {
            for (var i = 0; i < 5; i++)
            {
                yield return HammerStrike();
            }

            for (var i = 0; i < 4; i++)
            {
                yield return StormWard();
            }

            yield return MjolnirsCall();
        }
    }
}
