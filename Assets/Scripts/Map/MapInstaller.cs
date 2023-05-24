using System;
using Characters.Base;
using Characters.SO;
using Level;
using Level.Region.Views;
using UnityEditor;
using UnityEngine;

namespace Map
{
    public class MapInstaller : MonoBehaviour
    {
        [SerializeField] private RegionCharacterSODictionary _characterSORegions;
        
        private CharacterRegionContainer _characterRegionContainer;

        public void Construct()
        {
            foreach (var regionCharacterSO in _characterSORegions)
            {
                Character characterFromSO = new(regionCharacterSO.Value);

                RegionView region = regionCharacterSO.Key;
                
                region.SetColor(characterFromSO.Color);
                region.SetSkin(characterFromSO.Skin);
            }
        }
    }
    
    [Serializable]
    internal class RegionCharacterSODictionary : SerializableDictionary<RegionView, CharacterSO> {}
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(RegionCharacterSODictionary))]
    internal class RegionCharacterSODictionaryUI : SerializableDictionaryPropertyDrawer {}
#endif
}