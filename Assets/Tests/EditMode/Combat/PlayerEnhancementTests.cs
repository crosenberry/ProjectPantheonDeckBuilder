using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat
{
    public class PlayerEnhancementTests
    {
        [Test]
        public void Grant_RegistersTriggersOnPlayer()
        {
            var player = new Player(startingEnergy: 3);
            var trigger = new TriggeredEffect(TriggerEvent.TurnStarted, new GainVolleyEffect(1));
            var enhancement = new PlayerEnhancement("Test Enhancement", new[] { trigger });

            enhancement.Grant(player);

            Assert.That(player.Triggers.Contains(trigger), Is.True);
        }

        [Test]
        public void Grant_MultipleTriggers_RegistersAll()
        {
            var player = new Player(startingEnergy: 3);
            var triggerA = new TriggeredEffect(TriggerEvent.TurnStarted, new GainVolleyEffect(1));
            var triggerB = new TriggeredEffect(TriggerEvent.CombatStarted, new GainVolleyEffect(1));
            var enhancement = new PlayerEnhancement("Test Enhancement", new[] { triggerA, triggerB });

            enhancement.Grant(player);

            Assert.That(player.Triggers.Contains(triggerA), Is.True);
            Assert.That(player.Triggers.Contains(triggerB), Is.True);
        }
    }
}
