using Src.Map.Fraction;
using Src.Map.Garrisons.Defenders.Base;
using Src.Map.Regions;
using UnityEngine;

namespace Src.Map.Garrisons.Defenders.Deployment
{
    public class DefendersDeployer : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private Character _fraction;
        
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