using System.Collections.Generic;
using Characters.Base;

namespace Level.Models
{
    public class LevelModel
    {
        private int _numberOfPlayerEnemies;
        private List<Character> _charactersOnLevel;

        public int NumberOfPlayerEnemies => _numberOfPlayerEnemies;
        public List<Character> CharactersOnLevel => new(_charactersOnLevel);

        public LevelModel(List<Character> characters)
        {
            _charactersOnLevel = new(characters);
            
            _charactersOnLevel.ForEach(character =>
            {
                if (character.Fraction == Fraction.Fraction.Enemy)
                {
                    _numberOfPlayerEnemies++;
                }
            });
        }

        public void AddCharacter(Character character)
        {
            _charactersOnLevel.Add(character);

            if (character.Fraction == Fraction.Fraction.Enemy)
            {
                _numberOfPlayerEnemies++;
            }
        }
    }
}