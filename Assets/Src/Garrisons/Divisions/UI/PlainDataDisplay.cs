using TMPro;
using UnityEngine;

namespace Src.Garrisons.Divisions.UI
{
    public class PlainDataDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void UpdateData(int data)
        {
            _text.text = data.ToString();
        }
    }
}