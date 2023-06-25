using System;
using UnityEngine;

namespace Animations
{
    public class CharacterAnimations : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayHurt()
        {
            _animator.SetTrigger(AnimationNames.Hurt);
        }

        public void PlayWin()
        {
            _animator.SetTrigger(AnimationNames.Win);
        }

        public void PlayJump()
        {
            _animator.SetTrigger(AnimationNames.Jump);
        }

        public void PlayLose()
        {
            _animator.SetTrigger(AnimationNames.Lose);
        }
    }
}