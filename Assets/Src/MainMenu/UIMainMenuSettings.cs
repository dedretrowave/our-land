using UnityEngine;
using UnityEngine.UI;

namespace Src.MainMenu
{
    public class UIMainMenuSettings : MonoBehaviour
    {
        [SerializeField] private Toggle _soundToggle;

        public void SetSoundToggle(bool value)
        {
            _soundToggle.isOn = value;
        }
    }
}