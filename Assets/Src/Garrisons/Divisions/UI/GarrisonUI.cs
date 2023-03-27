using UnityEngine;
using UnityEngine.UI;

namespace Src.Garrisons.Divisions.UI
{
    public class GarrisonUI : MonoBehaviour
    {
        [SerializeField] private Image _flag;
        [SerializeField] private Image _eyes;

        public void UpdateByFraction(Fraction.Fraction fraction)
        {
            _flag.sprite = fraction.Flag;
            _eyes.sprite = fraction.Eyes;
        }
    }
}