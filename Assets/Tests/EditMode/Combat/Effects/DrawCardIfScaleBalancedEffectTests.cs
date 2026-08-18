using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class DrawCardIfScaleBalancedEffectTests
    {
        private static Player MakePlayerWithCards(int count)
        {
            var player = new Player(startingEnergy: 3);
            for (var i = 0; i < count; i++)
            {
                player.AddToDrawPile(new[] { Card.Attack($"Card {i}", energyCost: 1, damage: 1) });
            }

            return player;
        }

        [Test]
        public void Apply_ScaleBalanced_DrawsBaseAndBonus()
        {
            var player = MakePlayerWithCards(5);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DrawCardIfScaleBalancedEffect(baseAmount: 1, bonusAmount: 1);

            effect.Apply(context);

            Assert.That(player.Hand.Count, Is.EqualTo(2));
        }

        [Test]
        public void Apply_ScaleNotBalanced_DrawsOnlyBase()
        {
            var player = MakePlayerWithCards(5);
            player.AdjustScale(3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DrawCardIfScaleBalancedEffect(baseAmount: 1, bonusAmount: 1);

            effect.Apply(context);

            Assert.That(player.Hand.Count, Is.EqualTo(1));
        }

        [Test]
        public void Apply_ScaleAtBalancedBoundary_DrawsBaseAndBonus()
        {
            var player = MakePlayerWithCards(5);
            player.AdjustScale(-1);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new DrawCardIfScaleBalancedEffect(baseAmount: 1, bonusAmount: 1);

            effect.Apply(context);

            Assert.That(player.Hand.Count, Is.EqualTo(2));
        }
    }
}
