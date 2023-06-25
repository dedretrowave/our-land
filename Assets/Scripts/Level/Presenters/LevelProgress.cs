using System;
using Characters.Model;
using Level.Models;
using UnityEngine;

namespace Level.Presenters
{
    public class LevelProgress
    {
        private LevelStatus _status = LevelStatus.InProgress;

        private int _numberOfEnemies;

        public event Action<LevelStatus> OnStatusChange;
        public event Action<LevelStatus> OnEndWithStatus; 

        public LevelProgress(LevelModel model)
        {
            _numberOfEnemies = model.NumberOfPlayerEnemies;
        }

        public void ChangeStatusAfterCharacterLost(CharacterModel character)
        {
            switch (character.Fraction)
            {
                case Fraction.Fraction.Player:
                    SetLose();
                    break;
                case Fraction.Fraction.Enemy:
                    ProceedToSuccess();
                    break;
                case Fraction.Fraction.Neutral:
                default:
                    SetStatus(LevelStatus.InProgress);
                    break;
            }
        }

        private void ProceedToSuccess()
        {
            _numberOfEnemies--;
            
            if (_numberOfEnemies == 0)
            {
                SetWin();
            }
        }

        private void SetLose()
        {
            SetStatus(LevelStatus.Lose);
            OnEndWithStatus?.Invoke(_status);
        }

        private void SetWin()
        {
            SetStatus(LevelStatus.Win);
            OnEndWithStatus?.Invoke(_status);
        }

        private void SetStatus(LevelStatus newStatus)
        {
            _status = newStatus;
            OnStatusChange?.Invoke(_status);
        }
    }

    public enum LevelStatus
    {
        Win,
        Lose,
        InProgress,
    }
}