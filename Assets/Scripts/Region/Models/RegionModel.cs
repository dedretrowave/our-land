using Characters.Base;

namespace Region.Models
{
    public class RegionModel
    {
        private int _id;
        private Character _currentOwner;
        
        public Character CurrentOwner => _currentOwner;
        public int Id => _id;

        public void SetOwner(Character newOwner)
        {
            _currentOwner = newOwner;
        }

        public RegionModel() { }

        public RegionModel(Character owner, int id = -1)
        {
            _id = id;
            _currentOwner = owner;
        }
    }
}