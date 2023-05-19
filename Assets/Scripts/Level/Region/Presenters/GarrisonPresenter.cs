using System;
using System.Collections;
using Level.Region.Models;
using Level.Region.Views;
using UnityEngine;

namespace Level.Region.Presenters
{
    public class GarrisonPresenter
    {
        private Fraction.Fraction _fraction;
        private GarrisonView _view;
        private GarrisonModel _model;

        public event Action OnGarrisonRelease;

        public GarrisonPresenter(Fraction.Fraction fraction, GarrisonView view, GarrisonModel model)
        {
            _fraction = fraction;
            _view = view;
            _model = model;
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
                TakeDamage();
            }
        }

        public IEnumerator IncreaseContinuously()
        {
            yield return new WaitForSeconds(_model.IncreaseRate);
            IncreaseCount();
            yield return IncreaseContinuously();
        }

        public void TakeDamage()
        {
            UpdateCount(-1);
        }

        public void IncreaseCount()
        {
            UpdateCount(1);
        }

        private void UpdateCount(int count)
        {
            _model.ChangeCount(count);
            _view.SetCount(_model.Count);
        }
        
        
    }
}