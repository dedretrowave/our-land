using System;
using Characters.Skins;
using UnityEngine;

namespace Characters.SO
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game Entities/Character", fileName = "Character", order = 0)]
    public class CharacterSO : ScriptableObject
    {
        [SerializeField] private int _id;

        [Header("Fraction")]
        [SerializeField] private Fraction.Fraction _fraction;
        
        [Header("Components")]
        [SerializeField] private SkinData _skin;
        [SerializeField] private Color _color;

        [Header("Parameters")]
        [SerializeField] private bool _allowsDivisionGeneration;
        [SerializeField] private bool _isPlayerControlled;

        public int Id => _id;
        public Fraction.Fraction Fraction => _fraction;
        public Skin Skin => new (_skin);
        public Color Color => _color;
        public bool AllowsDivisionGeneration => _allowsDivisionGeneration;
        public bool IsPlayerControlled => _isPlayerControlled;
    }
}