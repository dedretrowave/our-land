using Characters.Skins;
using UnityEngine;

namespace Characters.SO
{
    [CreateAssetMenu(menuName = "Game Entities/Character", fileName = "Character", order = 0)]
    public class CharacterSO : ScriptableObject
    {
        [SerializeField] private int _id;

        [Header("Fraction")]
        [SerializeField] private Fraction.Fraction _fraction;
        
        [Header("Components")]
        [SerializeField] private Skin _skin;
        [SerializeField] private Color _color;

        [Header("Parameters")]
        [SerializeField] private bool _allowsDivisionGeneration;
        [SerializeField] private bool _isPlayerControlled;

        public int Id => _id;
        public Fraction.Fraction Fraction => _fraction;
        public Skin Skin => _skin;
        public Sprite Flag => _skin.GetItemByType(SkinItemType.Flag).Sprite;
        public Sprite Eyes => _skin.GetItemByType(SkinItemType.Eyes).Sprite;
        public Color Color => _color;
        public bool AllowsDivisionGeneration => _allowsDivisionGeneration;
        public bool IsPlayerControlled => _isPlayerControlled;
    }
}