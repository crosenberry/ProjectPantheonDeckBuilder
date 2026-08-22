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

        [Test]
        public void Storm_DefaultsToZero()
        {
            var enemy = new Enemy(maxHP: 42);

            Assert.That(enemy.Storm, Is.EqualTo(0));
        }

        [Test]
        public void GainStorm_IncreasesStorm()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.GainStorm(3);

            Assert.That(enemy.Storm, Is.EqualTo(3));
        }

        [Test]
        public void ConsumeStorm_ResetsStormToZero()
        {
            var enemy = new Enemy(maxHP: 42);
            enemy.GainStorm(6);

            enemy.ConsumeStorm();

            Assert.That(enemy.Storm, Is.EqualTo(0));
        }

        [Test]
        public void ConsumeStorm_ReturnsAmountConsumed()
        {
            var enemy = new Enemy(maxHP: 42);
            enemy.GainStorm(6);

            var consumed = enemy.ConsumeStorm();

            Assert.That(consumed, Is.EqualTo(6));
        }

        [Test]
        public void Scale_DefaultsToZero()
        {
            var enemy = new Enemy(maxHP: 42);

            Assert.That(enemy.Scale, Is.EqualTo(0));
        }

        [Test]
        public void AdjustScale_PositiveAmount_IncreasesScale()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.AdjustScale(2);

            Assert.That(enemy.Scale, Is.EqualTo(2));
        }

        [Test]
        public void AdjustScale_NegativeAmount_DecreasesScale()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.AdjustScale(-2);

            Assert.That(enemy.Scale, Is.EqualTo(-2));
        }

        [Test]
        public void AdjustScale_ExceedsUpperBound_ClampsAtFive()
        {
            var enemy = new Enemy(maxHP: 42);
            enemy.AdjustScale(4);

            enemy.AdjustScale(4);

            Assert.That(enemy.Scale, Is.EqualTo(5));
        }

        [Test]
        public void AdjustScale_ExceedsLowerBound_ClampsAtNegativeFive()
        {
            var enemy = new Enemy(maxHP: 42);
            enemy.AdjustScale(-4);

            enemy.AdjustScale(-4);

            Assert.That(enemy.Scale, Is.EqualTo(-5));
        }

        [Test]
        public void Form_DefaultsToMortal()
        {
            var enemy = new Enemy(maxHP: 42);

            Assert.That(enemy.Form, Is.EqualTo(Form.Mortal));
        }

        [Test]
        public void ChangeForm_SetsFormToTarget()
        {
            var enemy = new Enemy(maxHP: 42);

            enemy.ChangeForm(Form.Immortal);

            Assert.That(enemy.Form, Is.EqualTo(Form.Immortal));
        }

        [Test]
        public void ChooseNextIntent_MoveBelowMinStorm_IsExcludedFromPool()
        {
            var moves = new[]
            {
                new EnemyMove("Charge", IntentType.Buff, value: 0, weight: 1, stormDelta: 2, maxStorm: 5),
                new EnemyMove("Discharge", IntentType.Attack, value: 20, weight: 1, minStorm: 6)
            };
            var enemy = new Enemy(maxHP: 42, moves, new FixedValueRandom(0));

            enemy.ChooseNextIntent();

            Assert.That(enemy.CurrentIntent, Is.EqualTo(moves[0]));
        }

        [Test]
        public void ChooseNextIntent_MoveAtOrAboveMinStorm_BecomesEligibleAndExcludesMaxStormMove()
        {
            var moves = new[]
            {
                new EnemyMove("Charge", IntentType.Buff, value: 0, weight: 1, stormDelta: 2, maxStorm: 5),
                new EnemyMove("Discharge", IntentType.Attack, value: 20, weight: 1, minStorm: 6)
            };
            var enemy = new Enemy(maxHP: 42, moves, new FixedValueRandom(0));
            enemy.GainStorm(6);

            enemy.ChooseNextIntent();

            Assert.That(enemy.CurrentIntent, Is.EqualTo(moves[1]));
        }

        [Test]
        public void ChooseNextIntent_MoveAboveMaxScale_IsExcludedFromPool()
        {
            var moves = new[]
            {
                new EnemyMove("Sway", IntentType.Attack, value: 6, weight: 1, scaleDelta: -2),
                new EnemyMove("Surge", IntentType.Attack, value: 14, weight: 1, maxScale: -4)
            };
            var enemy = new Enemy(maxHP: 42, moves, new FixedValueRandom(0));

            enemy.ChooseNextIntent();

            Assert.That(enemy.CurrentIntent, Is.EqualTo(moves[0]));
        }

        [Test]
        public void ChooseNextIntent_MoveAtOrBelowMaxScale_IsEligible()
        {
            var moves = new[]
            {
                new EnemyMove("Sway", IntentType.Attack, value: 6, weight: 1, scaleDelta: -2, minScale: -3),
                new EnemyMove("Surge", IntentType.Attack, value: 14, weight: 1, maxScale: -4)
            };
            var enemy = new Enemy(maxHP: 42, moves, new FixedValueRandom(0));
            enemy.AdjustScale(-4);

            enemy.ChooseNextIntent();

            Assert.That(enemy.CurrentIntent, Is.EqualTo(moves[1]));
        }

        [Test]
        public void ChooseNextIntent_MoveRequiresDifferentForm_IsExcludedFromPool()
        {
            var moves = new[]
            {
                new EnemyMove("Guard", IntentType.Block, value: 8, weight: 1, requiredForm: Form.Mortal),
                new EnemyMove("Claw", IntentType.Attack, value: 12, weight: 1, requiredForm: Form.Beast)
            };
            var enemy = new Enemy(maxHP: 42, moves, new FixedValueRandom(0));

            enemy.ChooseNextIntent();

            Assert.That(enemy.CurrentIntent, Is.EqualTo(moves[0]));
        }

        [Test]
        public void ChooseNextIntent_MoveRequiresCurrentForm_IsEligible()
        {
            var moves = new[]
            {
                new EnemyMove("Guard", IntentType.Block, value: 8, weight: 1, requiredForm: Form.Mortal),
                new EnemyMove("Claw", IntentType.Attack, value: 12, weight: 1, requiredForm: Form.Beast)
            };
            var enemy = new Enemy(maxHP: 42, moves, new FixedValueRandom(0));
            enemy.ChangeForm(Form.Beast);

            enemy.ChooseNextIntent();

            Assert.That(enemy.CurrentIntent, Is.EqualTo(moves[1]));
        }
    }
}
