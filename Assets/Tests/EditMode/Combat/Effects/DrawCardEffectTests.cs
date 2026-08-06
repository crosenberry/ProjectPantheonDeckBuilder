using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class DrawCardEffectTests
    {
        [Test]
        public void Apply_DrawsRequestedCardsIntoHand()
        {
            var player = new Player(startingEnergy: 3);
            player.AddToDrawPile(new[]
            {
                Card.Attack("Card 1", energyCost: 1, damage: 1),
                Card.Attack("Card 2", energyCost: 1, damage: 1)
            });
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DrawCardEffect(2);

            effect.Apply(context);

            Assert.That(player.Hand.Count, Is.EqualTo(2));
            Assert.That(player.DrawPile.Count, Is.EqualTo(0));
        }
    }
}
