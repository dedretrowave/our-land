using UnityEngine;
using UnityEngine.UI;

namespace Src.Fraction
{
    [CreateAssetMenu(menuName = "Game Entities", fileName = "Fraction", order = 0)]
    public class Fraction : ScriptableObject
    {
        [Header("Components")]
        [SerializeField] private Image _selectedFlag;
        [SerializeField] private Image _selectedEyes;
        [SerializeField] private Color _color;

        [Header("Parameters")]
        [SerializeField] private bool _allowsDivisionGeneration;

        public Image Flag => _selectedFlag;
        public Image Eyes => _selectedEyes;
        public Color Color => _color;
        public bool AllowsDivisionGeneration => _allowsDivisionGeneration;
    }
}