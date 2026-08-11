using System.Collections.Generic;
using Pantheon.Core.Meta;

namespace Pantheon.Core.Run
{
    public static class BossReward
    {
        public static IReadOnlyList<Relic> Roll(IReadOnlyList<Relic> mythologyPool, Relic bossExclusiveRelic, IRandom random, int bossExclusiveChancePercent)
        {
            var pool = new List<Relic>(mythologyPool);
            var offered = new List<Relic>();

            if (random.Next(100) < bossExclusiveChancePercent)
            {
                offered.Add(bossExclusiveRelic);
            }

            while (offered.Count < 2)
            {
                var index = random.Next(pool.Count);
                offered.Add(pool[index]);
                pool.RemoveAt(index);
            }

            return offered;
        }
    }
}
