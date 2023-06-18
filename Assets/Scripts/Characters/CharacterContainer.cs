using System;
using System.Collections.Generic;
using Characters.Model;
using Characters.Skins;
using Characters.Skins.SO;
using Characters.SO;
using DI;
using EventBus;
using UnityEngine;

namespace Characters
{
    public class CharacterContainer : MonoBehaviour
    {
        [SerializeField] private List<CharacterSO> _characterSOs;
        [SerializeField] private CharacterSO _playerSO;
        
        private EventBus.EventBus _eventBus;

        private List<CharacterModel> _characters = new();
        private CharacterModel _playerModel;

        public void Construct()
        {
            _eventBus = EventBus.EventBus.Instance;

            _characterSOs.ForEach(characterSO => Add(new(characterSO)));
            
            DependencyContext.Dependencies.Add(
                new(typeof(CharacterContainer), () => this));

            _playerModel = new(_playerSO);
            
            _eventBus.AddListener<CharacterModel>
                (EventName.ON_PLAYER_MODEL_CREATED, CreatePlayerModel);
        }

        public void Disable()
        {
            _eventBus.RemoveListener<CharacterModel>
                (EventName.ON_PLAYER_MODEL_CREATED, CreatePlayerModel);
        }

        private void CreatePlayerModel(CharacterModel model)
        {
            _playerModel = model;
        }

        public void Add(CharacterModel character)
        {
            if (!_characters.Contains(character))
            {
                _characters.Add(character);
            }
        }

        public CharacterModel GetById(int id)
        {
            return Get(character => character.Id == id);
        }

        public CharacterModel GetByFraction(Fraction.Fraction fraction)
        {
            return Get(character => character.Fraction == fraction);
        }

        private CharacterModel Get(Predicate<CharacterModel> callback)
        {
            CharacterModel enemy = _characters
                .Find(callback);

            return enemy ?? _playerModel;
        }
    }
}