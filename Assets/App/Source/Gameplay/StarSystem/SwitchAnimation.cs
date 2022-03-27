using UnityEngine;
using System.Collections;
using System;

namespace Systemagedon.App.Gameplay
{

    public class SwitchAnimation : MonoBehaviour, ISwitchCallback
    {
        public event Action CallbackEnded;


        public const string StartAnimation = "Start";
        public const string EndAnimation = "End";
        public const string IdleAnimation = "Idle";


        [SerializeField] private Animator _animator;


        public void Run()
        {
            StartCoroutine(AnimationCoroutine());
        }


        private void Start()
        {
            _animator.enabled = false;
        }


        private IEnumerator AnimationCoroutine()
        {
            _animator.enabled = true;
            _animator.Play(StartAnimation);
            AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(state.length);
            _animator.Play(EndAnimation);
            CallbackEnded?.Invoke();
            state = _animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(state.length);
            _animator.enabled = false;
            
        }
    }

}
