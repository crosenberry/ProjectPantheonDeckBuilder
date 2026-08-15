using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Run;

namespace Pantheon.Core.Tests.Run
{
    public class RiftTests
    {
        private static readonly Mythology[] AllMythologies =
        {
            Mythology.Greek, Mythology.Norse, Mythology.Egyptian, Mythology.Chinese
        };

        [Test]
        public void RevealMythologyOptions_RevealCountTwo_ReturnsTwoOptions()
        {
            var revealed = Rift.RevealMythologyOptions(AllMythologies, new FakeRandom(), revealCount: 2);

            Assert.That(revealed.Count, Is.EqualTo(2));
        }

        [Test]
        public void RevealMythologyOptions_ReturnsDistinctMythologies()
        {
            var revealed = Rift.RevealMythologyOptions(AllMythologies, new FakeRandom(), revealCount: 2);

            Assert.That(revealed.Distinct().Count(), Is.EqualTo(revealed.Count));
        }

        [Test]
        public void RevealMythologyOptions_AllRevealedComeFromProvidedList()
        {
            var revealed = Rift.RevealMythologyOptions(AllMythologies, new FakeRandom(), revealCount: 2);

            Assert.That(revealed.All(m => AllMythologies.Contains(m)), Is.True);
        }

        [Test]
        public void RevealMythologyOptions_RevealCountThree_ReturnsThreeOptions()
        {
            var revealed = Rift.RevealMythologyOptions(AllMythologies, new FakeRandom(), revealCount: 3);

            Assert.That(revealed.Count, Is.EqualTo(3));
        }
    }
}
