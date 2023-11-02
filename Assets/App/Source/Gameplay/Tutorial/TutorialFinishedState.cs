using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

namespace Systemagedon.App.Gameplay.TutorialStates
{
    [Serializable]
    public class TutorialFinishedState : ITutorialState
    {
        [SerializeField] private float _waitToEnd;
        [SerializeField] private DoneHUD _hudPrefab;
        [SerializeField] private Canvas _canvas;
        [Header("Music")]
        [SerializeField] private AudioSource _optionalBackground;
        [SerializeField] private float _fadeOutDuration;


        private DoneHUD _hudInstance;


        public void OnFinish(ITutorialContext context)
        {
            GameObject.Destroy(_hudInstance.gameObject);
        }


        public void OnStart(ITutorialContext context)
        {
            if (_optionalBackground)
            {
                DOTween.To(() => _optionalBackground.volume,
                    x => _optionalBackground.volume = x, 0, _fadeOutDuration);
            }
            _hudInstance = GameObject.Instantiate(_hudPrefab);
            _hudInstance.transform.SetParent(_canvas.transform, false);
            context.Component.StartCoroutine(FinishCoroutine(context));
        }


        private IEnumerator FinishCoroutine(ITutorialContext context)
        {
            yield return new WaitForSeconds(_waitToEnd);
            GlobalInstaller.StarSystemTransferService.Give(context.StarSystem);
            new RegularMode().LoadAndPlay();
        }
    }

}