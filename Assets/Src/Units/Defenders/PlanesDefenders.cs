using Src.Regions.Combat;
using Src.Units.Defenders.Base;
using Src.Units.Divisions;
using UnityEngine;

namespace Src.Units.Defenders
{
    public class PlanesDefenders : Defender
    {
        [SerializeField] private Movement.Movement _movement;
        
        private RegionDefence _defendedRegion;

        public override void Init(RegionDefence defence)
        {
            _defendedRegion = defence;
            _movement.ApplyPoint(_defendedRegion.transform.position);
        }
        
        public override void InteractWithEnemy(Division enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}