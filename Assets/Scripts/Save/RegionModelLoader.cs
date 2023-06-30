using System;
using System.Collections.Generic;
using Characters;
using Characters.Model;
using Characters.Skins;
using DI;
using Newtonsoft.Json;
using Region.Models;
using UnityEngine;

namespace Save
{
    public class RegionModelLoader
    {
        private const string LocalPath = "regions";

        private SaveFileHandler _saveFileHandler;
        private CharacterContainer _characterContainer;

        private List<RegionModel> _regions = new();
        private List<RegionData> _regionDataSet;

        public RegionModelLoader()
        {
            _characterContainer = DependencyContext.Dependencies.Get<CharacterContainer>();
            _saveFileHandler = DependencyContext.Dependencies.Get<SaveFileHandler>();

            _regionDataSet = _saveFileHandler.Load<List<RegionData>>(LocalPath) 
                             ?? new();

            if (_regionDataSet.Count == 0) return;

            _regionDataSet.ForEach(region =>
            {
                CharacterModel regionOwner = _characterContainer.GetById(region.OwnerId);
                RegionModel regionModel = new(regionOwner, region.Id);
                
                _regions.Add(regionModel);
            });
            
            _regions.Sort((a, b) => a.Id - b.Id);
        }

        public List<RegionModel> GetRegions()
        {
            return new(_regions);
        }

        public void SaveRegion(RegionModel regionModel)
        {
            RegionModel existingRegion = _regions.Find(model => model.Id == regionModel.Id);
            
            if (existingRegion == null)
            {
                _regions.Add(regionModel);
                _regionDataSet.Add(new(regionModel));
            }
            else
            {
                _regions.Remove(existingRegion);
                _regions.Add(regionModel);
                _regionDataSet.Clear();
                _regions.ForEach(region => _regionDataSet.Add(new(region)));
            }

            _saveFileHandler.Save(LocalPath, _regionDataSet);
        }
    }

    [Serializable]
    internal class RegionData
    {
        private int _id;
        private int _ownerId;

        public int Id => _id;
        public int OwnerId => _ownerId;

        [JsonConstructor]
        public RegionData(int id, int ownerId)
        {
            _id = id;
            _ownerId = ownerId;
        }

        public RegionData(RegionModel regionModel)
        {
            _id = regionModel.Id;
            _ownerId = regionModel.CurrentOwner.Id;
        }
    }
}