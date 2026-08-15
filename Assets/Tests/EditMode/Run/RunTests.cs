using NUnit.Framework;
using Pantheon.Core.Run;

namespace Pantheon.Core.Tests.Run
{
    public class RunTests
    {
        private static Stage BuildStage(bool complete)
        {
            var nodes = new[]
            {
                new MapNode(NodeType.Combat, new[] { 1 }),
                new MapNode(NodeType.Boss, new int[0])
            };
            var stage = new Stage(nodes, new[] { 0 });
            stage.MoveTo(0);
            if (complete)
            {
                stage.MoveTo(1);
            }

            return stage;
        }

        [Test]
        public void Constructor_SetsInitialState()
        {
            var firstStage = BuildStage(complete: false);

            var run = new global::Pantheon.Core.Run.Run(stageCount: 3, firstStage);

            Assert.That(run.StageCount, Is.EqualTo(3));
            Assert.That(run.CompletedStageCount, Is.EqualTo(0));
            Assert.That(run.CurrentStage, Is.SameAs(firstStage));
            Assert.That(run.Outcome, Is.EqualTo(RunOutcome.InProgress));
        }

        [Test]
        public void AdvanceToNextStage_CurrentStageComplete_SetsNewStageAndIncrementsCount()
        {
            var run = new global::Pantheon.Core.Run.Run(stageCount: 3, BuildStage(complete: true));
            var nextStage = BuildStage(complete: false);

            run.AdvanceToNextStage(nextStage);

            Assert.That(run.CurrentStage, Is.SameAs(nextStage));
            Assert.That(run.CompletedStageCount, Is.EqualTo(1));
        }

        [Test]
        public void AdvanceToNextStage_CurrentStageNotComplete_Throws()
        {
            var run = new global::Pantheon.Core.Run.Run(stageCount: 3, BuildStage(complete: false));

            Assert.Throws<System.InvalidOperationException>(() => run.AdvanceToNextStage(BuildStage(complete: false)));
        }

        [Test]
        public void AdvanceToNextStage_OnFinalStage_Throws()
        {
            var run = new global::Pantheon.Core.Run.Run(stageCount: 1, BuildStage(complete: true));

            Assert.Throws<System.InvalidOperationException>(() => run.AdvanceToNextStage(BuildStage(complete: false)));
        }

        [Test]
        public void CompleteFinalStage_OnFinalStage_SetsVictory()
        {
            var run = new global::Pantheon.Core.Run.Run(stageCount: 1, BuildStage(complete: true));

            run.CompleteFinalStage();

            Assert.That(run.Outcome, Is.EqualTo(RunOutcome.Victory));
            Assert.That(run.CompletedStageCount, Is.EqualTo(1));
        }

        [Test]
        public void CompleteFinalStage_NotYetFinalStage_Throws()
        {
            var run = new global::Pantheon.Core.Run.Run(stageCount: 3, BuildStage(complete: true));

            Assert.Throws<System.InvalidOperationException>(() => run.CompleteFinalStage());
        }

        [Test]
        public void CompleteFinalStage_CurrentStageNotComplete_Throws()
        {
            var run = new global::Pantheon.Core.Run.Run(stageCount: 1, BuildStage(complete: false));

            Assert.Throws<System.InvalidOperationException>(() => run.CompleteFinalStage());
        }

        [Test]
        public void RecordDefeat_SetsOutcomeToDefeat()
        {
            var run = new global::Pantheon.Core.Run.Run(stageCount: 3, BuildStage(complete: false));

            run.RecordDefeat();

            Assert.That(run.Outcome, Is.EqualTo(RunOutcome.Defeat));
        }

        [Test]
        public void RecordDefeat_AfterVictory_DoesNotOverwriteOutcome()
        {
            var run = new global::Pantheon.Core.Run.Run(stageCount: 1, BuildStage(complete: true));
            run.CompleteFinalStage();

            run.RecordDefeat();

            Assert.That(run.Outcome, Is.EqualTo(RunOutcome.Victory));
        }
    }
}
