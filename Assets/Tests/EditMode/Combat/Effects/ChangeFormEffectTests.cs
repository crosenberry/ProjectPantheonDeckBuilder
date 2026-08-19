using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat.Effects
{
    public class ChangeFormEffectTests
    {
        [Test]
        public void Apply_CyclesPlayerFormToNext()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var context = new CardEffectContext(player, enemy);
            var effect = new ChangeFormEffect();

            effect.Apply(context);

            Assert.That(player.Form, Is.EqualTo(Form.Beast));
        }
    }
}
