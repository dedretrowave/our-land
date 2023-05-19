using System;
using System.Collections.Generic;
using DI;
using Src.Map.Fraction;
using UnityEngine;

namespace Src.SkinShop.Skin
{
    public class FractionContainer : MonoBehaviour
    {
        [SerializeField] private List<FractionData> _fractions;

        public FractionSkinHolder GetSkinByFraction(Map.Fraction.Fraction fraction)
        {
            return _fractions.Find(item => item.Fraction.Equals(fraction)).SkinHolder;
        }

        public Map.Fraction.Fraction GetFractionById(int id)
        {
            return _fractions.Find(item => item.Fraction.Id == id).Fraction;
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
        public Map.Fraction.Fraction Fraction;
    }
}