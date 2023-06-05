using System.Collections.Generic;
using Characters.Model;
using Characters.View;
using Components;
using DI;
using Level;
using Region.Models;
using Region.Views;
using UnityEngine;

namespace Map
{
    public class MapInstaller : MonoBehaviour
    {
        [SerializeField] private List<RegionView> _regions;
        
        private CharacterRegionContainer _characterRegionContainer;

        public void Construct(List<RegionModel> models)
        {
            for (int i = 0; i < models.Count; i++)
            {
                RegionView region = _regions[i];
                CharacterModel regionOwner = models[i].CurrentOwner;

                CharacterView owner = region.GetComponentInChildren<CharacterView>();

                if (owner != null)
                {
                    owner.SetSkin(regionOwner.Skin);
                }

                if (regionOwner.Fraction == Fraction.Fraction.Enemy)
                {
                    LevelSelector selector = region.GetComponent<LevelSelector>();
                    selector.enabled = true;

                    selector.Construct(models[i]);
                }
                
                region.SetVFXByCharacter(regionOwner);
            }
        }

        private void Awake()
        {
            DependencyContext.Dependencies.Add(new(typeof(MapInstaller), () => this));
        }
    }
}