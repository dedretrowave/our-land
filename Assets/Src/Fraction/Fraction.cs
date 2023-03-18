using UnityEngine;

namespace Src.Fraction
{
    [CreateAssetMenu(menuName = "Game Entities", fileName = "Fraction", order = 0)]
    public class Fraction : ScriptableObject
    {
        [Header("Components")]
        [SerializeField] private Sprite _selectedFlag;
        [SerializeField] private Sprite _selectedEyes;
        [SerializeField] private Color _color;

        [Header("Parameters")]
        [SerializeField] private bool _allowsDivisionGeneration;
        [SerializeField] private bool _isPlayerControlled;

        public Sprite Flag
        {
            get => _selectedFlag;
            set => _selectedFlag = value;
        }
        public Sprite Eyes => _selectedEyes;
        public Color Color => _color;
        public bool AllowsDivisionGeneration => _allowsDivisionGeneration;
        public bool IsPlayerControlled => _isPlayerControlled;
    }
}