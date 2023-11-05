using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systemagedon.App.Gameplay;
using Systemagedon.App;
using System;
using UnityEngine.SceneManagement;
using Systemagedon.App.Extensions;


namespace Systemagedon
{

    public class StartScreen : MonoBehaviour, ILegacyStarSystemProvider
    {
        public event Action<Planet> SomePlanetRuined;
        public event Action<ILegacyStarSystemProvider> ModelUpdated;


        public IReadOnlyCollection<Planet> Planets { get => _starSystem.Planets; }


        [SerializeField] private StarSystemGeneratorLegacy _generator;
        [SerializeField] private GameObject _callbackObject;
        [SerializeField] private string _thirdPartyScene;


        private StarSystem _starSystem;
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
            _starSystem = _generator.GenerateAndSpawn(_planetsCount);
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
            GlobalInstaller.StarSystemTransferService.Give(_starSystem);
            SceneManager.LoadScene("Tutorial");
        }


        private void OnDrawGizmos()
        {
            if (_generator)
            {
                _generator.DrawGizmos();
            }
        }

    }

}
