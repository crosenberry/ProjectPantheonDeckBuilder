using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Combat;
using Pantheon.Core.Tests;

namespace Pantheon.Core.Tests.Enemies.Chinese
{
    public class ChineseEnemiesTests
    {
        [Test]
        public void HeavenlySoldier_HasCorrectMaxHP()
        {
            var soldier = Core.Enemies.Chinese.ChineseEnemies.HeavenlySoldier(new FakeRandom());

            Assert.That(soldier.MaxHP, Is.EqualTo(42));
        }

        [Test]
        public void HeavenlySoldier_HasAttackAndGuardMoves()
        {
            var soldier = Core.Enemies.Chinese.ChineseEnemies.HeavenlySoldier(new FakeRandom());

            Assert.That(soldier.Moves.Count, Is.EqualTo(2));
            Assert.That(soldier.Moves.Any(m => m.Intent == IntentType.Attack && m.Value == 9), Is.True);
            Assert.That(soldier.Moves.Any(m => m.Intent == IntentType.Block && m.Value == 8), Is.True);
        }

        [Test]
        public void HeavenlySoldier_AttackIsMoreCommonThanGuard()
        {
            var soldier = Core.Enemies.Chinese.ChineseEnemies.HeavenlySoldier(new FakeRandom());

            var attackMove = soldier.Moves.Single(m => m.Intent == IntentType.Attack);
            var guardMove = soldier.Moves.Single(m => m.Intent == IntentType.Block);

            Assert.That(attackMove.Weight, Is.GreaterThan(guardMove.Weight));
        }

        [Test]
        public void NineTailedFoxSpirit_HasCorrectMaxHP()
        {
            var fox = Core.Enemies.Chinese.ChineseEnemies.NineTailedFoxSpirit(new FakeRandom());

            Assert.That(fox.MaxHP, Is.EqualTo(27));
        }

        [Test]
        public void NineTailedFoxSpirit_HasGatherPowerAndAttackMoves()
        {
            var fox = Core.Enemies.Chinese.ChineseEnemies.NineTailedFoxSpirit(new FakeRandom());

            Assert.That(fox.Moves.Count, Is.EqualTo(2));
            Assert.That(fox.Moves.Any(m => m.Intent == IntentType.Buff && m.Status == StatusType.Strength && m.Value == 2), Is.True);
            Assert.That(fox.Moves.Any(m => m.Intent == IntentType.Attack && m.Value == 6), Is.True);
        }

        [Test]
        public void NineTailedFoxSpirit_GatherPowerAppliesStrengthToSelfWithoutDamage()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var fox = Core.Enemies.Chinese.ChineseEnemies.NineTailedFoxSpirit(new FakeRandom());
            var gatherPower = fox.Moves.Single(m => m.Intent == IntentType.Buff);
            var forcedGatherPower = new Enemy(maxHP: 27, new[] { gatherPower }, new FakeRandom());

            CombatResolver.ExecuteEnemyIntent(forcedGatherPower, player);

            Assert.That(forcedGatherPower.Strength, Is.EqualTo(2));
            Assert.That(player.CurrentHP, Is.EqualTo(70));
        }

        [Test]
        public void YakshaSwarm_HasCorrectMaxHP()
        {
            var yaksha = Core.Enemies.Chinese.ChineseEnemies.YakshaSwarm(new FakeRandom());

            Assert.That(yaksha.MaxHP, Is.EqualTo(12));
        }

        [Test]
        public void YakshaSwarm_HasSingleStrikeMove()
        {
            var yaksha = Core.Enemies.Chinese.ChineseEnemies.YakshaSwarm(new FakeRandom());

            Assert.That(yaksha.Moves.Count, Is.EqualTo(1));
            Assert.That(yaksha.Moves[0].Intent, Is.EqualTo(IntentType.Attack));
            Assert.That(yaksha.Moves[0].Value, Is.EqualTo(5));
        }

        [Test]
        public void YakshaSwarmPack_ReturnsRequestedCount()
        {
            var pack = Core.Enemies.Chinese.ChineseEnemies.YakshaSwarmPack(3, new FakeRandom()).ToList();

            Assert.That(pack.Count, Is.EqualTo(3));
        }

        [Test]
        public void YakshaSwarmPack_ReturnsDistinctInstances()
        {
            var pack = Core.Enemies.Chinese.ChineseEnemies.YakshaSwarmPack(3, new FakeRandom()).ToList();

            Assert.That(pack.Distinct().Count(), Is.EqualTo(3));
        }
    }
}
