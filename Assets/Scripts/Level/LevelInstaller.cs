using System;
using System.Collections.Generic;
using Level.Region;
using Src.SerializableDictionary.Editor;
using UnityEditor;
using UnityEngine;

namespace Level
{
    public class LevelInstaller : MonoBehaviour
    {
        [SerializeField] private FractionRegionDictionary _fractionRegions;

        public void Construct()
        {
            foreach (var fractionRegion in _fractionRegions)
            {
                Fraction.Fraction fraction = fractionRegion.Key;
                List<RegionInstaller> regions = fractionRegion.Value;

                regions.ForEach(region =>
                {
                    region.Construct(fraction);
                });
            }
        }
    }
    
    [Serializable]
    internal class FractionRegionDictionary : SerializableDictionary<Fraction.Fraction, List<RegionInstaller>> {}
    [CustomPropertyDrawer(typeof(FractionRegionDictionary))]
    internal class FractionRegionDictionaryUI : SerializableDictionaryPropertyDrawer {}
}