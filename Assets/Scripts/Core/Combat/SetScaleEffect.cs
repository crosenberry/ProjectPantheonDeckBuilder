namespace Pantheon.Core.Combat
{
    public class SetScaleEffect : CardEffect
    {
        public int Value { get; }

        public SetScaleEffect(int value)
        {
            Value = value;
        }

        public override void Apply(CardEffectContext context)
        {
            context.Player.SetScale(Value);
        }
    }
}
