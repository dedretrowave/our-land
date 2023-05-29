using System.Collections.Generic;
using Characters.Base;
using Characters.SO;
using DI;
using UnityEngine;

namespace Characters
{
    public class CharacterFactory : MonoBehaviour
    {
        [SerializeField] private List<CharacterSO> _characters;

        public Character Get(Fraction.Fraction fraction)
        {
            CharacterSO characterSO = _characters.Find(character => character.Fraction == fraction);

            return new Character(characterSO);
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(CharacterFactory), () => this));
            DontDestroyOnLoad(gameObject);
        }
    }
}