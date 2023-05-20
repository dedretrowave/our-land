using System;
using System.Collections;
using Characters.Base;
using JetBrains.Annotations;
using Level.Region.Models;
using Level.Region.Views;
using UnityEngine;

namespace Level.Region.Presenters
{
    public class RegionPresenter
    {
        private RegionView _view;
        private RegionModel _model;

        public event Action OnGarrisonRelease;

        public RegionPresenter(Character defaultOwner, RegionView view, RegionModel model)
        {
            _view = view;
            _model = model;
            _model.SetOwner(defaultOwner);
        }

        public void Release()
        {
            OnGarrisonRelease?.Invoke();
        }

        public IEnumerator DecreaseContinuously()
        {
            while (_model.Count >= 0)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                TakeDamage(null);
            }
        }

        public IEnumerator IncreaseContinuously()
        {
            yield return new WaitForSeconds(_model.IncreaseRate);
            IncreaseCount();
            yield return IncreaseContinuously();
        }

        public void TakeDamage([CanBeNull] Character attacker)
        {
            try
            {
                UpdateCount(-1);
            }
            catch (Exception)
            {
                if (attacker == null) return;
                
                ChangeOwner(attacker);
            }
        }

        public void IncreaseCount()
        {
            UpdateCount(1);
        }

        private void ChangeOwner(Character newOwner)
        {
            _model.SetOwner(newOwner);
            _view.SetSkin(_model.CurrentOwner.Skin);
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