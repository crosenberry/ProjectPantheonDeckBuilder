using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Combat;
using Pantheon.Core.Tests;

namespace Pantheon.Core.Tests.Enemies.Greek
{
    public class GreekEnemiesTests
    {
        [Test]
        public void HopliteSkirmisher_HasCorrectMaxHP()
        {
            var hoplite = Core.Enemies.Greek.GreekEnemies.HopliteSkirmisher(new FakeRandom());

            Assert.That(hoplite.MaxHP, Is.EqualTo(42));
        }

        [Test]
        public void HopliteSkirmisher_HasAttackAndGuardMoves()
        {
            var hoplite = Core.Enemies.Greek.GreekEnemies.HopliteSkirmisher(new FakeRandom());

            Assert.That(hoplite.Moves.Count, Is.EqualTo(2));
            Assert.That(hoplite.Moves.Any(m => m.Intent == IntentType.Attack && m.Value == 9), Is.True);
            Assert.That(hoplite.Moves.Any(m => m.Intent == IntentType.Block && m.Value == 8), Is.True);
        }

        [Test]
        public void HopliteSkirmisher_AttackIsMoreCommonThanGuard()
        {
            var hoplite = Core.Enemies.Greek.GreekEnemies.HopliteSkirmisher(new FakeRandom());

            var attackMove = hoplite.Moves.Single(m => m.Intent == IntentType.Attack);
            var guardMove = hoplite.Moves.Single(m => m.Intent == IntentType.Block);

            Assert.That(attackMove.Weight, Is.GreaterThan(guardMove.Weight));
        }

        [Test]
        public void HarpyScreecher_HasCorrectMaxHP()
        {
            var harpy = Core.Enemies.Greek.GreekEnemies.HarpyScreecher(new FakeRandom());

            Assert.That(harpy.MaxHP, Is.EqualTo(30));
        }

        [Test]
        public void HarpyScreecher_HasShriekAndClawMoves()
        {
            var harpy = Core.Enemies.Greek.GreekEnemies.HarpyScreecher(new FakeRandom());

            Assert.That(harpy.Moves.Count, Is.EqualTo(2));
            Assert.That(harpy.Moves.Any(m => m.Intent == IntentType.Debuff && m.Status == StatusType.Drained && m.Value == 2), Is.True);
            Assert.That(harpy.Moves.Any(m => m.Intent == IntentType.Attack && m.Value == 6), Is.True);
        }

        [Test]
        public void HarpyScreecher_ShriekAppliesDrainedWithoutDamage()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var harpy = Core.Enemies.Greek.GreekEnemies.HarpyScreecher(new FakeRandom());
            var shriek = harpy.Moves.Single(m => m.Intent == IntentType.Debuff);
            var forcedShriek = new Enemy(maxHP: 30, new[] { shriek }, new FakeRandom());

            CombatResolver.ExecuteEnemyIntent(forcedShriek, player);

            Assert.That(player.Drained, Is.EqualTo(2));
            Assert.That(player.CurrentHP, Is.EqualTo(70));
        }

        [Test]
        public void Viper_HasCorrectMaxHP()
        {
            var viper = Core.Enemies.Greek.GreekEnemies.Viper(new FakeRandom());

            Assert.That(viper.MaxHP, Is.EqualTo(12));
        }

        [Test]
        public void Viper_HasSingleBiteMove()
        {
            var viper = Core.Enemies.Greek.GreekEnemies.Viper(new FakeRandom());

            Assert.That(viper.Moves.Count, Is.EqualTo(1));
            Assert.That(viper.Moves[0].Intent, Is.EqualTo(IntentType.Attack));
            Assert.That(viper.Moves[0].Value, Is.EqualTo(4));
        }

        [Test]
        public void ViperBrood_ReturnsRequestedCount()
        {
            var brood = Core.Enemies.Greek.GreekEnemies.ViperBrood(3, new FakeRandom()).ToList();

            Assert.That(brood.Count, Is.EqualTo(3));
        }

        [Test]
        public void ViperBrood_ReturnsDistinctInstances()
        {
            var brood = Core.Enemies.Greek.GreekEnemies.ViperBrood(3, new FakeRandom()).ToList();

            Assert.That(brood.Distinct().Count(), Is.EqualTo(3));
        }
    }
}
