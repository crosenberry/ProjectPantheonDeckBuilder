using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Combat;
using Pantheon.Core.Tests;

namespace Pantheon.Core.Tests.Enemies.Egyptian
{
    public class EgyptianEnemiesTests
    {
        [Test]
        public void UshabtiSentinel_HasCorrectMaxHP()
        {
            var ushabti = Core.Enemies.Egyptian.EgyptianEnemies.UshabtiSentinel(new FakeRandom());

            Assert.That(ushabti.MaxHP, Is.EqualTo(40));
        }

        [Test]
        public void UshabtiSentinel_HasAttackAndGuardMoves()
        {
            var ushabti = Core.Enemies.Egyptian.EgyptianEnemies.UshabtiSentinel(new FakeRandom());

            Assert.That(ushabti.Moves.Count, Is.EqualTo(2));
            Assert.That(ushabti.Moves.Any(m => m.Intent == IntentType.Attack && m.Value == 9), Is.True);
            Assert.That(ushabti.Moves.Any(m => m.Intent == IntentType.Block && m.Value == 8), Is.True);
        }

        [Test]
        public void UshabtiSentinel_AttackIsMoreCommonThanGuard()
        {
            var ushabti = Core.Enemies.Egyptian.EgyptianEnemies.UshabtiSentinel(new FakeRandom());

            var attackMove = ushabti.Moves.Single(m => m.Intent == IntentType.Attack);
            var guardMove = ushabti.Moves.Single(m => m.Intent == IntentType.Block);

            Assert.That(attackMove.Weight, Is.GreaterThan(guardMove.Weight));
        }

        [Test]
        public void SetsCultist_HasCorrectMaxHP()
        {
            var cultist = Core.Enemies.Egyptian.EgyptianEnemies.SetsCultist(new FakeRandom());

            Assert.That(cultist.MaxHP, Is.EqualTo(26));
        }

        [Test]
        public void SetsCultist_HasCurseAndAttackMoves()
        {
            var cultist = Core.Enemies.Egyptian.EgyptianEnemies.SetsCultist(new FakeRandom());

            Assert.That(cultist.Moves.Count, Is.EqualTo(2));
            Assert.That(cultist.Moves.Any(m => m.Intent == IntentType.Debuff && m.Status == StatusType.Exposed && m.Value == 2), Is.True);
            Assert.That(cultist.Moves.Any(m => m.Intent == IntentType.Attack && m.Value == 6), Is.True);
        }

        [Test]
        public void SetsCultist_CurseAppliesExposedWithoutDamage()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var cultist = Core.Enemies.Egyptian.EgyptianEnemies.SetsCultist(new FakeRandom());
            var curse = cultist.Moves.Single(m => m.Intent == IntentType.Debuff);
            var forcedCurse = new Enemy(maxHP: 26, new[] { curse }, new FakeRandom());

            CombatResolver.ExecuteEnemyIntent(forcedCurse, player);

            Assert.That(player.Exposed, Is.EqualTo(2));
            Assert.That(player.CurrentHP, Is.EqualTo(70));
        }

        [Test]
        public void ScarabSwarm_HasCorrectMaxHP()
        {
            var scarab = Core.Enemies.Egyptian.EgyptianEnemies.ScarabSwarm(new FakeRandom());

            Assert.That(scarab.MaxHP, Is.EqualTo(12));
        }

        [Test]
        public void ScarabSwarm_HasSingleBiteMove()
        {
            var scarab = Core.Enemies.Egyptian.EgyptianEnemies.ScarabSwarm(new FakeRandom());

            Assert.That(scarab.Moves.Count, Is.EqualTo(1));
            Assert.That(scarab.Moves[0].Intent, Is.EqualTo(IntentType.Attack));
            Assert.That(scarab.Moves[0].Value, Is.EqualTo(5));
        }

        [Test]
        public void ScarabSwarmPack_ReturnsRequestedCount()
        {
            var pack = Core.Enemies.Egyptian.EgyptianEnemies.ScarabSwarmPack(3, new FakeRandom()).ToList();

            Assert.That(pack.Count, Is.EqualTo(3));
        }

        [Test]
        public void ScarabSwarmPack_ReturnsDistinctInstances()
        {
            var pack = Core.Enemies.Egyptian.EgyptianEnemies.ScarabSwarmPack(3, new FakeRandom()).ToList();

            Assert.That(pack.Distinct().Count(), Is.EqualTo(3));
        }
    }
}
