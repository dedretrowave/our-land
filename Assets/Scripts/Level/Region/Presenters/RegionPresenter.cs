using System;
using System.Collections;
using Characters.Base;
using Level.Region.Models;
using Level.Region.Views;
using UnityEngine;

namespace Level.Region.Presenters
{
    public class RegionPresenter
    {
        private RegionView _view;
        private RegionModel _model;

        public event Action<RegionView> OnSuccessfulRegionTarget; 

        public RegionPresenter(Character defaultOwner, RegionView view, RegionModel model)
        {
            _view = view;
            _model = model;
            ChangeOwner(defaultOwner);
            _view.SetCount(_model.Count);
        }

        public IEnumerator ReleaseGarrison(RegionView target)
        {
            int i = 0;

            int initialCount = _model.Count;

            while (i < initialCount)
            {
                _view.SendDivision(target, _model.CurrentOwner);
                DecreaseCount();
                i++;
                yield return new WaitForSeconds(_model.DivisionSpawnRate);
            }
        }

        public void TryTargetRegion(Transform point)
        {
            if (point.Equals(_view.transform)) return;

            if (point.TryGetComponent(out RegionView target))
            {
                OnSuccessfulRegionTarget?.Invoke(target);
            }
        }

        public IEnumerator IncreaseContinuously()
        {
            yield return new WaitForSeconds(_model.IncreaseRate);
            IncreaseCount();
            yield return IncreaseContinuously();
        }

        public void TakeDamage(Character attacker)
        {
            if (attacker.Equals(_model.CurrentOwner)) return;
            
            try
            {
                DecreaseCount();
            }
            catch (Exception)
            {
                ChangeOwner(attacker);
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

        private void ChangeOwner(Character newOwner)
        {
            _model.SetOwner(newOwner);
            _view.SetSkin(_model.CurrentOwner.Skin);
            _view.SetColor(_model.CurrentOwner.Color);
        }

        private void UpdateCount(int count)
        {
            int changedCount = _model.Count + count;

            if (changedCount < 0)
            {
                throw new Exception("Count is zero");
            }
            
            _model.ChangeCount(count);
            _view.SetCount(_model.Count);
        }
    }
}