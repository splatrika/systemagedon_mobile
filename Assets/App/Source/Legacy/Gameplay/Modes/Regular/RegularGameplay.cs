using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using Systemagedon.App.Services;

namespace Systemagedon.App.Gameplay
{

    [RequireComponent(typeof(AsteroidsAttack))]
    [RequireComponent(typeof(StarSystemSwitcher))]
    [RequireComponent(typeof(FrequencyComplication))]
    public class RegularGameplay : MonoBehaviour, IScore
    {
        public event Action<RegularLoseContext> Lose;
        public event Action<int> ScoreChanged;


        public int Score { get => _scoreCounter.Score; }


        [SerializeField] private float _safeTime;
        [SerializeField] private StarSystemContainer _starSystemContainer;


        private AsteroidsAttack _atack;
        private StarSystemSwitcher _starSystemSwitcher;
        private FrequencyComplication _complication;
        private RegularScoreCounter _scoreCounter;
        private RegularMode _mode = new RegularMode();


        public void Restart()
        {
            _mode.LoadAndPlay();
        }


        private void Awake()
        {
            _atack = GetComponent<AsteroidsAttack>();
            _starSystemSwitcher = GetComponent<StarSystemSwitcher>();
            if (GlobalInstaller.StarSystemTransferService.IsNotEmpty())
            {
                StarSystem startsWith = GlobalInstaller.StarSystemTransferService.Take();
                _starSystemSwitcher.Init(startsWith);
            }
            _complication = GetComponent<FrequencyComplication>();
            _scoreCounter = gameObject.AddComponent<RegularScoreCounter>();
            _scoreCounter.Init(_atack);
            _scoreCounter.ScoreChanged += OnScoreChanged;
            _starSystemContainer.SomePlanetRuined += OnSomePlanetRuined;
            _starSystemSwitcher.SwitchStarted += OnSwitchStarted;
            _starSystemSwitcher.SwitchEnded += OnSwitchEnded;
            StartCoroutine(GameStartCoroutine());
        }


        private void OnDestroy()
        {
            _scoreCounter.ScoreChanged -= OnScoreChanged;
            _starSystemContainer.SomePlanetRuined -= OnSomePlanetRuined;
            _starSystemSwitcher.SwitchStarted -= OnSwitchStarted;
            _starSystemSwitcher.SwitchEnded -= OnSwitchEnded;
        }


        private IEnumerator GameStartCoroutine()
        {
            yield return new WaitForSeconds(_safeTime);
            _atack.Run();
        }


        private void OnSomePlanetRuined(Planet planet)
        {
            Lose?.Invoke(new RegularLoseContext()
            {
                Ruined = planet,
                Score = Score,
                Sender = this
            });
            _atack.Clear();
            _atack.Stop();
            _complication.enabled = false;
        }


        private void OnSwitchStarted()
        {
            _atack.Clear();
            _atack.Stop();
        }


        private void OnSwitchEnded()
        {
            StartCoroutine(OnSwitchEndedCoroutine());
        }


        private void OnScoreChanged(int value)
        {
            ScoreChanged?.Invoke(value);
        }


        private IEnumerator OnSwitchEndedCoroutine()
        {
            yield return new WaitForSeconds(_safeTime);
            _atack.Run();
        }
    }

}
