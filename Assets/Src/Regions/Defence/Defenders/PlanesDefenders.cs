using Src.Divisions;
using Src.Regions.Defence.Defenders.Base;

namespace Src.Regions.Defence.Defenders
{
    public class PlanesDefenders : Defender
    {
        private RegionCombatZone _defendedRegion;

        public void Init(RegionCombatZone region)
        {
            _defendedRegion = region;
        }
        
        public override void InteractWithEnemy(Division enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}