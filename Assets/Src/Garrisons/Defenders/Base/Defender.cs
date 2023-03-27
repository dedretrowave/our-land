using Src.Garrisons.Divisions;
using Src.Regions.Combat;
using UnityEngine;

namespace Src.Divisions.Defenders.Base
{
    public abstract class Defender : MonoBehaviour
    {
        public abstract void Init(RegionDefence targetRegion, Fraction.Fraction ownerFraction);
        public abstract void InteractWithEnemy(Division enemy);
    }
}