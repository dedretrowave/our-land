using Src.Map.Fraction;
using Src.Map.Garrisons.Divisions;
using Src.Map.Regions.Combat;
using UnityEngine;

namespace Src.Map.Garrisons.Defenders.Base
{
    public abstract class Defender : MonoBehaviour
    {
        public abstract void Init(RegionDefence targetRegion, Character ownerFraction);
        public abstract void InteractWithEnemy(Division enemy);
    }
}