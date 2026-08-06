using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat
{
    public class CombatTests
    {
        [Test]
        public void EndPlayerTurn_EnemyAlive_EnemyAttacksPlayer()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);
            var combat = new CombatEncounter(player, enemy);

            combat.EndPlayerTurn();

            Assert.That(player.CurrentHP, Is.EqualTo(60));
        }

        [Test]
        public void EndPlayerTurn_EnemyAlreadyDefeated_SkipsAttackAndSetsPlayerWon()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);
            enemy.TakeDamage(999);
            var combat = new CombatEncounter(player, enemy);

            combat.EndPlayerTurn();

            Assert.That(player.CurrentHP, Is.EqualTo(70));
            Assert.That(combat.Outcome, Is.EqualTo(CombatOutcome.PlayerWon));
        }

        [Test]
        public void EndPlayerTurn_EnemyAttackDefeatsPlayer_SetsPlayerLost()
        {
            var player = new Player(startingEnergy: 3, startingHP: 5, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);
            var combat = new CombatEncounter(player, enemy);

            combat.EndPlayerTurn();

            Assert.That(player.CurrentHP, Is.EqualTo(0));
            Assert.That(combat.Outcome, Is.EqualTo(CombatOutcome.PlayerLost));
        }

        [Test]
        public void EndPlayerTurn_BothSurvive_OutcomeStaysInProgress()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);
            var combat = new CombatEncounter(player, enemy);

            combat.EndPlayerTurn();

            Assert.That(combat.Outcome, Is.EqualTo(CombatOutcome.InProgress));
        }

        [Test]
        public void EndPlayerTurn_EnemyExposedAboveZero_DecrementsAtStartOfEnemyTurn()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);
            enemy.ApplyExposed(2);
            var combat = new CombatEncounter(player, enemy);

            combat.EndPlayerTurn();

            Assert.That(enemy.Exposed, Is.EqualTo(1));
        }

        [Test]
        public void EndPlayerTurn_EnemyAlreadyDefeated_DoesNotStartEnemyTurn()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);
            enemy.ApplyExposed(2);
            enemy.TakeDamage(999);
            var combat = new CombatEncounter(player, enemy);

            combat.EndPlayerTurn();

            Assert.That(enemy.Exposed, Is.EqualTo(2));
        }

        [Test]
        public void EndPlayerTurn_MultipleEnemiesAlive_AllEnemiesAttackPlayer()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemyA = new Enemy(maxHP: 42, attackDamage: 10);
            var enemyB = new Enemy(maxHP: 30, attackDamage: 5);
            var combat = new CombatEncounter(player, new[] { enemyA, enemyB });

            combat.EndPlayerTurn();

            Assert.That(player.CurrentHP, Is.EqualTo(55));
        }

        [Test]
        public void EndPlayerTurn_FirstOfMultipleEnemiesDefeated_SurvivingLaterEnemyStillAttacks()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemyA = new Enemy(maxHP: 42, attackDamage: 10);
            var enemyB = new Enemy(maxHP: 30, attackDamage: 5);
            enemyA.TakeDamage(999);
            var combat = new CombatEncounter(player, new[] { enemyA, enemyB });

            combat.EndPlayerTurn();

            Assert.That(player.CurrentHP, Is.EqualTo(65));
        }

        [Test]
        public void EndPlayerTurn_FirstEnemyDeadSecondAlive_OutcomeStaysInProgress()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemyA = new Enemy(maxHP: 42, attackDamage: 10);
            var enemyB = new Enemy(maxHP: 30, attackDamage: 5);
            enemyA.TakeDamage(999);
            var combat = new CombatEncounter(player, new[] { enemyA, enemyB });

            combat.EndPlayerTurn();

            Assert.That(combat.Outcome, Is.EqualTo(CombatOutcome.InProgress));
        }

        [Test]
        public void EndPlayerTurn_AllEnemiesDefeated_SetsPlayerWon()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemyA = new Enemy(maxHP: 42, attackDamage: 10);
            var enemyB = new Enemy(maxHP: 30, attackDamage: 5);
            enemyA.TakeDamage(999);
            enemyB.TakeDamage(999);
            var combat = new CombatEncounter(player, new[] { enemyA, enemyB });

            combat.EndPlayerTurn();

            Assert.That(player.CurrentHP, Is.EqualTo(70));
            Assert.That(combat.Outcome, Is.EqualTo(CombatOutcome.PlayerWon));
        }

        [Test]
        public void Enemy_SingleEnemyConstructor_ReturnsThatEnemy()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);
            var combat = new CombatEncounter(player, enemy);

            Assert.That(combat.Enemy, Is.EqualTo(enemy));
            Assert.That(combat.Enemies.Count, Is.EqualTo(1));
        }
    }
}
