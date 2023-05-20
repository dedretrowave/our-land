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

        public FractionSkinHolder GetSkinByFraction(Character fraction)
        {
            return null;
            // _fractions.Find(item => item.Fraction.Equals(fraction)).SkinHolder;
        }

        public Character GetFractionById(int id)
        {
            return null;
            // _fractions.Find(item => item.Fraction.Id == id).Fraction;
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
        // public Map.Fraction.Fraction Fraction;
    }
}