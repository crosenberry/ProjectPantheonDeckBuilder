using Pantheon.Core.Combat;

namespace Pantheon.Core.Blessings.Artemis
{
    public static class ArtemisEnhancements
    {
        public static PlayerEnhancement TwinMoons()
        {
            return new PlayerEnhancement("Twin Moons", new[]
            {
                new TriggeredEffect(TriggerEvent.TurnStarted, new DrawCardEffect(1))
            });
        }

        public static PlayerEnhancement ApexPredatorsMantle()
        {
            return new PlayerEnhancement("Apex Predator's Mantle", new[]
            {
                new TriggeredEffect(TriggerEvent.TurnStarted, new ExposeLowHpEnemiesEffect(hpThresholdPercent: 25, amount: 2))
            });
        }
    }
}
