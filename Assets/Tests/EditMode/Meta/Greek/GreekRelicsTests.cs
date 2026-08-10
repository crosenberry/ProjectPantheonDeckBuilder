using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Meta.Greek;

namespace Pantheon.Core.Tests.Meta.Greek
{
    public class GreekRelicsTests
    {
        [Test]
        public void RegularPool_ReturnsThirteenRelics()
        {
            var pool = GreekRelics.RegularPool().ToList();

            Assert.That(pool.Count, Is.EqualTo(13));
        }

        [Test]
        public void RegularPool_ContainsAllExpectedNames()
        {
            var names = GreekRelics.RegularPool().Select(r => r.Name).ToList();

            Assert.That(names, Is.EquivalentTo(new[]
            {
                "Ares' First Blood",
                "Hermes' Winged Sandals",
                "Nyx's Embrace",
                "Medusa's Glare",
                "Styx's Blessing",
                "Erinyes' Vengeance",
                "Aphrodite's Bathwater",
                "Sisyphus's Gym Membership",
                "Midas's Credit Card",
                "Zeus's Group Project",
                "Pandora's Group Chat",
                "Hades' Bulk Discount",
                "Narcissus's Front-Facing Camera"
            }));
        }

        [Test]
        public void AresFirstBlood_HasExpectedFlavorText()
        {
            var relic = GreekRelics.AresFirstBlood();

            Assert.That(relic.FlavorText, Is.EqualTo("Your first Attack card each turn deals 3 additional damage."));
        }

        [Test]
        public void NyxsEmbrace_HasExpectedFlavorText()
        {
            var relic = GreekRelics.NyxsEmbrace();

            Assert.That(relic.FlavorText, Is.EqualTo("At the start of each combat, gain 1 Strength. If you have Volley, gain 1 Volley instead."));
        }

        [Test]
        public void TalossCore_HasExpectedNameAndFlavorText()
        {
            var relic = GreekRelics.TalossCore();

            Assert.That(relic.Name, Is.EqualTo("Talos's Core"));
            Assert.That(relic.FlavorText, Is.EqualTo("Your Attack cards deal 2 additional damage to enemies below 50% HP."));
        }

        [Test]
        public void TalossCore_IsNotIncludedInRegularPool()
        {
            var names = GreekRelics.RegularPool().Select(r => r.Name).ToList();

            Assert.That(names, Does.Not.Contain("Talos's Core"));
        }
    }
}
