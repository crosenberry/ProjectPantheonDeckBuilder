using Pantheon.Core.Combat;

namespace Pantheon.Core.Syncretism
{
    // Shape 3 (resource-interaction cards) from the Artemis+Thor prototype
    // (Docs/Syncretism/Artemis-Thor-Prototype.md), committed as Syncretism's
    // only mechanical shape for M5 - the only one of the 3 prototyped shapes
    // that actually got pressure-tested. No unlock gating: these are just
    // cards, playable/testable like any other. Aegis of the Hunt (the
    // prototype's 5th card) is held back - its triggers need TriggerEvent
    // types the system doesn't have yet (on-play, on-resource-gain).
    public static class ArtemisThorCards
    {
        public static Card TwinStorms()
        {
            return new Card("Twin Storms", energyCost: 2, CardType.Attack, new CardEffect[]
            {
                new ConditionalDamageIfStormPresentEffect(baseAmount: 7, bonusAmount: 4),
                new GainStormEffect(1)
            }, tags: new[] { CardTag.Shot });
        }

        public static Card HuntersSquall()
        {
            return new Card("Hunter's Squall", energyCost: 1, CardType.Skill, new CardEffect[]
            {
                new GainBlockEffect(6),
                new GainStormEffect(1),
                new GainVolleyEffect(1)
            });
        }

        public static Card ThunderousVolley()
        {
            return new Card("Thunderous Volley", energyCost: 2, CardType.Attack, new CardEffect[]
            {
                new ConsumeStormPlusVolleyBonusDamageToAllEnemiesEffect(damagePerStorm: 3, bonusPerVolley: 2)
            });
        }

        public static Card RagnaroksQuarry()
        {
            return new Card("Ragnarok's Quarry", energyCost: 3, CardType.Attack, new CardEffect[]
            {
                new ConsumeStormDamageDoubleIfVolleyThresholdEffect(damagePerPoint: 6, volleyThreshold: 3)
            });
        }
    }
}
