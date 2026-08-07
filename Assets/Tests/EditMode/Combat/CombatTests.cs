using System;
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

        [Test]
        public void EndPlayerTurn_MoveBasedEnemy_ExecutesCurrentIntentInsteadOfLegacyAttack()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var moves = new[] { new EnemyMove("Bite", IntentType.Attack, value: 9) };
            var enemy = new Enemy(maxHP: 12, moves, new FakeRandom());
            var combat = new CombatEncounter(player, enemy);

            combat.EndPlayerTurn();

            Assert.That(player.CurrentHP, Is.EqualTo(61));
        }

        [Test]
        public void EndPlayerTurn_MoveBasedEnemy_ChoosesNewIntentAfterActing()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var moves = new[]
            {
                new EnemyMove("Attack", IntentType.Attack, value: 9, weight: 3),
                new EnemyMove("Guard", IntentType.Block, value: 8, weight: 1)
            };
            var enemy = new Enemy(maxHP: 42, moves, new SequenceRandom(0, 3));
            var combat = new CombatEncounter(player, enemy);
            var firstIntent = enemy.CurrentIntent;

            combat.EndPlayerTurn();

            Assert.That(firstIntent, Is.EqualTo(moves[0]));
            Assert.That(enemy.CurrentIntent, Is.EqualTo(moves[1]));
        }

        [Test]
        public void PlayCard_KillsOnlyEnemy_SetsPlayerWonImmediatelyWithoutEndingTurn()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 5);
            var card = Card.Attack("Killing Blow", energyCost: 1, damage: 10);
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            var combat = new CombatEncounter(player, enemy);

            combat.PlayCard(card, enemy);

            Assert.That(combat.Outcome, Is.EqualTo(CombatOutcome.PlayerWon));
        }

        [Test]
        public void PlayCard_EnemySurvives_OutcomeStaysInProgress()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            var combat = new CombatEncounter(player, enemy);

            combat.PlayCard(card, enemy);

            Assert.That(combat.Outcome, Is.EqualTo(CombatOutcome.InProgress));
        }

        [Test]
        public void PlayCard_MultipleEnemiesOneSurvives_OutcomeStaysInProgress()
        {
            var player = new Player(startingEnergy: 1);
            var enemyA = new Enemy(maxHP: 5);
            var enemyB = new Enemy(maxHP: 42);
            var card = Card.Attack("Killing Blow", energyCost: 1, damage: 10);
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            var combat = new CombatEncounter(player, new[] { enemyA, enemyB });

            combat.PlayCard(card, enemyA);

            Assert.That(combat.Outcome, Is.EqualTo(CombatOutcome.InProgress));
        }

        [Test]
        public void PlayCard_KillsLastRemainingEnemy_SetsPlayerWonImmediately()
        {
            var player = new Player(startingEnergy: 1);
            var enemyA = new Enemy(maxHP: 5);
            var enemyB = new Enemy(maxHP: 5);
            enemyB.TakeDamage(999);
            var card = Card.Attack("Killing Blow", energyCost: 1, damage: 10);
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            var combat = new CombatEncounter(player, new[] { enemyA, enemyB });

            combat.PlayCard(card, enemyA);

            Assert.That(combat.Outcome, Is.EqualTo(CombatOutcome.PlayerWon));
        }

        [Test]
        public void PlayCard_MultiTargetEffect_HitsEveryEnemyInEncounter()
        {
            var player = new Player(startingEnergy: 1);
            var enemyA = new Enemy(maxHP: 42);
            var enemyB = new Enemy(maxHP: 30);
            var card = new Card("Rain of Arrows", energyCost: 1, CardType.Attack, new CardEffect[]
            {
                new VolleyScaledDamageToAllEnemiesEffect(baseAmount: 3, bonusPerVolley: 0)
            });
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            var combat = new CombatEncounter(player, new[] { enemyA, enemyB });

            combat.PlayCard(card, enemyA);

            Assert.That(enemyA.CurrentHP, Is.EqualTo(39));
            Assert.That(enemyB.CurrentHP, Is.EqualTo(27));
        }

        [Test]
        public void PlayCard_CardNotInHand_Throws()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var phantomCard = Card.Attack("Phantom", energyCost: 1, damage: 6);
            var combat = new CombatEncounter(player, enemy);

            Assert.Throws<InvalidOperationException>(() => combat.PlayCard(phantomCard, enemy));
        }
    }
}
