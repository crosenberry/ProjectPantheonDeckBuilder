using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat
{
    public class EnemyTests
    {
        [Test]
        public void TakeDamage_ReducesCurrentHP()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.TakeDamage(9);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
        }

        [Test]
        public void TakeDamage_AmountExceedsCurrentHP_ClampsAtZero()
        {
            var enemy = new Enemy(maxHP: 10);

            enemy.TakeDamage(999);

            Assert.That(enemy.CurrentHP, Is.EqualTo(0));
        }

        [Test]
        public void GainStrength_IncreasesStrength()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.GainStrength(2);

            Assert.That(enemy.Strength, Is.EqualTo(2));
        }

        [Test]
        public void ApplyExposed_IncreasesExposed()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.ApplyExposed(2);

            Assert.That(enemy.Exposed, Is.EqualTo(2));
        }

        [Test]
        public void ApplyDrained_IncreasesDrained()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.ApplyDrained(2);

            Assert.That(enemy.Drained, Is.EqualTo(2));
        }

        [Test]
        public void StartTurn_ExposedAboveZero_DecrementsByOne()
        {
            var enemy = new Enemy(maxHP: 42);
            enemy.ApplyExposed(2);

            enemy.StartTurn();

            Assert.That(enemy.Exposed, Is.EqualTo(1));
        }

        [Test]
        public void StartTurn_ExposedAtZero_StaysAtZero()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.StartTurn();

            Assert.That(enemy.Exposed, Is.EqualTo(0));
        }

        [Test]
        public void StartTurn_DrainedAboveZero_DecrementsByOne()
        {
            var enemy = new Enemy(maxHP: 42);
            enemy.ApplyDrained(2);

            enemy.StartTurn();

            Assert.That(enemy.Drained, Is.EqualTo(1));
        }

        [Test]
        public void StartTurn_StrengthDoesNotDecay()
        {
            var enemy = new Enemy(maxHP: 42);
            enemy.GainStrength(3);

            enemy.StartTurn();

            Assert.That(enemy.Strength, Is.EqualTo(3));
        }

        [Test]
        public void GainBlock_IncreasesCurrentBlock()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.GainBlock(5);

            Assert.That(enemy.CurrentBlock, Is.EqualTo(5));
        }

        [Test]
        public void TakeDamage_DamageLessThanBlock_ReducesBlockOnlyNoHPLoss()
        {
            var enemy = new Enemy(maxHP: 42);
            enemy.GainBlock(10);

            enemy.TakeDamage(6);

            Assert.That(enemy.CurrentBlock, Is.EqualTo(4));
            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
        }

        [Test]
        public void TakeDamage_DamageExceedsBlock_ExcessReducesHP()
        {
            var enemy = new Enemy(maxHP: 42);
            enemy.GainBlock(5);

            enemy.TakeDamage(8);

            Assert.That(enemy.CurrentBlock, Is.EqualTo(0));
            Assert.That(enemy.CurrentHP, Is.EqualTo(39));
        }

        [Test]
        public void StartTurn_ResetsBlockToZero()
        {
            var enemy = new Enemy(maxHP: 42);
            enemy.GainBlock(8);

            enemy.StartTurn();

            Assert.That(enemy.CurrentBlock, Is.EqualTo(0));
        }

        [Test]
        public void LegacyConstructor_MovesIsEmpty()
        {
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);

            Assert.That(enemy.Moves.Count, Is.EqualTo(0));
            Assert.That(enemy.CurrentIntent, Is.Null);
        }

        [Test]
        public void MoveBasedConstructor_SingleMove_SetsCurrentIntentImmediately()
        {
            var moves = new[] { new EnemyMove("Bite", IntentType.Attack, value: 4) };

            var enemy = new Enemy(maxHP: 12, moves, new FakeRandom());

            Assert.That(enemy.CurrentIntent, Is.EqualTo(moves[0]));
        }

        [Test]
        public void ChooseNextIntent_RollWithinFirstMovesWeight_SelectsFirstMove()
        {
            var moves = new[]
            {
                new EnemyMove("Attack", IntentType.Attack, value: 9, weight: 3),
                new EnemyMove("Guard", IntentType.Block, value: 8, weight: 1)
            };
            var enemy = new Enemy(maxHP: 42, moves, new FixedValueRandom(2));

            enemy.ChooseNextIntent();

            Assert.That(enemy.CurrentIntent, Is.EqualTo(moves[0]));
        }

        [Test]
        public void ChooseNextIntent_RollPastFirstMovesWeight_SelectsSecondMove()
        {
            var moves = new[]
            {
                new EnemyMove("Attack", IntentType.Attack, value: 9, weight: 3),
                new EnemyMove("Guard", IntentType.Block, value: 8, weight: 1)
            };
            var enemy = new Enemy(maxHP: 42, moves, new FixedValueRandom(3));

            enemy.ChooseNextIntent();

            Assert.That(enemy.CurrentIntent, Is.EqualTo(moves[1]));
        }
    }
}
