using Src.Regions;
using Src.Regions.Fraction;
using UnityEngine;

namespace Src.Models.Region
{
    [CreateAssetMenu(fileName = "Region", menuName = "Level/Region", order = 0)]
    public class RegionData : ScriptableObject
    {
        [Header("Prefab")]
        [SerializeField] private Regions.Region _regionPrefab;

        [Header("InitialParameters")]
        [SerializeField] private Vector3 _position;
        [SerializeField] private int _garrisonInitialNumber;
        [SerializeField] private int _generationRate;
        [SerializeField] private Fraction _fraction;

        [Header("Models")]
        [SerializeField] private Sprite _image;

        public Regions.Region RegionPrefab => _regionPrefab;
        public Vector3 Position => _position;
        public int GarrisonInitialNumber => _garrisonInitialNumber;
        public int GenerationRate => _generationRate;
        public Fraction Fraction => _fraction;
        public Sprite Image => _image;
    }
}