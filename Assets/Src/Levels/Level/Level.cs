using System.Collections.Generic;
using DI;
using Src.Enemy;
using Src.Levels.Level.Initialization;
using Src.Levels.Level.UI;
using Src.Map.Fraction;
using Src.Map.Regions;
using Src.Map.Regions.Containers;
using Src.Saves;
using Src.SkinShop.Skin;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Src.Levels.Level
{
    public class Level : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private LevelReward _reward;
        [SerializeField] private LevelSpawner _spawner;
        [SerializeField] private LevelUI _ui;
        
        [Header("Parameters")]
        [SerializeField] private int _id;
        [SerializeField] private Character _owner;
        
        [Header("Containers")]
        [SerializeField] private RegionContainer _playerContainer;
        [SerializeField] private EnemyAI _enemy;
        [SerializeField] private List<RegionContainer> _enemyContainers;

        [Header("Events")]
        [SerializeField] private UnityEvent<Level> _onFinish = new();
        [SerializeField] private UnityEvent<Level> _onOwnerChange = new();

        private Character _originalOwner;

        private PlayerDataSaveSystem _save;

        private bool _isControlledByPlayer;
        
        public LevelReward Reward => _reward;
        public Character Owner => _owner;

        public bool IsControlledByPlayer
        {
            get
            {
                if (_playerContainer.Owner == null) return false;
                
                _isControlledByPlayer = _owner.Equals(_playerContainer.Owner.Fraction);

                return _isControlledByPlayer;
            }
        }

        public void Init()
        {
            BindEvents();
            _spawner.Spawn();
            _ui.Hide();
        }

        public void SetRandomOwnerBesidesPlayer()
        {
            Character newOwner = _enemyContainers[Random.Range(0, _enemyContainers.Count)].Owner.Fraction;
            SetOwner(newOwner);
        }

        public void BindEvents()
        {
            _playerContainer.OnEmpty.AddListener(Fail);
            _enemy.OnGiveUp.AddListener(Complete);
        }

        private void Start()
        {
            _originalOwner = _owner;
            
            _save = DependencyContext.Dependencies.Get<PlayerDataSaveSystem>();

            LevelData data = _save.GetLevelById(_id);
            
            if (data != null)
            {
                FractionContainer fractionContainer = DependencyContext.Dependencies.Get<FractionContainer>();

                _owner = fractionContainer.GetFractionById(data.OwnerId);
            }

            _onOwnerChange.Invoke(this);
        }

        private void Fail()
        {
            Finish(_originalOwner);
        }

        private void Complete()
        {
            // Finish(_playerContainer.Owner.Fraction);
        }

        private void Finish(Character newOwner)
        {
            // SetOwner(newOwner);

            _onFinish.Invoke(this);
            
            _playerContainer.Clear();
            _enemy.OnGiveUp.RemoveListener(Complete);
        }

        private void SetOwner(Character newOwner)
        {
            _owner = newOwner;
            // _save.SaveLevel(new LevelData(_id, _owner.Id));
            _onOwnerChange.Invoke(this);
        }
    }
}