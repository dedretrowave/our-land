using TMPro;
using UnityEngine;

namespace Src.Regions.RegionDivisions.UI
{
    public class UIDivisionsBase : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void UpdateInfo(int number)
        {
            _text.text = number.ToString();
        }
    }
}