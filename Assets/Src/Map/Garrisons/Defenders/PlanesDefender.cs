using Src.Map.Fraction;
using Src.Map.Garrisons.Defenders.Base;
using Src.Map.Garrisons.Divisions;
using Src.Map.Garrisons.Divisions.Movement;
using Src.Map.Regions.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Map.Garrisons.Defenders
{
    public class PlanesDefender : Defender
    {
        [Header("Components")]
        [SerializeField] private Movement _movement;
        
        [Header("Events")]
        [SerializeField] private UnityEvent _onTargetRegionReached;

        private RegionDefence _targetRegion;
        private Character _fraction;

        public override void Init(RegionDefence targetRegion, Character ownerFraction)
        {
            _targetRegion = targetRegion;
            _fraction = ownerFraction;
            _movement.ApplyPoint(_targetRegion.transform.position);
        }
        
        public override void InteractWithEnemy(Division enemy)
        {
            Destroy(enemy.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.Equals(_targetRegion.transform)) return;

            if (_fraction == _targetRegion.Fraction)
            {
                _targetRegion.RegisterDefender(this);
                _onTargetRegionReached.Invoke();
            }
            else
            {
                PlanesDefender enemyPlanes = _targetRegion.GetDefenderOfType<PlanesDefender>();

                if (enemyPlanes != null)
                {
                    Destroy(enemyPlanes.gameObject);
                }
                
                Destroy(gameObject);
            }
        }
    }
}