using System;
using System.Collections;
using Characters.Base;
using Components.Division;
using Region.Models;
using Region.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Region.Presenters
{
    public class RegionPresenter
    {
        private GarrisonView _garrisonView;
        private RegionView _regionView;
        private GarrisonModel _garrisonModel;
        private RegionModel _regionModel;

        public event Action<GarrisonView> OnSuccessfulRegionTarget;
        public event Action<Character, Character> OnOwnerChange;

        public RegionPresenter(
            Character defaultOwner,
            RegionView regionView,
            GarrisonView garrisonView,
            RegionModel regionModel,
            GarrisonModel garrisonModel)
        {
            _garrisonView = garrisonView;
            _garrisonModel = garrisonModel;
            _regionView = regionView;
            _regionModel = regionModel;
            ChangeOwner(defaultOwner);
            _garrisonView.SetCount(_garrisonModel.Count);
        }

        public IEnumerator ReleaseGarrison(GarrisonView target)
        {
            int i = 0;

            int initialCount = _garrisonModel.Count;

            while (i < initialCount)
            {
                _garrisonView.SendDivision(target, _regionModel.CurrentOwner);
                DecreaseCount();
                i++;
                yield return new WaitForSeconds(_garrisonModel.DivisionSpawnRate);
            }
        }

        public void TryTargetRegion(Transform point)
        {
            if (point.Equals(_garrisonView.transform)) return;

            if (point.TryGetComponent(out GarrisonView target))
            {
                OnSuccessfulRegionTarget?.Invoke(target);
            }
        }
        
        private void ChangeOwner(Character newOwner)
        {
            Character oldOwner = _regionModel.CurrentOwner;
            _regionModel.SetOwner(newOwner);
            _regionView.SetSkin(_regionModel.CurrentOwner.Skin);
            _regionView.SetColor(_regionModel.CurrentOwner.Color);
            OnOwnerChange?.Invoke(oldOwner, _regionModel.CurrentOwner);
        }

        public IEnumerator IncreaseContinuously()
        {
            yield return new WaitForSeconds(_garrisonModel.IncreaseRate);
            IncreaseCount();
            yield return IncreaseContinuously();
        }

        public void TakeDamage(Division attacker)
        {
            if (!attacker.Target.Equals(_garrisonView)) return;
            
            Object.Destroy(attacker.gameObject);
            
            if (attacker.Owner.Equals(_regionModel.CurrentOwner))
            {
                IncreaseCount();
            }
            else
            {
                try
                {
                    DecreaseCount();
                    _regionView.PlayHurt();
                }
                catch (Exception)
                {
                    ChangeOwner(attacker.Owner);
                }
            }
        }

        public void DecreaseCount()
        {
            UpdateCount(-1);
        }

        public void IncreaseCount()
        {
            UpdateCount(1);
        }

        private void UpdateCount(int count)
        {
            int changedCount = _garrisonModel.Count + count;

            if (changedCount < 0)
            {
                throw new Exception("Count is zero");
            }
            
            _garrisonModel.ChangeCount(count);
            _garrisonView.SetCount(_garrisonModel.Count);
        }
    }
}