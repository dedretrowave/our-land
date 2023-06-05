using System.Collections.Generic;
using System.Linq;
using Characters.Model;
using Characters.SO;
using DI;
using UnityEngine;

namespace Characters
{
    public class CharacterContainer : MonoBehaviour
    {
        [SerializeField] private List<CharacterSO> _characterSOs;
        private List<CharacterModel> _characters = new();

        public void AddCharacter(CharacterModel character)
        {
            if (!_characters.Contains(character))
            {
                _characters.Add(character);
            }
        }

        public CharacterModel Get(Fraction.Fraction fraction)
        {
            return _characters.Find(character => character.Fraction == fraction);
        }

        private void Awake()
        {
            _characterSOs.ForEach(characterSO => _characters.Add(new(characterSO)));
            
            DependencyContext.Dependencies.Add(new(typeof(CharacterContainer), () => this));
            DontDestroyOnLoad(gameObject);
        }
    }
}