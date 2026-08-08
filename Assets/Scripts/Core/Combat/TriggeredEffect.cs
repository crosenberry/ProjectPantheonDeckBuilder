namespace Pantheon.Core.Combat
{
    public class TriggeredEffect
    {
        public TriggerEvent Event { get; }
        public CardEffect Effect { get; }

        public TriggeredEffect(TriggerEvent @event, CardEffect effect)
        {
            Event = @event;
            Effect = effect;
        }
    }
}
