using Src.Regions.Combat;
using Src.Regions.Fraction;
using Src.Units.Divisions;
using UnityEngine;

namespace Src.Units.Defenders.Base
{
    public abstract class Defender : MonoBehaviour
    {
        public abstract void Init(RegionDefence targetRegion, Fraction ownerFraction);
        public abstract void InteractWithEnemy(Division enemy);
    }
}