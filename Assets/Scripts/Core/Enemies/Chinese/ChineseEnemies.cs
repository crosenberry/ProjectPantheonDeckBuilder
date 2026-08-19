using System.Collections.Generic;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Enemies.Chinese
{
    public static class ChineseEnemies
    {
        public static Enemy HeavenlySoldier(IRandom random)
        {
            return new Enemy(maxHP: 42, new[]
            {
                new EnemyMove("Attack", IntentType.Attack, value: 9, weight: 3),
                new EnemyMove("Guard", IntentType.Block, value: 8, weight: 1)
            }, random);
        }

        public static Enemy NineTailedFoxSpirit(IRandom random)
        {
            return new Enemy(maxHP: 27, new[]
            {
                new EnemyMove("Gather Power", IntentType.Buff, value: 2, weight: 1, status: StatusType.Strength),
                new EnemyMove("Attack", IntentType.Attack, value: 6, weight: 1)
            }, random);
        }

        public static Enemy YakshaSwarm(IRandom random)
        {
            return new Enemy(maxHP: 12, new[]
            {
                new EnemyMove("Strike", IntentType.Attack, value: 5, weight: 1)
            }, random);
        }

        public static IEnumerable<Enemy> YakshaSwarmPack(int count, IRandom random)
        {
            for (var i = 0; i < count; i++)
            {
                yield return YakshaSwarm(random);
            }
        }
    }
}
