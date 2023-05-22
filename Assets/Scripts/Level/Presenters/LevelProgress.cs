using System;
using Characters.Base;

namespace Level.Presenters
{
    public class LevelProgress
    {
        private LevelStatus _status = LevelStatus.InProgress;

        private int _numberOfEnemies;

        public event Action<LevelStatus> OnStatusChange; 

        public void SetNumberOfEnemies(int number)
        {
            _numberOfEnemies = number;
        }

        public void ChangeStatusAfterCharacterLost(Character character)
        {
            switch (character.Fraction)
            {
                case Fraction.Fraction.Player:
                    SetStatus(LevelStatus.Lose);
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
                SetStatus(LevelStatus.Win);
            }
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