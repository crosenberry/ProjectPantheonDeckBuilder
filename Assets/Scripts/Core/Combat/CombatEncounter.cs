namespace Pantheon.Core.Combat
{
    public class CombatEncounter
    {
        public Player Player { get; }
        public Enemy Enemy { get; }
        public CombatOutcome Outcome { get; private set; }

        public CombatEncounter(Player player, Enemy enemy)
        {
            Player = player;
            Enemy = enemy;
            Outcome = CombatOutcome.InProgress;
        }

        public void EndPlayerTurn()
        {
            Player.EndTurn();

            if (Enemy.CurrentHP <= 0)
            {
                Outcome = CombatOutcome.PlayerWon;
                return;
            }

            Enemy.StartTurn();
            CombatResolver.EnemyAttack(Enemy, Player);

            if (Player.CurrentHP <= 0)
            {
                Outcome = CombatOutcome.PlayerLost;
            }
        }
    }
}
