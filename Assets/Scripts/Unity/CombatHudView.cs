using UnityEngine;
using UnityEngine.UI;

namespace Pantheon.Unity
{
    public class CombatHudView : MonoBehaviour
    {
        public CombatEncounterRunner Runner;
        public Text StatusText;

        private void Update()
        {
            if (Runner == null || Runner.Encounter == null || StatusText == null)
            {
                return;
            }

            var player = Runner.Encounter.Player;
            var enemy = Runner.Encounter.Enemy;

            StatusText.text =
                $"Player HP: {player.CurrentHP}/{player.MaxHP}  Block: {player.CurrentBlock}  Energy: {player.CurrentEnergy}/{player.MaxEnergy}\n" +
                $"Enemy HP: {enemy.CurrentHP}/{enemy.MaxHP}\n" +
                $"Outcome: {Runner.Encounter.Outcome}";
        }
    }
}
