namespace Pantheon.Core.Combat
{
    public class DamageEqualToCurrentBlockEffect : CardEffect
    {
        public override void Apply(CardEffectContext context)
        {
            var damage = CombatMath.ApplyDamageModifiers(context.Player.CurrentBlock, context.Player.Strength, context.Player.Drained, context.Enemy.Exposed);
            context.Enemy.TakeDamage(damage);
        }
    }
}
