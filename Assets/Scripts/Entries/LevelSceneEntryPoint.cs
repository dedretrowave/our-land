using System.Collections.Generic;
using Characters;
using DI;
using Map;
using Save;
using UnityEngine;

namespace Entries
{
    public class LevelSceneEntryPoint : MonoBehaviour
    {
        [SerializeField] private MapInitializer _mapInitializer;
        
        [Header("Containers")]
        private CharacterContainer _characterContainer;
        
        [Header("Loaders")]
        private RegionModelLoader _regionModelLoader;
        private CharactersModelLoader _charactersModelLoader;

        private void Start()
        {
            _characterContainer = DependencyContext.Dependencies.Get<CharacterContainer>();
            _characterContainer.Construct();
            
            _regionModelLoader = new();
            _charactersModelLoader = new();

            List<CharacterData> characterData = _charactersModelLoader.GetData();

            if (characterData.Count > 0)
            {
                _characterContainer.CreateModelsFromData(characterData);
            }

            _mapInitializer.Construct();
        }

        private void OnDisable()
        {
            _mapInitializer.Disable();
        }
    }
}