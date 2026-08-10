using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Blessings.Artemis
{
    public class ArtemisEnhancementsTests
    {
        [Test]
        public void TwinMoons_WhenGranted_DrawsExtraCardAtStartOfTurn()
        {
            var player = new Player(startingEnergy: 3);
            var deck = new[]
            {
                Card.Attack("Card 1", energyCost: 1, damage: 1),
                Card.Attack("Card 2", energyCost: 1, damage: 1)
            };
            player.AddToDrawPile(deck);
            var enemy = new Enemy(maxHP: 42);
            var combat = new CombatEncounter(player, enemy);
            Core.Blessings.Artemis.ArtemisEnhancements.TwinMoons().Grant(player);

            combat.StartPlayerTurn(cardsToDraw: 1);

            Assert.That(player.Hand.Count, Is.EqualTo(2));
        }

        [Test]
        public void ApexPredatorsMantle_WhenGranted_ExposesLowHpEnemyAtStartOfTurn()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 100);
            enemy.TakeDamage(80);
            var combat = new CombatEncounter(player, enemy);
            Core.Blessings.Artemis.ArtemisEnhancements.ApexPredatorsMantle().Grant(player);

            combat.StartPlayerTurn(cardsToDraw: 0);

            Assert.That(enemy.Exposed, Is.EqualTo(2));
        }

        [Test]
        public void ApexPredatorsMantle_WhenGranted_DoesNotExposeHealthyEnemy()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 100);
            var combat = new CombatEncounter(player, enemy);
            Core.Blessings.Artemis.ArtemisEnhancements.ApexPredatorsMantle().Grant(player);

            combat.StartPlayerTurn(cardsToDraw: 0);

            Assert.That(enemy.Exposed, Is.EqualTo(0));
        }
    }
}
