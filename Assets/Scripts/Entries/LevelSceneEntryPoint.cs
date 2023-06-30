using Characters;
using Characters.Skins;
using Components.Music;
using DI;
using EventBus;
using Map;
using Player;
using Player.Wallet;
using Save;
using SkinShop;
using UnityEngine;

namespace Entries
{
    public class LevelSceneEntryPoint : MonoBehaviour
    {
        private EventBus.EventBus _eventBus;

        [SerializeField] private SaveFileHandler _saveFileHandler;
        
        [SerializeField] private MapInitializer _mapInitializer;
        [SerializeField] private MusicInstaller _musicInstaller;
        [SerializeField] private CharacterContainer _characterContainer;
        [SerializeField] private SkinItemsContainer _skinItemsContainer;

        [SerializeField] private SkinShopInstaller _skinShop;
        [SerializeField] private PlayerInstaller _player;
        [SerializeField] private WalletInstaller _walletInstaller;

        private void Start()
        {
            _eventBus = EventBus.EventBus.Instance;
            
            DependencyContext.Dependencies.Add(new(typeof(SaveFileHandler), () => _saveFileHandler));
            
            _musicInstaller.Construct();
            _characterContainer.Construct();
            _skinItemsContainer.Construct();

            _player.Construct();
            _walletInstaller.Construct();
            _mapInitializer.Construct();
            _skinShop.Construct(_player.GetSkin());
            
            _eventBus.TriggerEvent(EventName.ON_MAP_OPENED);
        }

        private void OnDisable()
        {
            _musicInstaller.Disable();
            _characterContainer.Disable();
            _skinItemsContainer.Disable();
            _player.Disable();
            _mapInitializer.Disable();
            _skinShop.Disable();
        }
    }
}