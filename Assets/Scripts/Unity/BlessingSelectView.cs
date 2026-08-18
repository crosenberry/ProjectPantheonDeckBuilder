using UnityEngine;
using UnityEngine.UI;

namespace Pantheon.Unity
{
    public class BlessingSelectView : MonoBehaviour
    {
        public RunController Controller;
        public Button ArtemisButton;
        public Button ThorButton;

        private void Start()
        {
            if (ArtemisButton != null)
            {
                ArtemisButton.onClick.AddListener(() => Controller.SelectBlessing(SelectedBlessing.Artemis));
            }

            if (ThorButton != null)
            {
                ThorButton.onClick.AddListener(() => Controller.SelectBlessing(SelectedBlessing.Thor));
            }
        }
    }
}
