namespace Pantheon.Core.Combat
{
    public class EnemyMove
    {
        public string Name { get; }
        public IntentType Intent { get; }
        public int Value { get; }
        public int Weight { get; }
        public StatusType Status { get; }

        public EnemyMove(string name, IntentType intent, int value, int weight = 1, StatusType status = default)
        {
            Name = name;
            Intent = intent;
            Value = value;
            Weight = weight;
            Status = status;
        }
    }
}
