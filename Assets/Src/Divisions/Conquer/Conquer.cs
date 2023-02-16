using System.Collections;
using Src.Divisions.Number;
using Src.Regions;
using Src.Regions.RegionCombat;
using UnityEngine;

namespace Src.Divisions.Conquer
{
    public class Conquer : MonoBehaviour
    {
        [SerializeField] private RegionOwner _owner;
        [SerializeField] private float _captureTickTime;
        [SerializeField] private DivisionNumber _division; 

        private Region _conquestRegion;
        private Coroutine _conquerRoutine;

        public void SetDependencies(Region region, RegionCombatZone combatZone)
        {
            _conquestRegion = region;
            _conquestRegion.SetNewClaimer(_owner.Fraction);
            combatZone.OnDefenceDivisionsDefeated.AddListener(StartConquest);
            combatZone.OnDefenceDivisionsRestored.AddListener(StopConquest);
        }

        private void StartConquest()
        {
            _conquerRoutine = StartCoroutine(ConquerRegion());
        }

        private void StopConquest()
        {
            StopCoroutine(_conquerRoutine);
        }

        private IEnumerator ConquerRegion()
        {
            _conquestRegion.TakeDamage();

            yield return new WaitForSeconds(_captureTickTime / _division.Number);

            yield return ConquerRegion();
        }
    }
}