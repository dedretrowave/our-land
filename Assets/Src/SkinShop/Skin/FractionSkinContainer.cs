using System;
using Src.DI;
using Src.Map.Fraction;
using Src.SerializableDictionary.Editor;
using UnityEditor;
using UnityEngine;

namespace Src.SkinShop.Skin
{
    public class FractionSkinContainer : MonoBehaviour
    {
        [SerializeField] private SkinToFraction _skinToFraction;

        public FractionSkin GetSkinByFraction(Fraction fraction)
        {
            return _skinToFraction[fraction];
        }

        private void Start()
        {
            DependencyContext.Dependencies.Add(typeof(FractionSkinContainer), () => this);
        }
    }
    
    [Serializable]
    internal class SkinToFraction : SerializableDictionary<Fraction, FractionSkin> {}
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SkinToFraction))]
    internal class SkinToFractionUI : SerializableDictionaryPropertyDrawer {}
#endif
}