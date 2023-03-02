using Src.Regions.Combat;
using Src.Units.Base;
using Src.Units.Defenders.Base;
using UnityEngine;

namespace Src.Units.Defenders.Deployment
{
    public class DefendersDeployer : MonoBehaviour
    {
        [SerializeField] private Defender _defenderPrefab;
        [SerializeField] private Garrison _garrison;

        public void Deploy(Transform regionTransform)
        {
            if (!regionTransform.TryGetComponent(out RegionDefence defence)) return;

            Defender defender = Instantiate(_defenderPrefab, transform.position, Quaternion.identity);
            _garrison.DecreaseByOne();
            defender.Init(defence);
        }
    }
}