using TMPro;
using UnityEngine;

namespace Src.Divisions.UI
{
    public class GarrisonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void UpdateAmount(int amount)
        {
            _text.text = amount.ToString();
        }
    }
}