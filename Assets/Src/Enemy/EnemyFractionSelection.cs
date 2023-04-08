using System.Collections.Generic;
using Src.Map.Fraction;
using Src.Map.Regions.Containers;
using UnityEngine;

namespace Src.Enemy
{
    public class EnemyFractionSelection : MonoBehaviour
    {
        [SerializeField] private RegionContainer _container;
        [SerializeField] private EnemyAI _ai;
        
        [Header("Character")]
        [SerializeField] private List<Character> _characters;

        private void Start()
        {
            
        }
    }
}