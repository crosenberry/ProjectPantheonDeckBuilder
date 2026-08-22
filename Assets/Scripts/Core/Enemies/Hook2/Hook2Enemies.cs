using Pantheon.Core.Combat;

namespace Pantheon.Core.Enemies.Hook2
{
    public static class Hook2Enemies
    {
        public static Enemy WrathfulErinys(IRandom random)
        {
            return new Enemy(maxHP: 34, new[]
            {
                new EnemyMove("Seethe", IntentType.Buff, value: 0, stormDelta: 3, maxStorm: 5),
                new EnemyMove("Vengeance Strike", IntentType.Attack, value: 20, consumesStorm: true, minStorm: 6)
            }, random);
        }

        public static Enemy ThunderhideJotunn(IRandom random)
        {
            return new Enemy(maxHP: 38, new[]
            {
                new EnemyMove("Gather Squall", IntentType.Buff, value: 0, stormDelta: 2, maxStorm: 5),
                new EnemyMove("Storm Slam", IntentType.Attack, value: 16, consumesStorm: true, minStorm: 6)
            }, random);
        }

        public static Enemy AmmitsShade(IRandom random)
        {
            return new Enemy(maxHP: 36, new[]
            {
                new EnemyMove("Sway Toward Chaos", IntentType.Attack, value: 6, weight: 2, scaleDelta: -2),
                new EnemyMove("Sway Toward Order", IntentType.Block, value: 6, weight: 2, scaleDelta: 2),
                new EnemyMove("Chaos Surge", IntentType.Attack, value: 14, weight: 3, maxScale: -4)
            }, random);
        }

        public static Enemy StoneGuardian(IRandom random)
        {
            return new Enemy(maxHP: 40, new[]
            {
                new EnemyMove("Guard", IntentType.Block, value: 8, requiredForm: Form.Mortal, formTarget: Form.Beast),
                new EnemyMove("Savage Claw", IntentType.Attack, value: 12, requiredForm: Form.Beast, formTarget: Form.Immortal),
                new EnemyMove("Radiant Ward", IntentType.Block, value: 14, requiredForm: Form.Immortal, formTarget: Form.Mortal)
            }, random);
        }
    }
}
