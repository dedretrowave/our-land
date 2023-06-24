using System;
using Characters;
using Map;
using Misc.Music;
using UnityEngine;

namespace Entries
{
    public class MainMenuSceneEntry : MonoBehaviour
    {
        [SerializeField] private MapInitializer _mapInitializer;
        [SerializeField] private MusicInstaller _musicInstaller;
        [SerializeField] private CharacterContainer _characterContainer;

        private void Start()
        {
            _characterContainer.Construct();
            _musicInstaller.Construct();
            _mapInitializer.Construct();
        }

        private void OnDisable()
        {
            _characterContainer.Disable();
            _musicInstaller.Disable();
            _mapInitializer.Disable();
        }
    }
}