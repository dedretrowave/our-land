using System;
using System.Collections.Generic;
using Characters.Skins.SO;
using DI;
using SkinShop;
using UnityEditor;
using UnityEngine;

namespace Characters.Skins
{
    public class SkinItemsContainer : MonoBehaviour
    {
        [SerializeField] private SkinItemTypeCollectionDictionary _items = new();

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(SkinItemsContainer), () => this));
        }
        
        public SkinItem GetByIdAndType(SkinItemType type, int id)
        {
            return _items[type].GetById(id);
        }
    }
}