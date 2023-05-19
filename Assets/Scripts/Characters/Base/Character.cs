using Characters.SO;
using UnityEngine;

namespace Characters.Base
{
    public class Character
    {
        private int _id;

        private Color _color;

        private bool _allowsDivisionGeneration;
        private bool _isPlayerControlled;

        public int Id => _id;
        public Color Color => _color;
        public bool AllowsDivisionGeneration => _allowsDivisionGeneration;
        public bool IsPlayerControlled => _isPlayerControlled;

        public Character(CharacterSO so)
        {
            _id = so.Id;
            _color = so.Color;
            _allowsDivisionGeneration = so.AllowsDivisionGeneration;
            _isPlayerControlled = so.IsPlayerControlled;
        }
    }
}