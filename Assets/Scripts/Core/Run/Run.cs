namespace Pantheon.Core.Run
{
    public class Run
    {
        public int StageCount { get; }
        public int CompletedStageCount { get; private set; }
        public Stage CurrentStage { get; private set; }
        public RunOutcome Outcome { get; private set; }

        public Run(int stageCount, Stage firstStage)
        {
            StageCount = stageCount;
            CurrentStage = firstStage;
            Outcome = RunOutcome.InProgress;
        }

        public void AdvanceToNextStage(Stage nextStage)
        {
            if (!CurrentStage.IsComplete)
            {
                throw new System.InvalidOperationException("Current stage's Boss has not been defeated yet.");
            }

            if (CompletedStageCount + 1 >= StageCount)
            {
                throw new System.InvalidOperationException("This was the final stage; call CompleteFinalStage instead.");
            }

            CompletedStageCount += 1;
            CurrentStage = nextStage;
        }

        public void CompleteFinalStage()
        {
            if (!CurrentStage.IsComplete)
            {
                throw new System.InvalidOperationException("Current stage's Boss has not been defeated yet.");
            }

            if (CompletedStageCount + 1 < StageCount)
            {
                throw new System.InvalidOperationException("This was not the final stage; call AdvanceToNextStage instead.");
            }

            CompletedStageCount += 1;
            Outcome = RunOutcome.Victory;
        }

        public void RecordDefeat()
        {
            if (Outcome == RunOutcome.InProgress)
            {
                Outcome = RunOutcome.Defeat;
            }
        }
    }
}
