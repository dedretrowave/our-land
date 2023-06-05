using System.Collections.Generic;
using Characters.Model;
using UnityEngine;

namespace Level.Models
{
    public class LevelModel
    {
        private int _reward;
        private int _numberOfPlayerEnemies = 0;
        private List<CharacterModel> _charactersOnLevel;

        public int Reward => _reward;
        public int NumberOfPlayerEnemies => _numberOfPlayerEnemies;
        public List<CharacterModel> CharactersOnLevel => new(_charactersOnLevel);

        public LevelModel(List<CharacterModel> characters, int reward)
        {
            _reward = reward;
            _charactersOnLevel = new(characters);
            
            _charactersOnLevel.ForEach(character =>
            {
                if (character.Fraction == Fraction.Fraction.Enemy)
                {
                    _numberOfPlayerEnemies++;
                }
            });
        }
    }
}