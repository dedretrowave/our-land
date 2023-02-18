using Src.Divisions.Number;
using TMPro;
using UnityEngine;

namespace Src.Divisions.UI
{
    public class DivisionNumberUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private DivisionNumber _number;

        private void Start()
        {
            UpdateNumber(_number.Number);
        }

        public void UpdateNumber(int number)
        {
            _text.text = number.ToString();
        }
    }
}