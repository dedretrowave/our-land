using System;
using Characters.Base;
using Characters.SO;
using Src.SerializableDictionary.Editor;
using UnityEditor;
using UnityEngine;

namespace Characters.Factory
{
    public class CharacterFactory : MonoBehaviour
    {
        [SerializeField] private FractionCharacterDictionary _fractionCharacters;

        public Character CreateCharacter(Fraction.Fraction fraction)
        {
            return new (_fractionCharacters[fraction]);
        }
    }
    
    [Serializable]
    internal class FractionCharacterDictionary : SerializableDictionary<Fraction.Fraction, CharacterSO> {}
    [CustomPropertyDrawer(typeof(FractionCharacterDictionary))]
    internal class FractionCharacterDictionaryUI : SerializableDictionaryPropertyDrawer {}
}