using System.Collections.Generic;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Enemies.Egyptian
{
    public static class EgyptianEnemies
    {
        public static Enemy UshabtiSentinel(IRandom random)
        {
            return new Enemy(maxHP: 40, new[]
            {
                new EnemyMove("Attack", IntentType.Attack, value: 9, weight: 3),
                new EnemyMove("Guard", IntentType.Block, value: 8, weight: 1)
            }, random);
        }

        public static Enemy SetsCultist(IRandom random)
        {
            return new Enemy(maxHP: 26, new[]
            {
                new EnemyMove("Curse", IntentType.Debuff, value: 2, weight: 1, status: StatusType.Exposed),
                new EnemyMove("Attack", IntentType.Attack, value: 6, weight: 1)
            }, random);
        }

        public static Enemy ScarabSwarm(IRandom random)
        {
            return new Enemy(maxHP: 12, new[]
            {
                new EnemyMove("Bite", IntentType.Attack, value: 5, weight: 1)
            }, random);
        }

        public static IEnumerable<Enemy> ScarabSwarmPack(int count, IRandom random)
        {
            for (var i = 0; i < count; i++)
            {
                yield return ScarabSwarm(random);
            }
        }
    }
}
