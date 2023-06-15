using System.Collections.Generic;
using Characters;
using DI;
using Map;
using Player;
using Save;
using SkinShop;
using UnityEngine;

namespace Entries
{
    public class LevelSceneEntryPoint : MonoBehaviour
    {
        [SerializeField] private MapInitializer _mapInitializer;

        [SerializeField] private SkinShopInstaller _skinShop;

        [SerializeField] private PlayerInstaller _player;

        private void Start()
        {
            _player.Construct();
            // _mapInitializer.Construct();
            _skinShop.Construct(_player.GetSkin());
        }

        private void OnDisable()
        {
            _player.Disable();
            // _mapInitializer.Disable();
            _skinShop.Disable();
        }
    }
}