using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pantheon.Unity
{
    public class BossRewardView : MonoBehaviour
    {
        public RunController Controller;
        public List<Button> RelicButtons;
        public Text StatusText;

        private void Update()
        {
            if (Controller == null || !Controller.AwaitingBossReward)
            {
                return;
            }

            if (StatusText != null)
            {
                StatusText.text = "A Soul falls. Choose a relic:";
            }

            var offered = Controller.OfferedBossRelics;

            for (var i = 0; i < RelicButtons.Count; i++)
            {
                var button = RelicButtons[i];
                if (button == null)
                {
                    continue;
                }

                if (offered == null || i >= offered.Count)
                {
                    button.gameObject.SetActive(false);
                    continue;
                }

                button.gameObject.SetActive(true);
                var relic = offered[i];

                var label = button.GetComponentInChildren<Text>();
                if (label != null)
                {
                    label.text = $"{relic.Name}\n{relic.FlavorText}";
                }

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => Controller.ClaimBossReward(relic));
            }
        }
    }
}
