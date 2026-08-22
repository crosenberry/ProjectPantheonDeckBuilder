using UnityEngine;
using UnityEngine.UI;

namespace Pantheon.Unity
{
    public class MainMenuView : MonoBehaviour
    {
        public RunController Controller;
        public Button PlayButton;

        private void Start()
        {
            if (PlayButton != null)
            {
                PlayButton.onClick.AddListener(() => Controller.StartPlay());
            }
        }
    }
}
