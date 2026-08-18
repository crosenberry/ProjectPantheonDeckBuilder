using System.Collections.Generic;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Enemies.Norse
{
    public static class NorseEnemies
    {
        public static Enemy DraugrReaver(IRandom random)
        {
            return new Enemy(maxHP: 44, new[]
            {
                new EnemyMove("Attack", IntentType.Attack, value: 10, weight: 3),
                new EnemyMove("Guard", IntentType.Block, value: 7, weight: 1)
            }, random);
        }

        public static Enemy SeidrHexer(IRandom random)
        {
            return new Enemy(maxHP: 28, new[]
            {
                new EnemyMove("Hex", IntentType.Debuff, value: 2, weight: 1, status: StatusType.Sundered),
                new EnemyMove("Claw Strike", IntentType.Attack, value: 6, weight: 1)
            }, random);
        }

        public static Enemy Wolf(IRandom random)
        {
            return new Enemy(maxHP: 12, new[]
            {
                new EnemyMove("Bite", IntentType.Attack, value: 5, weight: 1)
            }, random);
        }

        public static IEnumerable<Enemy> WolfPack(int count, IRandom random)
        {
            for (var i = 0; i < count; i++)
            {
                yield return Wolf(random);
            }
        }
    }
}
