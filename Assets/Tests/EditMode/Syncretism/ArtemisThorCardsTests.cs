using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Syncretism
{
    public class ArtemisThorCardsTests
    {
        private static void Play(Player player, Card card, Enemy enemy)
        {
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            CombatResolver.PlayCard(player, card, enemy);
        }

        [Test]
        public void TwinStorms_NoPriorStorm_DealsBaseDamageAndGrantsStormAndIsTaggedShot()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Syncretism.ArtemisThorCards.TwinStorms();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(35));
            Assert.That(player.Storm, Is.EqualTo(1));
            Assert.That(card.Tags.Contains(CardTag.Shot), Is.True);
        }

        [Test]
        public void TwinStorms_HadPriorStorm_DealsBonusDamage()
        {
            var player = new Player(startingEnergy: 2);
            player.GainStorm(2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Syncretism.ArtemisThorCards.TwinStorms();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(31));
        }

        [Test]
        public void HuntersSquall_GrantsBlockStormAndVolley()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Syncretism.ArtemisThorCards.HuntersSquall();

            Play(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(6));
            Assert.That(player.Storm, Is.EqualTo(1));
            Assert.That(player.Volley, Is.EqualTo(1));
        }

        [Test]
        public void ThunderousVolley_DealsCombinedScaledDamageToAllEnemies()
        {
            var player = new Player(startingEnergy: 2);
            var enemyA = new Enemy(maxHP: 42);
            var enemyB = new Enemy(maxHP: 42);
            var card = Core.Syncretism.ArtemisThorCards.ThunderousVolley();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            player.GainStorm(2);
            player.GainVolley(3);

            CombatResolver.PlayCard(player, card, enemyA, new[] { enemyA, enemyB });

            Assert.That(enemyA.CurrentHP, Is.EqualTo(30));
            Assert.That(enemyB.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void RagnaroksQuarry_VolleyBelowThreshold_HitsOnce()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Syncretism.ArtemisThorCards.RagnaroksQuarry();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            player.GainStorm(2);
            player.GainVolley(2);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void RagnaroksQuarry_VolleyAtThreshold_HitsTwice()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Syncretism.ArtemisThorCards.RagnaroksQuarry();
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            player.GainStorm(2);
            player.GainVolley(3);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(18));
        }
    }
}
