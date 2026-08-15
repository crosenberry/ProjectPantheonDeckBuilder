using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Run;
using Pantheon.Core.Run.Greek;

namespace Pantheon.Core.Tests.Run.Greek
{
    public class GreekStagesTests
    {
        [Test]
        public void SampleStage_HasThreeNodesEndingInBoss()
        {
            var stage = GreekStages.SampleStage();

            Assert.That(stage.Nodes.Count, Is.EqualTo(3));
            Assert.That(stage.Nodes[0].Type, Is.EqualTo(NodeType.Combat));
            Assert.That(stage.Nodes[1].Type, Is.EqualTo(NodeType.Combat));
            Assert.That(stage.Nodes[2].Type, Is.EqualTo(NodeType.Boss));
        }

        [Test]
        public void SampleStage_EntryIsFirstNode()
        {
            var stage = GreekStages.SampleStage();

            Assert.That(stage.AvailableNodeIndices, Is.EquivalentTo(new[] { 0 }));
        }

        [Test]
        public void SampleStage_IsTraversableToCompletion()
        {
            var stage = GreekStages.SampleStage();

            stage.MoveTo(0);
            stage.MoveTo(1);
            stage.MoveTo(2);

            Assert.That(stage.IsComplete, Is.True);
        }
    }
}
