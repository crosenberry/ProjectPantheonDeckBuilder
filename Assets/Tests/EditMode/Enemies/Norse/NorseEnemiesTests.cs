using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Combat;
using Pantheon.Core.Tests;

namespace Pantheon.Core.Tests.Enemies.Norse
{
    public class NorseEnemiesTests
    {
        [Test]
        public void DraugrReaver_HasCorrectMaxHP()
        {
            var draugr = Core.Enemies.Norse.NorseEnemies.DraugrReaver(new FakeRandom());

            Assert.That(draugr.MaxHP, Is.EqualTo(44));
        }

        [Test]
        public void DraugrReaver_HasAttackAndGuardMoves()
        {
            var draugr = Core.Enemies.Norse.NorseEnemies.DraugrReaver(new FakeRandom());

            Assert.That(draugr.Moves.Count, Is.EqualTo(2));
            Assert.That(draugr.Moves.Any(m => m.Intent == IntentType.Attack && m.Value == 10), Is.True);
            Assert.That(draugr.Moves.Any(m => m.Intent == IntentType.Block && m.Value == 7), Is.True);
        }

        [Test]
        public void DraugrReaver_AttackIsMoreCommonThanGuard()
        {
            var draugr = Core.Enemies.Norse.NorseEnemies.DraugrReaver(new FakeRandom());

            var attackMove = draugr.Moves.Single(m => m.Intent == IntentType.Attack);
            var guardMove = draugr.Moves.Single(m => m.Intent == IntentType.Block);

            Assert.That(attackMove.Weight, Is.GreaterThan(guardMove.Weight));
        }

        [Test]
        public void SeidrHexer_HasCorrectMaxHP()
        {
            var hexer = Core.Enemies.Norse.NorseEnemies.SeidrHexer(new FakeRandom());

            Assert.That(hexer.MaxHP, Is.EqualTo(28));
        }

        [Test]
        public void SeidrHexer_HasHexAndClawStrikeMoves()
        {
            var hexer = Core.Enemies.Norse.NorseEnemies.SeidrHexer(new FakeRandom());

            Assert.That(hexer.Moves.Count, Is.EqualTo(2));
            Assert.That(hexer.Moves.Any(m => m.Intent == IntentType.Debuff && m.Status == StatusType.Sundered && m.Value == 2), Is.True);
            Assert.That(hexer.Moves.Any(m => m.Intent == IntentType.Attack && m.Value == 6), Is.True);
        }

        [Test]
        public void SeidrHexer_HexAppliesSunderedWithoutDamage()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var hexer = Core.Enemies.Norse.NorseEnemies.SeidrHexer(new FakeRandom());
            var hex = hexer.Moves.Single(m => m.Intent == IntentType.Debuff);
            var forcedHex = new Enemy(maxHP: 28, new[] { hex }, new FakeRandom());

            CombatResolver.ExecuteEnemyIntent(forcedHex, player);

            Assert.That(player.Sundered, Is.EqualTo(2));
            Assert.That(player.CurrentHP, Is.EqualTo(70));
        }

        [Test]
        public void Wolf_HasCorrectMaxHP()
        {
            var wolf = Core.Enemies.Norse.NorseEnemies.Wolf(new FakeRandom());

            Assert.That(wolf.MaxHP, Is.EqualTo(12));
        }

        [Test]
        public void Wolf_HasSingleBiteMove()
        {
            var wolf = Core.Enemies.Norse.NorseEnemies.Wolf(new FakeRandom());

            Assert.That(wolf.Moves.Count, Is.EqualTo(1));
            Assert.That(wolf.Moves[0].Intent, Is.EqualTo(IntentType.Attack));
            Assert.That(wolf.Moves[0].Value, Is.EqualTo(5));
        }

        [Test]
        public void WolfPack_ReturnsRequestedCount()
        {
            var pack = Core.Enemies.Norse.NorseEnemies.WolfPack(3, new FakeRandom()).ToList();

            Assert.That(pack.Count, Is.EqualTo(3));
        }

        [Test]
        public void WolfPack_ReturnsDistinctInstances()
        {
            var pack = Core.Enemies.Norse.NorseEnemies.WolfPack(3, new FakeRandom()).ToList();

            Assert.That(pack.Distinct().Count(), Is.EqualTo(3));
        }
    }
}
