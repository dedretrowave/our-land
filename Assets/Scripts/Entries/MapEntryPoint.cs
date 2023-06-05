using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Model;
using Characters.SO;
using DI;
using Map;
using Region.Models;
using Region.Views;
using UnityEditor;
using UnityEngine;

namespace Entries
{
    public class MapEntryPoint : MonoBehaviour
    {
        [SerializeField] private RegionCharacterSODictionary _regionCharacterSODefault;

        private void Start()
        {
            //TODO: Load models from Save?

            foreach (var regionCharacterSO in _regionCharacterSODefault)
            {
                MapRegionInstaller region = regionCharacterSO.Key;
                CharacterModel character = new(regionCharacterSO.Value);
                RegionModel regionModel = new(character);

                region.Construct(regionModel);
            }
        }
    }
    
    [Serializable]
    internal class RegionCharacterSODictionary : SerializableDictionary<MapRegionInstaller, CharacterSO> {}
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(RegionCharacterSODictionary))]
    internal class RegionCharacterSODictionaryUI : SerializableDictionaryPropertyDrawer {}
#endif
}