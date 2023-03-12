using Src.Global;
using Src.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Divisions.Divisions.VFX
{
    public class DivisionVFX : MonoBehaviour
    {
        [SerializeField] private RawImage _labelImage;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Transform _uiWrapper;

        private ColorsSettings _colorsToFractions;

        public void Init(Division division)
        {
            _colorsToFractions = DependencyContext.Dependencies.Get<ColorsSettings>();
            _labelImage.color = _colorsToFractions.GetColorByFraction(division.Fraction);
            _text.text = division.Amount.ToString();
            // _uiWrapper.transform.LookAt(Camera.main.transform, Vector3.up);
        }
    }
}