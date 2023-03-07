using Src.Divisions.Divisions;
using Src.Regions.Combat;
using Src.Regions.Fraction;
using UnityEngine;

namespace Src.Divisions.Defenders.Base
{
    public abstract class Defender : MonoBehaviour
    {
        public abstract void Init(RegionDefence targetRegion, Fraction ownerFraction);
        public abstract void InteractWithEnemy(Division enemy);
    }
}