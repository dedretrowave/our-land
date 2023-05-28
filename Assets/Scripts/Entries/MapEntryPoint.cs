using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Base;
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
        
        private List<RegionModel> _regionModels = new();

        private MapInstaller _mapInstaller;
        
        private void Start()
        {
            // TODO: Load RegionModels from Save
            
            if (_regionModels.Count == 0)
            {
                CreateDefaultModels();
            }
            
            _mapInstaller = DependencyContext.Dependencies.Get<MapInstaller>();
            
            _mapInstaller.Construct(_regionModels);
        }

        private void CreateDefaultModels()
        {
            foreach (RegionModel newModel in _regionCharacterSODefault
                         .Select(regionCharacterSO 
                             => new Character(regionCharacterSO.Value))
                         .Select(owner 
                             => new RegionModel(owner)))
            {
                _regionModels.Add(newModel);
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