using Characters;
using Characters.Skins;
using Components.Music;
using EventBus;
using Map;
using Player;
using Player.Wallet;
using SkinShop;
using UnityEngine;

namespace Entries
{
    public class LevelSceneEntryPoint : MonoBehaviour
    {
        private EventBus.EventBus _eventBus;
        
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