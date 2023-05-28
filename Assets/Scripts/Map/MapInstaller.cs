using System;
using System.Collections.Generic;
using Characters.Base;
using Characters.SO;
using DI;
using Level;
using Region.Models;
using Region.Views;
using UnityEditor;
using UnityEngine;

namespace Map
{
    public class MapInstaller : MonoBehaviour
    {
        [SerializeField] private List<RegionView> _regions;
        
        private CharacterRegionContainer _characterRegionContainer;

        public void Construct(List<RegionModel> models)
        {
            for (var i = 0; i < models.Count; i++)
            {
                RegionView region = _regions[i];
                Character regionOwner = models[i].CurrentOwner;
                
                region.SetVFXByCharacter(regionOwner);
            }
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(MapInstaller), () => this));
        }
    }
}