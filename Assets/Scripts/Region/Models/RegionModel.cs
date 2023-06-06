using System;
using Characters.Model;

namespace Region.Models
{
    public class RegionModel
    {
        private int _id;
        private CharacterModel _currentOwner;
        
        public CharacterModel CurrentOwner => _currentOwner;
        public int Id => _id;

        public void SetOwner(CharacterModel newOwner)
        {
            _currentOwner = newOwner;
        }

        public RegionModel() { }

        public RegionModel(CharacterModel owner, int id = -1)
        {
            _id = id;
            _currentOwner = owner;
        }
    }
}