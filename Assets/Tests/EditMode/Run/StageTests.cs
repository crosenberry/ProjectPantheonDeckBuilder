using NUnit.Framework;
using Pantheon.Core.Run;

namespace Pantheon.Core.Tests.Run
{
    public class StageTests
    {
        private static Stage BuildSimpleStage()
        {
            var nodes = new[]
            {
                new MapNode(NodeType.Combat, new[] { 1 }),
                new MapNode(NodeType.Chest, new[] { 2 }),
                new MapNode(NodeType.Boss, new int[0])
            };

            return new Stage(nodes, new[] { 0 });
        }

        [Test]
        public void AvailableNodeIndices_BeforeAnyMove_ReturnsEntryNodes()
        {
            var stage = BuildSimpleStage();

            Assert.That(stage.AvailableNodeIndices, Is.EquivalentTo(new[] { 0 }));
        }

        [Test]
        public void MoveTo_ReachableNode_UpdatesCurrentNodeIndex()
        {
            var stage = BuildSimpleStage();

            stage.MoveTo(0);

            Assert.That(stage.CurrentNodeIndex, Is.EqualTo(0));
        }

        [Test]
        public void MoveTo_UnreachableNode_Throws()
        {
            var stage = BuildSimpleStage();

            Assert.Throws<System.InvalidOperationException>(() => stage.MoveTo(1));
        }

        [Test]
        public void AvailableNodeIndices_AfterMove_ReturnsCurrentNodesNextIndices()
        {
            var stage = BuildSimpleStage();

            stage.MoveTo(0);

            Assert.That(stage.AvailableNodeIndices, Is.EquivalentTo(new[] { 1 }));
        }

        [Test]
        public void MoveTo_SkipsAheadPastAvailableNodes_Throws()
        {
            var stage = BuildSimpleStage();
            stage.MoveTo(0);

            Assert.Throws<System.InvalidOperationException>(() => stage.MoveTo(2));
        }

        [Test]
        public void IsComplete_BeforeAnyMove_ReturnsFalse()
        {
            var stage = BuildSimpleStage();

            Assert.That(stage.IsComplete, Is.False);
        }

        [Test]
        public void IsComplete_AtNonBossNode_ReturnsFalse()
        {
            var stage = BuildSimpleStage();
            stage.MoveTo(0);

            Assert.That(stage.IsComplete, Is.False);
        }

        [Test]
        public void IsComplete_AtBossNode_ReturnsTrue()
        {
            var stage = BuildSimpleStage();
            stage.MoveTo(0);
            stage.MoveTo(1);
            stage.MoveTo(2);

            Assert.That(stage.IsComplete, Is.True);
        }
    }
}
