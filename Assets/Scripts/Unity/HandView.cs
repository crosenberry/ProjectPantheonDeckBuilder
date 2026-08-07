using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pantheon.Core.Combat;

namespace Pantheon.Unity
{
    public class HandView : MonoBehaviour
    {
        public CombatEncounterRunner Runner;
        public List<Button> CardButtons;

        private void Update()
        {
            if (Runner == null || Runner.Encounter == null || CardButtons == null)
            {
                return;
            }

            var hand = Runner.Encounter.Player.Hand;
            var combatActive = Runner.Encounter.Outcome == CombatOutcome.InProgress;

            for (var i = 0; i < CardButtons.Count; i++)
            {
                var button = CardButtons[i];
                if (button == null)
                {
                    continue;
                }

                if (i >= hand.Count)
                {
                    button.gameObject.SetActive(false);
                    continue;
                }

                var card = hand[i];
                button.gameObject.SetActive(true);
                button.interactable = combatActive;

                var label = button.GetComponentInChildren<Text>();
                if (label != null)
                {
                    label.text = $"{card.Name}\n({card.EnergyCost})";
                }

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => Runner.PlayCard(card));
            }
        }
    }
}
