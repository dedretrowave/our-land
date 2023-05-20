using System;
using System.Collections.Generic;
using Characters.SO;
using Level.Region;
using Src.SerializableDictionary.Editor;
using UnityEditor;
using UnityEngine;

namespace Level
{
    public class LevelInstaller : MonoBehaviour
    {
        [SerializeField] private CharacterSORegionDictionary _characterRegions;

        public void Construct()
        {
            foreach (var fractionRegion in _characterRegions)
            {
                CharacterSO characterSO = fractionRegion.Key;
                List<RegionInstaller> regions = fractionRegion.Value;

                regions.ForEach(region =>
                {
                    region.Construct(new (characterSO));
                });
            }
        }
    }
    
    [Serializable]
    internal class CharacterSORegionDictionary : SerializableDictionary<CharacterSO, List<RegionInstaller>> {}
    [CustomPropertyDrawer(typeof(CharacterSORegionDictionary))]
    internal class CharacterSORegionDictionaryUI : SerializableDictionaryPropertyDrawer {}
}