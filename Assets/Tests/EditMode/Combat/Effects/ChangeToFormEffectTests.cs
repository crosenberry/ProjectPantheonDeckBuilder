using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ChangeToFormEffectTests
    {
        [Test]
        public void Apply_SetsPlayerFormToTarget()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ChangeToFormEffect(Form.Immortal);

            effect.Apply(context);

            Assert.That(player.Form, Is.EqualTo(Form.Immortal));
        }
    }
}
