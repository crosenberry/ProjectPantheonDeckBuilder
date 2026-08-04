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
    }
}
