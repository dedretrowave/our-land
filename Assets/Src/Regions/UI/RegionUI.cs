using Src.Global;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Regions.UI
{
    public class RegionUI : MonoBehaviour
    {
        [SerializeField] private RawImage _image;

        private ColorsToFractions _colorsConfig;

        public void UpdateColorByFraction(Fraction.Fraction fraction)
        {
            _image.color = _colorsConfig.GetColorByFraction(fraction);
        }

        private void Start()
        {
            _colorsConfig = DependencyContext.Dependencies.Get<ColorsToFractions>();
        }
    }
}