using UnityEngine;
using System.Collections;
using System;

namespace Systemagedon.App.Gameplay
{

    public class SwitchAnimation : MonoBehaviour, IAnimation
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

        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                Debug.Log("tab");
                StartCoroutine(AnimationCoroutine());
            }
        }


        private IEnumerator AnimationCoroutine()
        {
            _animator.enabled = true;
            yield return StartAnimationCoroutine();
            yield return null;
            CallbackEnded?.Invoke();
            yield return EndAnimationCoroutine();
            _animator.enabled = false;
            
        }

        public IEnumerator RunAndWaitUntilEnd()
        {
            yield return StartAnimationCoroutine();
            yield return null;
            StartCoroutine(EndAnimationCoroutine()); // do not wait for end animation
        }

        private IEnumerator StartAnimationCoroutine()
        {
            _animator.Play(StartAnimation);
            yield return null;
            AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(state.length + 2);
        }

        private IEnumerator EndAnimationCoroutine()
        {
            _animator.Play(EndAnimation);
            CallbackEnded?.Invoke();
            yield return null;
            var state = _animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(state.length);
            _animator.Play(IdleAnimation);
            yield return null;
        }
    }

}
