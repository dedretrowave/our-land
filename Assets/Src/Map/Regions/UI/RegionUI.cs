using Src.Map.Fraction;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Map.Regions.UI
{
    public class RegionUI : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void UpdateColorByFraction(Character fraction)
        {
            // _image.color = fraction.Color;
        }
    }
}