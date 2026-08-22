namespace Pantheon.Core.Combat
{
    public class EnemyMove
    {
        public string Name { get; }
        public IntentType Intent { get; }
        public int Value { get; }
        public int Weight { get; }
        public StatusType Status { get; }

        // Optional resource side effects/eligibility gates for Hook 2 enemies
        // (Docs/Enemies/Hook2-ResourceEnemies.md) - mirror the shape of the
        // corresponding Blessing resource. All default to no-op/no-gate so
        // every existing enemy move is unaffected.
        public int StormDelta { get; }
        public bool ConsumesStorm { get; }
        public int? MinStorm { get; }
        public int? MaxStorm { get; }
        public int ScaleDelta { get; }
        public int? MinScale { get; }
        public int? MaxScale { get; }
        public Form? FormTarget { get; }
        public Form? RequiredForm { get; }

        public EnemyMove(string name, IntentType intent, int value, int weight = 1, StatusType status = default,
            int stormDelta = 0, bool consumesStorm = false, int? minStorm = null, int? maxStorm = null,
            int scaleDelta = 0, int? minScale = null, int? maxScale = null,
            Form? formTarget = null, Form? requiredForm = null)
        {
            Name = name;
            Intent = intent;
            Value = value;
            Weight = weight;
            Status = status;
            StormDelta = stormDelta;
            ConsumesStorm = consumesStorm;
            MinStorm = minStorm;
            MaxStorm = maxStorm;
            ScaleDelta = scaleDelta;
            MinScale = minScale;
            MaxScale = maxScale;
            FormTarget = formTarget;
            RequiredForm = requiredForm;
        }
    }
}
