using UnityEngine;
using UnityEngine.UI;

namespace Pantheon.Unity
{
    public class BlessingSelectView : MonoBehaviour
    {
        public RunController Controller;
        public Button ArtemisButton;
        public Button ThorButton;
        public Button AnubisButton;
        public Button SunWukongButton;

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

            if (AnubisButton != null)
            {
                AnubisButton.onClick.AddListener(() => Controller.SelectBlessing(SelectedBlessing.Anubis));
            }

            if (SunWukongButton != null)
            {
                SunWukongButton.onClick.AddListener(() => Controller.SelectBlessing(SelectedBlessing.SunWukong));
            }
        }
    }
}
