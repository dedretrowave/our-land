using Src.Regions.Combat;
using Src.Units.Defenders.Base;
using UnityEngine;

namespace Src.Units.Defenders
{
    public class DefendersDeployer : MonoBehaviour
    {
        [SerializeField] private Defender _defenderPrefab;

        private bool _isDragging;

        public void StartDragging()
        {
            _isDragging = true;
        }

        public void Deploy(Transform regionTransform)
        {
            if (!_isDragging) return;
            
            if (!regionTransform.TryGetComponent(out RegionDefence defence)) return;

            Defender defender = Instantiate(_defenderPrefab, transform.position, Quaternion.identity);
            defender.Init(defence);

            _isDragging = false;
        }
    }
}