using System.Collections.Generic;
using Characters.Model;
using Characters.Skins;
using Characters.SO;
using DI;
using UnityEngine;

namespace Characters
{
    public class CharacterContainer : MonoBehaviour
    {
        [SerializeField] private List<CharacterSO> _characterSOs;

        private List<CharacterModel> _characters = new();

        private void Awake()
        {
            _characterSOs.ForEach(characterSO => Add(new(characterSO)));
            
            DependencyContext.Dependencies.Add(new(typeof(CharacterContainer), () => this));
        }

        public void Add(CharacterModel character)
        {
            if (!_characters.Contains(character))
            {
                _characters.Add(character);
            }
        }

        public CharacterModel GetById(int id)
        {
            return _characters.Find(character => character.Id == id);
        }

        public CharacterModel GetByFraction(Fraction.Fraction fraction)
        {
            return _characters.Find(character => character.Fraction == fraction);
        }
    }
}