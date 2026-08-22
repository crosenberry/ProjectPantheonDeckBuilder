using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Combat;
using Pantheon.Core.Tests;

namespace Pantheon.Core.Tests.Enemies.Hook2
{
    public class Hook2EnemiesTests
    {
        [Test]
        public void WrathfulErinys_HasCorrectMaxHP()
        {
            var erinys = Core.Enemies.Hook2.Hook2Enemies.WrathfulErinys(new FakeRandom());

            Assert.That(erinys.MaxHP, Is.EqualTo(34));
        }

        [Test]
        public void WrathfulErinys_HasSeetheAndVengeanceStrikeMoves()
        {
            var erinys = Core.Enemies.Hook2.Hook2Enemies.WrathfulErinys(new FakeRandom());

            Assert.That(erinys.Moves.Count, Is.EqualTo(2));
            Assert.That(erinys.Moves.Any(m => m.Name == "Seethe" && m.StormDelta == 3), Is.True);
            Assert.That(erinys.Moves.Any(m => m.Name == "Vengeance Strike" && m.Value == 20 && m.ConsumesStorm), Is.True);
        }

        [Test]
        public void WrathfulErinys_BuildsThenDischargesAfterTwoSeethes()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var erinys = Core.Enemies.Hook2.Hook2Enemies.WrathfulErinys(new FixedValueRandom(0));

            CombatResolver.ExecuteEnemyIntent(erinys, player);
            Assert.That(erinys.CurrentIntent.Name, Is.EqualTo("Seethe"));
            Assert.That(player.CurrentHP, Is.EqualTo(70));

            CombatResolver.ExecuteEnemyIntent(erinys, player);
            Assert.That(erinys.CurrentIntent.Name, Is.EqualTo("Vengeance Strike"));

            CombatResolver.ExecuteEnemyIntent(erinys, player);
            Assert.That(player.CurrentHP, Is.EqualTo(50));
            Assert.That(erinys.Storm, Is.EqualTo(0));
        }

        [Test]
        public void ThunderhideJotunn_HasCorrectMaxHP()
        {
            var jotunn = Core.Enemies.Hook2.Hook2Enemies.ThunderhideJotunn(new FakeRandom());

            Assert.That(jotunn.MaxHP, Is.EqualTo(38));
        }

        [Test]
        public void ThunderhideJotunn_HasGatherSquallAndStormSlamMoves()
        {
            var jotunn = Core.Enemies.Hook2.Hook2Enemies.ThunderhideJotunn(new FakeRandom());

            Assert.That(jotunn.Moves.Count, Is.EqualTo(2));
            Assert.That(jotunn.Moves.Any(m => m.Name == "Gather Squall" && m.StormDelta == 2), Is.True);
            Assert.That(jotunn.Moves.Any(m => m.Name == "Storm Slam" && m.Value == 16 && m.ConsumesStorm), Is.True);
        }

        [Test]
        public void ThunderhideJotunn_BuildsThenDischargesAfterThreeGatherSqualls()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var jotunn = Core.Enemies.Hook2.Hook2Enemies.ThunderhideJotunn(new FixedValueRandom(0));

            CombatResolver.ExecuteEnemyIntent(jotunn, player);
            CombatResolver.ExecuteEnemyIntent(jotunn, player);
            Assert.That(jotunn.CurrentIntent.Name, Is.EqualTo("Gather Squall"));
            Assert.That(jotunn.Storm, Is.EqualTo(4));

            CombatResolver.ExecuteEnemyIntent(jotunn, player);
            Assert.That(jotunn.CurrentIntent.Name, Is.EqualTo("Storm Slam"));

            CombatResolver.ExecuteEnemyIntent(jotunn, player);
            Assert.That(player.CurrentHP, Is.EqualTo(54));
            Assert.That(jotunn.Storm, Is.EqualTo(0));
        }

        [Test]
        public void AmmitsShade_HasCorrectMaxHP()
        {
            var shade = Core.Enemies.Hook2.Hook2Enemies.AmmitsShade(new FakeRandom());

            Assert.That(shade.MaxHP, Is.EqualTo(36));
        }

        [Test]
        public void AmmitsShade_HasSwayAndChaosSurgeMoves()
        {
            var shade = Core.Enemies.Hook2.Hook2Enemies.AmmitsShade(new FakeRandom());

            Assert.That(shade.Moves.Count, Is.EqualTo(3));
            Assert.That(shade.Moves.Any(m => m.Name == "Sway Toward Chaos" && m.ScaleDelta == -2), Is.True);
            Assert.That(shade.Moves.Any(m => m.Name == "Sway Toward Order" && m.ScaleDelta == 2), Is.True);
            Assert.That(shade.Moves.Any(m => m.Name == "Chaos Surge" && m.Value == 14), Is.True);
        }

        [Test]
        public void AmmitsShade_ChaosSurgeIneligibleAtNeutralScale()
        {
            var shade = Core.Enemies.Hook2.Hook2Enemies.AmmitsShade(new FakeRandom());

            Assert.That(shade.CurrentIntent.Name, Is.Not.EqualTo("Chaos Surge"));
        }

        [Test]
        public void AmmitsShade_ChaosSurgeBecomesEligibleAfterLeaningChaosTwice()
        {
            // Roll fixed at 4: at construction (Scale 0, pool weight 2+2=4) this
            // lands out of range so CurrentIntent stays unset - harmless, not
            // asserted on. After Scale drops to -4, the pool becomes
            // 2+2+3=7 and roll 4 falls within Chaos Surge's weight (cumulative
            // 4-7), deterministically selecting it.
            var shade = Core.Enemies.Hook2.Hook2Enemies.AmmitsShade(new FixedValueRandom(4));
            shade.AdjustScale(-4);

            shade.ChooseNextIntent();

            Assert.That(shade.CurrentIntent.Name, Is.EqualTo("Chaos Surge"));
        }

        [Test]
        public void StoneGuardian_HasCorrectMaxHP()
        {
            var guardian = Core.Enemies.Hook2.Hook2Enemies.StoneGuardian(new FakeRandom());

            Assert.That(guardian.MaxHP, Is.EqualTo(40));
        }

        [Test]
        public void StoneGuardian_HasGuardClawAndWardMoves()
        {
            var guardian = Core.Enemies.Hook2.Hook2Enemies.StoneGuardian(new FakeRandom());

            Assert.That(guardian.Moves.Count, Is.EqualTo(3));
            Assert.That(guardian.Moves.Any(m => m.Name == "Guard" && m.RequiredForm == Form.Mortal), Is.True);
            Assert.That(guardian.Moves.Any(m => m.Name == "Savage Claw" && m.RequiredForm == Form.Beast), Is.True);
            Assert.That(guardian.Moves.Any(m => m.Name == "Radiant Ward" && m.RequiredForm == Form.Immortal), Is.True);
        }

        [Test]
        public void StoneGuardian_StartsInMortalWithGuardIntent()
        {
            var guardian = Core.Enemies.Hook2.Hook2Enemies.StoneGuardian(new FakeRandom());

            Assert.That(guardian.Form, Is.EqualTo(Form.Mortal));
            Assert.That(guardian.CurrentIntent.Name, Is.EqualTo("Guard"));
        }

        [Test]
        public void StoneGuardian_CyclesMortalBeastImmortalMortalAcrossThreeIntents()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var guardian = Core.Enemies.Hook2.Hook2Enemies.StoneGuardian(new FixedValueRandom(0));

            CombatResolver.ExecuteEnemyIntent(guardian, player);
            Assert.That(guardian.Form, Is.EqualTo(Form.Beast));
            Assert.That(guardian.CurrentIntent.Name, Is.EqualTo("Savage Claw"));

            CombatResolver.ExecuteEnemyIntent(guardian, player);
            Assert.That(guardian.Form, Is.EqualTo(Form.Immortal));
            Assert.That(guardian.CurrentIntent.Name, Is.EqualTo("Radiant Ward"));

            CombatResolver.ExecuteEnemyIntent(guardian, player);
            Assert.That(guardian.Form, Is.EqualTo(Form.Mortal));
            Assert.That(guardian.CurrentIntent.Name, Is.EqualTo("Guard"));
        }
    }
}
