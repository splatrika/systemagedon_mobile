using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systemagedon.App.Gameplay;
using Systemagedon.App;
using System;
using UnityEngine.SceneManagement;
using Systemagedon.App.Extensions;
using Systemagedon.App.Services;

namespace Systemagedon
{

    public class StartScreen : MonoBehaviour
    {
        [SerializeField] private StarSystemConfiguration _starSystemSettings;
        [SerializeField] private GameObject _callbackObject;
        [SerializeField] private string _thirdPartyScene;
        [SerializeField] private StarSystemContainer _starSystemContainer;

        private IStarScreenCallback _callback;
        private const int _planetsCount = 2;


        public void StartGame()
        {
            StartCoroutine(StartGameCoroutine());
        }


        public void ShowThirdPartyInfo()
        {
            SceneManager.LoadScene(_thirdPartyScene);
        }


        private void Awake()
        {
            var generator = new StarSystemGenerator(
                _starSystemSettings.ParseSettings());
            var starSystem = generator.Generate(_planetsCount);
            _starSystemContainer.Load(starSystem);

            OnValidate();
        }


        private void OnValidate()
        {
            this.AssignInterfaceField(ref _callbackObject, ref _callback,
                nameof(_callbackObject));
        }


        private IEnumerator StartGameCoroutine()
        {
            if (_callback != null)
            {
                bool callbackFinished = false;
                Action onCallBackFinished = () => callbackFinished = true;
                _callback.StartGameCallbackEnded += onCallBackFinished;
                _callback.RunStartGameCallback();
                yield return new WaitUntil(() => callbackFinished);
                _callback.StartGameCallbackEnded -= onCallBackFinished;
            }
            GlobalInstaller.StarSystemTransferService.GiveSnapshot(
                _starSystemContainer.Read()); // todo use snapshot
            SceneManager.LoadScene("Tutorial");
        }


        private void OnDrawGizmos()
        {
            if (_starSystemSettings)
            {
                _starSystemSettings.DrawGizmos();
            }
        }

    }

}
