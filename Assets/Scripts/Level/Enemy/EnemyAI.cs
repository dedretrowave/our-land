using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Base;
using DI;
using Level.Models;
using Level.Region.Views;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level.Enemy
{
    public class EnemyAI
    {
        private Character _character;
        private CharacterRegionContainer _characterRegionContainer;
        private LevelModel _levelModel;

        private float _minAttackDelay = 6f;
        private float _maxAttackDelay = 12f;

        private RegionView _targetRegion;
        private RegionView _attackStartRegion;

        public EnemyAI(Character character, LevelModel model)
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
            Character enemy = PickRandomEnemy();

            _targetRegion = PickRandomRegion(enemy);
        }

        private void PickAttackStartRegion()
        {
            _attackStartRegion = PickRandomRegion(_character);
        }

        private RegionView PickRandomRegion(Character character)
        {
            List<RegionView> regions = _characterRegionContainer.GetRegionsByCharacter(character);

            RegionView region = null;

            try
            {
                region = regions[Random.Range(0, regions.Count - 1)];
            }
            catch (ArgumentOutOfRangeException)
            {
                PickRandomRegion(character);
            }

            return region;
        }

        private Character PickRandomEnemy()
        {
            Character randomEnemy =
                _levelModel.CharactersOnLevel[Random.Range(0, _levelModel.CharactersOnLevel.Count - 1)];

            if (randomEnemy.Equals(_character))
                PickRandomEnemy();

            return randomEnemy;
        }
    }
}