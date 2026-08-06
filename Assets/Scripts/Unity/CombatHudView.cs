using Pantheon.Core.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace Pantheon.Unity
{
    public class CombatHudView : MonoBehaviour
    {
        public CombatEncounterRunner Runner;
        public Text StatusText;
        public Button EndTurnButton;

        private void Update()
        {
            if (Runner == null || Runner.Encounter == null || StatusText == null)
            {
                return;
            }

            var player = Runner.Encounter.Player;
            var enemy = Runner.Encounter.Enemy;
            var intentText = enemy.CurrentIntent != null
                ? $"{enemy.CurrentIntent.Name} ({enemy.CurrentIntent.Intent}: {enemy.CurrentIntent.Value})"
                : "-";

            StatusText.text =
                $"Player HP: {player.CurrentHP}/{player.MaxHP}  Block: {player.CurrentBlock}  Energy: {player.CurrentEnergy}/{player.MaxEnergy}  Volley: {player.Volley}\n" +
                $"Enemy HP: {enemy.CurrentHP}/{enemy.MaxHP}  Block: {enemy.CurrentBlock}  Intent: {intentText}\n" +
                $"Outcome: {Runner.Encounter.Outcome}";

            var combatActive = Runner.Encounter.Outcome == CombatOutcome.InProgress;
            if (EndTurnButton != null)
            {
                EndTurnButton.interactable = combatActive;
            }
        }
    }
}
