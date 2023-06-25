using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Model;
using DI;
using Level.Models;
using Region.Views;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level.Enemy
{
    public class EnemyAI
    {
        private CharacterModel _character;
        private CharacterRegionContainer _characterRegionContainer;
        private LevelModel _levelModel;

        private float _minAttackDelay = 6f;
        private float _maxAttackDelay = 12f;

        private GarrisonView _targetRegion;
        private GarrisonView _attackStartRegion;

        public EnemyAI(CharacterModel character, LevelModel model)
        {
            _levelModel = model;
            _character = character;

            _characterRegionContainer =
                DependencyContext.Dependencies.Get<CharacterRegionContainer>();
        }

        public IEnumerator StartAttacking()
        {
            float waitTime = Random.Range(_minAttackDelay, _maxAttackDelay);

            yield return new WaitForSeconds(waitTime);
            
            PickTargetRegion();
            PickAttackStartRegion();
            
            _attackStartRegion.Release(_targetRegion.transform);

            yield return StartAttacking();
        }

        private void PickTargetRegion()
        {
            CharacterModel enemy = PickRandomEnemy();

            _targetRegion = PickRandomRegion(enemy);
        }

        private void PickAttackStartRegion()
        {
            _attackStartRegion = PickRandomRegion(_character);
        }

        private GarrisonView PickRandomRegion(CharacterModel character)
        {
            List<RegionView> regions = _characterRegionContainer.GetRegionsByCharacter(character);

            GarrisonView region = null;

            try
            {
                region = regions[Random.Range(0, regions.Count - 1)].GetComponent<GarrisonView>();
            }
            catch (Exception)
            {
                PickRandomRegion(PickRandomEnemy());
            }

            return region;
        }

        private CharacterModel PickRandomEnemy()
        {
            CharacterModel randomEnemy =
                _levelModel.CharactersOnLevel[Random.Range(0, _levelModel.CharactersOnLevel.Count - 1)];

            if (randomEnemy.Equals(_character))
                PickRandomEnemy();

            return randomEnemy;
        }
    }
}