using System.Collections.Generic;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Enemies.Greek
{
    public static class GreekEnemies
    {
        public static Enemy HopliteSkirmisher(IRandom random)
        {
            return new Enemy(maxHP: 42, new[]
            {
                new EnemyMove("Attack", IntentType.Attack, value: 9, weight: 3),
                new EnemyMove("Guard", IntentType.Block, value: 8, weight: 1)
            }, random);
        }

        public static Enemy HarpyScreecher(IRandom random)
        {
            return new Enemy(maxHP: 30, new[]
            {
                new EnemyMove("Shriek", IntentType.Debuff, value: 2, weight: 1, status: StatusType.Drained),
                new EnemyMove("Claw", IntentType.Attack, value: 6, weight: 1)
            }, random);
        }

        public static Enemy Viper(IRandom random)
        {
            return new Enemy(maxHP: 12, new[]
            {
                new EnemyMove("Bite", IntentType.Attack, value: 4, weight: 1)
            }, random);
        }

        public static IEnumerable<Enemy> ViperBrood(int count, IRandom random)
        {
            for (var i = 0; i < count; i++)
            {
                yield return Viper(random);
            }
        }
    }
}
