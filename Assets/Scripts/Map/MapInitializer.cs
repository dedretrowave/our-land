using System;
using System.Collections.Generic;
using Characters.Model;
using Characters.SO;
using Region.Models;
using Save;
using UnityEngine;

namespace Map
{
    public class MapInitializer : MonoBehaviour
    {
        [SerializeField] private List<CharacterSORegion> _regionCharacterSODefault;

        private RegionModelLoader _regionModelLoader;

        public void Construct()
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

                CreateModel(regionModel, region);
            }
        }

        public void Disable()
        {
            _regionCharacterSODefault.ForEach(
                region => region.Region.OnModelUpdated -= SaveModel);
        }

        private void SaveModel(RegionModel model)
        {
            _regionModelLoader.SaveRegion(model);
        }

        private void CreateDefaultModels()
        {
            for (int i = 0; i < _regionCharacterSODefault.Count; i++)
            {
                MapRegionInstaller region = _regionCharacterSODefault[i].Region;
                CharacterModel character = new(_regionCharacterSODefault[i].CharacterSO);

                CreateModel(new(character, i), region);
            }
        }

        private void CreateModel(RegionModel model, MapRegionInstaller region)
        {
            _regionModelLoader.SaveRegion(model);
            region.Construct(model);
            region.OnModelUpdated += SaveModel;
        }
    }

    [Serializable]
    internal class CharacterSORegion
    {
        public MapRegionInstaller Region;
        public CharacterSO CharacterSO;
    }
}