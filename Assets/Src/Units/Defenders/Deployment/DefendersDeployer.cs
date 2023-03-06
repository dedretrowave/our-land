using Src.Regions;
using Src.Regions.Combat;
using Src.Regions.Fraction;
using Src.Units.Defenders.Base;
using Src.Units.Number;
using UnityEngine;

namespace Src.Units.Defenders.Deployment
{
    public class DefendersDeployer : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private Fraction _fraction;
        
        [Header("Components")]
        [SerializeField] private Defender _defenderPrefab;
        [SerializeField] private Garrison _garrison;

        public void Deploy(Transform regionTransform)
        {
            if (!regionTransform.TryGetComponent(out Region region) || _garrison.Amount == 0) return;

            Defender defender = Instantiate(_defenderPrefab, transform.position, Quaternion.identity);
            _garrison.Decrease();
            defender.Init(region.Defence, _fraction);
        }
    }
}