using Characters;
using Characters.Skins;
using Components.Music;
using DI;
using Map;
using Player;
using Save;
using UnityEngine;

namespace Entries
{
    public class MainMenuSceneEntry : MonoBehaviour
    {
        [SerializeField] private SaveFileHandler _saveFileHandler;
        
        [SerializeField] private CharacterContainer _characterContainer;
        [SerializeField] private SkinItemsContainer _skinItemsContainer;
        
        [SerializeField] private MusicInstaller _musicInstaller;
        [SerializeField] private MapInitializer _mapInitializer;
        [SerializeField] private PlayerInstaller _playerInstaller;

        private void Start()
        {
            DependencyContext.Dependencies.Add(new(typeof(SaveFileHandler), () => _saveFileHandler));
            
            _characterContainer.Construct();
            _skinItemsContainer.Construct();
            _musicInstaller.Construct();
            _mapInitializer.Construct();
            _playerInstaller.Construct();
        }

        private void OnDisable()
        {
            _characterContainer.Disable();
            _skinItemsContainer.Disable();
            _musicInstaller.Disable();
            _mapInitializer.Disable();
            _playerInstaller.Disable();
        }
    }
}