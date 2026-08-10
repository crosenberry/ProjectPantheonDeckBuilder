using NUnit.Framework;
using Pantheon.Core.Meta;

namespace Pantheon.Core.Tests.Meta
{
    public class RelicTests
    {
        [Test]
        public void Constructor_SetsNameAndFlavorText()
        {
            var relic = new Relic("Ares' First Blood", "The god of war lends his edge to your first strike.");

            Assert.That(relic.Name, Is.EqualTo("Ares' First Blood"));
            Assert.That(relic.FlavorText, Is.EqualTo("The god of war lends his edge to your first strike."));
        }
    }
}
