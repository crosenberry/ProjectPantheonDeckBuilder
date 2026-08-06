using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class DiscardCardWithoutTagEffectTests
    {
        [Test]
        public void Apply_HandHasCardWithoutTag_DiscardsIt()
        {
            var player = new Player(startingEnergy: 3);
            var shotCard = Card.Attack("Quick Shot", energyCost: 1, damage: 6, tags: new[] { CardTag.Shot });
            var plainCard = Card.Skill("Side Step", energyCost: 1, block: 5);
            player.AddToDrawPile(new[] { shotCard, plainCard });
            player.DrawCards(2);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DiscardCardWithoutTagEffect(CardTag.Shot);

            effect.Apply(context);

            Assert.That(player.Hand.Contains(plainCard), Is.False);
            Assert.That(player.DiscardPile.Contains(plainCard), Is.True);
            Assert.That(player.Hand.Contains(shotCard), Is.True);
        }

        [Test]
        public void Apply_NoCardWithoutTag_HandUnchanged()
        {
            var player = new Player(startingEnergy: 3);
            var shotCard = Card.Attack("Quick Shot", energyCost: 1, damage: 6, tags: new[] { CardTag.Shot });
            player.AddToDrawPile(new[] { shotCard });
            player.DrawCards(1);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DiscardCardWithoutTagEffect(CardTag.Shot);

            effect.Apply(context);

            Assert.That(player.Hand.Count, Is.EqualTo(1));
            Assert.That(player.DiscardPile.Count, Is.EqualTo(0));
        }
    }
}
