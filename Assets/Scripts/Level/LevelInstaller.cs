using System;
using System.Collections.Generic;
using Characters.SO;
using DI;
using Level.Region;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

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
                List<RegionInstaller> regions = fractionRegion.Value.List;

                regions.ForEach(region =>
                {
                    region.Construct(new (characterSO));
                });
            }
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(LevelInstaller), () => this));
        }
    }

    [Serializable]
    internal class Regions
    { 
        public List<RegionInstaller> List;
    }
    
#if UNITY_EDITOR
    [Serializable]
    internal class CharacterSORegionDictionary : SerializableDictionary<CharacterSO, Regions> {}
    [CustomPropertyDrawer(typeof(CharacterSORegionDictionary))]
    internal class CharacterSORegionDictionaryUI : SerializableDictionaryPropertyDrawer {}
#endif
}