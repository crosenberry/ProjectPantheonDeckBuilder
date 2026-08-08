using System.Collections.Generic;

namespace Pantheon.Core.Combat
{
    public class PlayerEnhancement
    {
        public string Name { get; }
        public IReadOnlyList<TriggeredEffect> Triggers { get; }

        public PlayerEnhancement(string name, IReadOnlyList<TriggeredEffect> triggers)
        {
            Name = name;
            Triggers = triggers;
        }

        public void Grant(Player player)
        {
            foreach (var trigger in Triggers)
            {
                player.AddTrigger(trigger);
            }
        }
    }
}
