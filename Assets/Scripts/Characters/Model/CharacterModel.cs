using Characters.Skins;
using Characters.SO;
using UnityEngine;

namespace Characters.Model
{
    public class CharacterModel
    {
        private int _id;

        private Fraction.Fraction _fraction;

        private Skin _skin;
        private Color _color;

        private bool _allowsDivisionGeneration;
        private bool _isPlayerControlled;

        public int Id => _id;
        public Fraction.Fraction Fraction => _fraction;
        public Skin Skin => _skin;
        public Color Color => _color;
        public bool AllowsDivisionGeneration => _allowsDivisionGeneration;
        public bool IsPlayerControlled => _isPlayerControlled;

        public CharacterModel(CharacterSO so)
        {
            _id = so.Id;
            _fraction = so.Fraction;
            _skin = so.Skin;
            _color = so.Color;
            _allowsDivisionGeneration = so.AllowsDivisionGeneration;
            _isPlayerControlled = so.IsPlayerControlled;
        }
    }
}