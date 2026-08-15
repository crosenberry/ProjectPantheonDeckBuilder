using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Pantheon.Core.Run;

namespace Pantheon.Unity
{
    public class MapView : MonoBehaviour
    {
        public RunController Controller;
        public List<Button> NodeButtons;
        public Text StatusText;

        private void Update()
        {
            if (Controller == null || Controller.CurrentRun == null)
            {
                return;
            }

            var run = Controller.CurrentRun;
            var stage = run.CurrentStage;

            if (StatusText != null)
            {
                StatusText.text = run.Outcome switch
                {
                    RunOutcome.Victory => "Victory! The stage's Boss has fallen.",
                    RunOutcome.Defeat => "Defeat. Your run has ended.",
                    _ => $"Player HP: {Controller.Player.CurrentHP}/{Controller.Player.MaxHP} - Choose your path"
                };
            }

            var available = run.Outcome == RunOutcome.InProgress && !Controller.InCombat
                ? stage.AvailableNodeIndices
                : (IReadOnlyList<int>)new int[0];

            for (var i = 0; i < NodeButtons.Count; i++)
            {
                var button = NodeButtons[i];
                if (button == null)
                {
                    continue;
                }

                if (i >= stage.Nodes.Count)
                {
                    button.gameObject.SetActive(false);
                    continue;
                }

                button.gameObject.SetActive(true);
                button.interactable = available.Contains(i);

                var label = button.GetComponentInChildren<Text>();
                if (label != null)
                {
                    label.text = $"{stage.Nodes[i].Type}";
                }

                var nodeIndex = i;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => Controller.EnterNode(nodeIndex));
            }
        }
    }
}
