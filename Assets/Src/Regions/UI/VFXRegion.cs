using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Src.Regions.UI
{
    public class VFXRegion : MonoBehaviour
    {
        [SerializeField] private RawImage _regionColor;
        [SerializeField] private List<ColorByOwner> _colors;

        public void ChangeColorByFraction(Fraction fraction)
        {
            _regionColor.color = _colors.Find(color => color.Fraction == fraction).Color;
        }
    }

    [Serializable]
    class ColorByOwner
    {
        public Color Color;
        public Fraction Fraction;
    }
}