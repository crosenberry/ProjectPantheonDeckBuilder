using System.Collections.Generic;
using System.Linq;

namespace Pantheon.Core.Combat
{
    public class CombatEncounter
    {
        public Player Player { get; }
        public IReadOnlyList<Enemy> Enemies { get; }
        public Enemy Enemy => Enemies[0];
        public CombatOutcome Outcome { get; private set; }

        public CombatEncounter(Player player, Enemy enemy) : this(player, new[] { enemy })
        {
        }

        public CombatEncounter(Player player, IReadOnlyList<Enemy> enemies)
        {
            Player = player;
            Enemies = enemies;
            Outcome = CombatOutcome.InProgress;
        }

        public void EndPlayerTurn()
        {
            Player.EndTurn();

            if (Enemies.All(enemy => enemy.CurrentHP <= 0))
            {
                Outcome = CombatOutcome.PlayerWon;
                return;
            }

            foreach (var enemy in Enemies.Where(enemy => enemy.CurrentHP > 0))
            {
                enemy.StartTurn();

                if (enemy.Moves.Count > 0)
                {
                    CombatResolver.ExecuteEnemyIntent(enemy, Player);
                }
                else
                {
                    CombatResolver.EnemyAttack(enemy, Player);
                }
            }

            if (Player.CurrentHP <= 0)
            {
                Outcome = CombatOutcome.PlayerLost;
            }
        }
    }
}
