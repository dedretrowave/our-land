using UnityEngine;
using UnityEngine.UI;

namespace Src.Map.Regions.UI
{
    public class RegionUI : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void UpdateColorByFraction(Fraction.Fraction fraction)
        {
            _image.color = fraction.Color;
        }
    }
}