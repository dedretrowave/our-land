using System.Collections.Generic;
using Src.DI;
using Src.Levels.Level.Initialization;
using Src.Levels.Level.UI;
using Src.Map.Fraction;
using Src.Map.Regions.Containers;
using Src.Saves;
using Src.SkinShop.Skin;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] private Fraction _owner;
        
        [Header("Containers")]
        [SerializeField] private RegionContainer _playerContainer;
        [SerializeField] private List<RegionContainer> _enemyContainers;

        [Header("Events")]
        [SerializeField] private UnityEvent<Level> _onFinish = new();
        [SerializeField] private UnityEvent<Level> _onStatusChange = new();

        private Fraction _originalOwner;
        private int _defeatedEnemies;
        
        private PlayerDataSaveSystem _save;
        
        public LevelReward Reward => _reward;
        public Fraction Owner => _owner;

        public bool IsControlledByPlayer
        {
            get
            {
                if (_playerContainer.Owner == null) return false;

                return _owner.Equals(_playerContainer.Owner.Fraction);
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
            // Fraction newOwner = _enemyContainers[Random.Range(0, _enemyContainers.Count)].Owner.Fraction;
            // SetOwner(newOwner);
        }

        public void BindEvents()
        {
            _playerContainer.OnEmpty.AddListener(Fail);
            _enemyContainers.ForEach(enemy =>
            {
                enemy.OnEmpty.AddListener(ProceedToCompletion);
            });
        }

        private void ClearContainers()
        {
            _playerContainer.Clear();
            _enemyContainers.ForEach(container => container.Clear());
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

            _onStatusChange.Invoke(this);
        }

        private void ProceedToCompletion()
        {
            _defeatedEnemies++;

            if (_defeatedEnemies >= _enemyContainers.Count)
            {
                Complete();
            }
        }

        private void Fail()
        {
            Finish(_originalOwner);
        }

        private void Complete()
        {
            Finish(_playerContainer.Owner.Fraction);
        }

        private void Finish(Fraction newOwner)
        {
            SetOwner(newOwner);

            _onFinish.Invoke(this);
            
            ClearContainers();
        }

        private void SetOwner(Fraction newOwner)
        {
            _owner = newOwner;
            _save.SaveLevel(new LevelData(_id, _owner.Id));
            _onStatusChange.Invoke(this);
        }
    }
}