namespace Pantheon.Core.Combat
{
    public class CardEffectContext
    {
        public Player Player { get; }
        public Enemy Enemy { get; }

        public CardEffectContext(Player player, Enemy enemy)
        {
            Player = player;
            Enemy = enemy;
        }

        public ICombatant Resolve(EffectTarget target)
        {
            return target == EffectTarget.Self ? (ICombatant)Player : Enemy;
        }
    }
}
