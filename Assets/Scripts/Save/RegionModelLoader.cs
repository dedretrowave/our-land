using System;
using System.Collections.Generic;
using Characters;
using Characters.Model;
using DI;
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
            _saveFileHandler = new SaveFileHandler();

            _regionDataSet = _saveFileHandler.Load<List<RegionData>>(LocalPath) 
                             ?? new();

            if (_regionDataSet.Count == 0) return;

            _regionDataSet.ForEach(region =>
            {
                CharacterModel regionOwner = _characterContainer.GetById(region.OwnerId);
                RegionModel regionModel = new(regionOwner, region.Id);
                
                _regions.Add(regionModel);
            });
        }

        public List<RegionModel> GetRegions()
        {
            return _regions;
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
    internal class RegionDataSet
    {
        private List<RegionData> _data;

        public List<RegionData> Data => _data;

        public RegionDataSet()
        {
            _data = new();
        }

        public void Add(RegionData region)
        {
            
        }
    }

    [Serializable]
    internal class RegionData
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        public RegionData()
        {
            Id = -1;
            OwnerId = -1;
        }

        public RegionData(int id, int ownerId)
        {
            Id = id;
            OwnerId = ownerId;
        }

        public RegionData(RegionModel regionModel)
        {
            Id = regionModel.Id;
            OwnerId = regionModel.CurrentOwner.Id;
        }
        

        // public List<RegionModel> Regions => _regions;
        //
        // public RegionData()
        // {
        //     _regions = new List<RegionModel>();
        // }
        //
        // public RegionModel GetById(int id)
        // {
        //     return _regions.Find(region => region.Id == id);
        // }
        //
        // public void SetRegion(RegionModel regionModel)
        // {
        //
        //     // if (!_regions.Contains(regionModel))
        //     // {
        //     //     _regions.Add(regionModel);
        //     //     return;
        //     // }
        //
        //     RegionModel existingRegionModel = _regions.Find(region => region.Id == regionModel.Id);
        //     int existingRegionModesIndex = _regions.IndexOf(existingRegionModel);
        //
        //     if (existingRegionModesIndex == -1)
        //     {
        //         _regions.Add(regionModel);
        //         return;
        //     }
        //     
        //     _regions[existingRegionModesIndex] = regionModel;
        // }
    }
}