using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Meta;
using Pantheon.Core.Run;

namespace Pantheon.Core.Tests.Run
{
    public class BossRewardTests
    {
        private static readonly Relic RelicA = new Relic("Relic A", "Flavor A");
        private static readonly Relic RelicB = new Relic("Relic B", "Flavor B");
        private static readonly Relic RelicC = new Relic("Relic C", "Flavor C");
        private static readonly Relic BossExclusive = new Relic("Boss Exclusive", "Flavor Exclusive");

        [Test]
        public void Roll_AlwaysReturnsTwoRelics()
        {
            var pool = new[] { RelicA, RelicB, RelicC };

            var offered = BossReward.Roll(pool, BossExclusive, new FixedValueRandom(0), bossExclusiveChancePercent: 50);

            Assert.That(offered.Count, Is.EqualTo(2));
        }

        [Test]
        public void Roll_RandomBelowThreshold_IncludesBossExclusiveRelic()
        {
            var pool = new[] { RelicA, RelicB, RelicC };

            var offered = BossReward.Roll(pool, BossExclusive, new FixedValueRandom(0), bossExclusiveChancePercent: 50);

            Assert.That(offered, Contains.Item(BossExclusive));
        }

        [Test]
        public void Roll_ChancePercentZero_NeverIncludesBossExclusiveRelic()
        {
            var pool = new[] { RelicA, RelicB, RelicC };

            var offered = BossReward.Roll(pool, BossExclusive, new FixedValueRandom(0), bossExclusiveChancePercent: 0);

            Assert.That(offered, Has.No.Member(BossExclusive));
        }

        [Test]
        public void Roll_NoBossExclusive_ReturnsTwoDistinctPoolRelics()
        {
            var pool = new[] { RelicA, RelicB, RelicC };

            var offered = BossReward.Roll(pool, BossExclusive, new FixedValueRandom(0), bossExclusiveChancePercent: 0);

            Assert.That(offered.Distinct().Count(), Is.EqualTo(2));
            Assert.That(offered.All(r => pool.Contains(r)), Is.True);
        }

        [Test]
        public void Roll_BossExclusiveHit_SecondSlotIsFromPool()
        {
            var pool = new[] { RelicA, RelicB, RelicC };

            var offered = BossReward.Roll(pool, BossExclusive, new FixedValueRandom(0), bossExclusiveChancePercent: 100);

            Assert.That(offered.Count(r => r == BossExclusive), Is.EqualTo(1));
            Assert.That(offered.Any(r => pool.Contains(r)), Is.True);
        }
    }
}
