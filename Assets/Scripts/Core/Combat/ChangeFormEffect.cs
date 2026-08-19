namespace Pantheon.Core.Combat
{
    public class ChangeFormEffect : CardEffect
    {
        public override void Apply(CardEffectContext context)
        {
            context.Player.ChangeForm();
        }
    }
}
