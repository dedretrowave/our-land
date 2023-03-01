using Src.Regions.Combat;
using Src.Units.Divisions;
using UnityEngine;

namespace Src.Units.Defenders.Base
{
    public abstract class Defender : MonoBehaviour
    {
        public abstract void Init(RegionDefence defence);
        public abstract void InteractWithEnemy(Division enemy);
    }
}