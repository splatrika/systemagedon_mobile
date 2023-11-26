using UnityEngine;
using System.Collections;
using System;

namespace Systemagedon
{
    [RequireComponent(typeof(Animator))]
    public class StartScreenAnimations : MonoBehaviour, IStarScreenCallback
    {
        public event Action StartGameCallbackEnded;


        [SerializeField] private string _starGameAnimation;
        [SerializeField] private AudioSource _optionalStartGameSound;
        [SerializeField] private AudioSource _optionalIdleSound;


        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            if (_optionalIdleSound)
            {
                _optionalIdleSound.Play();
            }
        }


        public void RunStartGameCallback()
        {
            if (_optionalIdleSound)
            {
                _optionalIdleSound.Stop();
            }
            if (_optionalStartGameSound)
            {
                _optionalStartGameSound.Play();
            }
            StartCoroutine(StartGameCoroutine());
        }


        private IEnumerator StartGameCoroutine()
        {
            _animator.Play(_starGameAnimation);
            yield return null;
            float duration = _animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(duration);
            StartGameCallbackEnded?.Invoke();
        }
    }

}