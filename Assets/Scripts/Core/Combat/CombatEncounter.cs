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

            CombatResolver.FireTriggers(Player, TriggerEvent.CombatStarted, Enemy, Enemies);
        }

        public void PlayCard(Card card, Enemy target)
        {
            CombatResolver.PlayCard(Player, card, target, Enemies);
            CheckOutcome();
        }

        public void StartPlayerTurn(int cardsToDraw)
        {
            Player.StartTurn(cardsToDraw);
            CombatResolver.FireTriggers(Player, TriggerEvent.TurnStarted, Enemy, Enemies);
        }

        public void EndPlayerTurn()
        {
            Player.EndTurn();
            CombatResolver.FireTriggers(Player, TriggerEvent.TurnEnded, Enemy, Enemies);
            CheckOutcome();

            if (Outcome != CombatOutcome.InProgress)
            {
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

            CheckOutcome();
        }

        private void CheckOutcome()
        {
            if (Outcome != CombatOutcome.InProgress)
            {
                return;
            }

            if (Enemies.All(enemy => enemy.CurrentHP <= 0))
            {
                Outcome = CombatOutcome.PlayerWon;
                return;
            }

            if (Player.CurrentHP <= 0)
            {
                Outcome = CombatOutcome.PlayerLost;
            }
        }
    }
}
