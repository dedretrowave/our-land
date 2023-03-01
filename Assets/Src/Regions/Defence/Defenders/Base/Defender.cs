using Src.Divisions;
using UnityEngine;

namespace Src.Regions.Defence.Defenders.Base
{
    public abstract class Defender : MonoBehaviour
    {
        public abstract void InteractWithEnemy(Division enemy);
    }
}