using System.Collections.Generic;

namespace Pantheon.Core.Combat
{
    public class CardEffectContext
    {
        public Player Player { get; }
        public Enemy Enemy { get; }
        public IReadOnlyList<Enemy> Enemies { get; }

        public CardEffectContext(Player player, Enemy enemy) : this(player, enemy, new[] { enemy })
        {
        }

        public CardEffectContext(Player player, Enemy enemy, IReadOnlyList<Enemy> enemies)
        {
            Player = player;
            Enemy = enemy;
            Enemies = enemies;
        }

        public ICombatant Resolve(EffectTarget target)
        {
            return target == EffectTarget.Self ? (ICombatant)Player : Enemy;
        }
    }
}
