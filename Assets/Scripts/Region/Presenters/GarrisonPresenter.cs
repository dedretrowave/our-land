using System;
using System.Collections;
using Characters.Model;
using Components.Division;
using Region.Models;
using Region.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Region.Presenters
{
    public class GarrisonPresenter
    {
        private GarrisonView _garrisonView;

        private GarrisonModel _garrisonModel;
        private RegionModel _regionModel;

        public event Action OnDamageTaken;
        public event Action OnGarrisonIsZero;
        public event Action<CharacterModel> OnOwnerChangePossible; 
        public event Action<GarrisonView> OnSuccessfulRegionTarget; 

        public GarrisonPresenter(GarrisonView garrisonView,
            GarrisonModel garrisonModel,
            RegionModel regionModel)
        {
            _garrisonView = garrisonView;
            _garrisonModel = garrisonModel;
            _regionModel = regionModel;
            
            _garrisonView.SetCount(_garrisonModel.Count);
        }
        
        public void TryTargetRegion(Transform point)
        {
            if (point.Equals(_garrisonView.transform)) return;

            if (point.TryGetComponent(out GarrisonView target))
            {
                OnSuccessfulRegionTarget?.Invoke(target);
            }
        }
        
        public IEnumerator ReleaseGarrison(GarrisonView target)
        {
            int i = 0;

            int initialCount = _garrisonModel.Count;

            while (i < initialCount)
            {
                _garrisonView.SendDivision(target, _regionModel.CurrentOwner);

                try
                {
                    DecreaseCount();
                }
                catch (Exception)
                {
                    //ignored
                }
                
                i++;
                yield return new WaitForSeconds(_garrisonModel.DivisionSpawnRate);
            }
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
                    OnDamageTaken?.Invoke();
                }
                catch (GarrisonIsZeroException) 
                {
                    OnGarrisonIsZero?.Invoke();
                    OnOwnerChangePossible?.Invoke(attacker.Owner);
                }
            }
        }

        private void DecreaseCount()
        {
            UpdateCount(-1);
        }

        private void IncreaseCount()
        {
            UpdateCount(1);
        }

        private void UpdateCount(int count)
        {
            int changedCount = _garrisonModel.Count + count;

            if (changedCount < 0)
            {
                throw new GarrisonIsZeroException();
            }
            
            _garrisonModel.ChangeCount(count);
            _garrisonView.SetCount(_garrisonModel.Count);
        }
    }
}