using System;
using System.Collections;
using Characters.Base;
using Components.Division;
using Level.Region.Models;
using Level.Region.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Level.Region.Presenters
{
    public class RegionPresenter
    {
        private GarrisonView _garrisonView;
        private RegionView _regionView;
        private RegionModel _model;

        public event Action<GarrisonView> OnSuccessfulRegionTarget;
        public event Action<Character, Character> OnOwnerChange;

        public RegionPresenter(Character defaultOwner, RegionView regionView, GarrisonView garrisonView, RegionModel model)
        {
            _garrisonView = garrisonView;
            _regionView = regionView;
            _model = model;
            ChangeOwner(defaultOwner);
            _garrisonView.SetCount(_model.Count);
        }

        public IEnumerator ReleaseGarrison(GarrisonView target)
        {
            int i = 0;

            int initialCount = _model.Count;

            while (i < initialCount)
            {
                _garrisonView.SendDivision(target, _model.CurrentOwner);
                DecreaseCount();
                i++;
                yield return new WaitForSeconds(_model.DivisionSpawnRate);
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
            Character oldOwner = _model.CurrentOwner;
            _model.SetOwner(newOwner);
            _regionView.SetSkin(_model.CurrentOwner.Skin);
            _regionView.SetColor(_model.CurrentOwner.Color);
            OnOwnerChange?.Invoke(oldOwner, _model.CurrentOwner);
        }

        public IEnumerator IncreaseContinuously()
        {
            yield return new WaitForSeconds(_model.IncreaseRate);
            IncreaseCount();
            yield return IncreaseContinuously();
        }

        public void TakeDamage(Division attacker)
        {
            if (!attacker.Target.Equals(_garrisonView)) return;
            
            Object.Destroy(attacker.gameObject);
            
            if (attacker.Owner.Equals(_model.CurrentOwner))
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
            int changedCount = _model.Count + count;

            if (changedCount < 0)
            {
                throw new Exception("Count is zero");
            }
            
            _model.ChangeCount(count);
            _garrisonView.SetCount(_model.Count);
        }
    }
}