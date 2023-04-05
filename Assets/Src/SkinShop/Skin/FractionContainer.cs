using System;
using System.Collections.Generic;
using Src.DI;
using Src.Map.Fraction;
using Src.SerializableDictionary.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Src.SkinShop.Skin
{
    public class FractionContainer : MonoBehaviour
    {
        [SerializeField] private List<FractionData> _fractions;

        public FractionSkinHolder GetSkinByFraction(Fraction fraction)
        {
            return _fractions.Find(item => item.Fraction.Equals(fraction)).SkinHolder;
        }

        public Fraction GetFractionById(int id)
        {
            return _fractions.Find(item => item.Id == id).Fraction;
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(typeof(FractionContainer), () => this);
        }
    }

    [Serializable]
    internal class FractionData
    {
        public FractionSkinHolder SkinHolder;
        public int Id;
        public Fraction Fraction;
    }
}