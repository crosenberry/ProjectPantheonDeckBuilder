namespace Pantheon.Core.Combat
{
    public class ChangeToFormEffect : CardEffect
    {
        public Form TargetForm { get; }

        public ChangeToFormEffect(Form targetForm)
        {
            TargetForm = targetForm;
        }

        public override void Apply(CardEffectContext context)
        {
            context.Player.ChangeForm(TargetForm);
        }
    }
}
