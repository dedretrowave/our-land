using System.Collections.Generic;
using UnityEngine;

namespace Src.Divisions.Divisions.VFX
{
    public class UnitsAmountVFX : MonoBehaviour
    {
        [SerializeField] private GameObject _tankMesh;
        [SerializeField] private List<Transform> _places;

        public void Init(Division division)
        {
            switch (division.Amount)
            {
                case <5:
                    Spawn(1);
                    break;
                case <10:
                    Spawn(3);
                    break;
                case <15:
                    Spawn(6);
                    break;
                default:
                    Spawn(6);
                    return;
            }
        }

        private void Spawn(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(_tankMesh, _places[i].transform);
            }
        }
    }
}