using Characters;
using Map;
using Player;
using Player.Wallet;
using SkinShop;
using UnityEngine;

namespace Entries
{
    public class LevelSceneEntryPoint : MonoBehaviour
    {
        [SerializeField] private MapInitializer _mapInitializer;
        [SerializeField] private CharacterContainer _characterContainer;

        [SerializeField] private SkinShopInstaller _skinShop;
        [SerializeField] private PlayerInstaller _player;
        [SerializeField] private WalletInstaller _walletInstaller;

        private void Start()
        {
            _characterContainer.Construct();
            _player.Construct();
            _walletInstaller.Construct();
            _mapInitializer.Construct();
            _skinShop.Construct(_player.GetSkin());
        }

        private void OnDisable()
        {
            _characterContainer.Disable();
            _player.Disable();
            _mapInitializer.Disable();
            _skinShop.Disable();
        }
    }
}