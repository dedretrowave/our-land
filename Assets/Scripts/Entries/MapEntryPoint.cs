using System;
using System.Collections.Generic;
using Characters.Model;
using Characters.SO;
using Map;
using Region.Models;
using Save;
using UnityEngine;

namespace Entries
{
    public class MapEntryPoint : MonoBehaviour
    {
        [SerializeField] private List<RegionCharacterSO> _regionCharacterSODefault;

        private RegionModelLoader _regionModelLoader;

        private void Start()
        {
            _regionModelLoader = new();

            List<RegionModel> regionModels = _regionModelLoader.GetRegions();

            if (regionModels.Count == 0)
            {
                CreateDefaultModels();
                return;
            }
            
            for (int i = 0; i < _regionCharacterSODefault.Count; i++)
            {
                MapRegionInstaller region = _regionCharacterSODefault[i].Region;
                RegionModel regionModel = regionModels[i];

                _regionModelLoader.SaveRegion(regionModel);
                region.Construct(regionModel);
            }
        }

        private void CreateDefaultModels()
        {
            for (int i = 0; i < _regionCharacterSODefault.Count; i++)
            {
                MapRegionInstaller region = _regionCharacterSODefault[i].Region;
                CharacterModel character = new(_regionCharacterSODefault[i].CharacterSO);
                RegionModel regionModel = new(character, i);

                _regionModelLoader.SaveRegion(regionModel);
                region.Construct(regionModel);
            }
        }
    }

    [Serializable]
    internal class RegionCharacterSO
    {
        public MapRegionInstaller Region;
        public CharacterSO CharacterSO;
    }
}