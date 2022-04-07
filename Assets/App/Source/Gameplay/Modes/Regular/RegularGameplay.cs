using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

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


        private AsteroidsAttack _atack;
        private StarSystemSwitcher _starSystem;
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
            _starSystem = GetComponent<StarSystemSwitcher>();
            _complication = GetComponent<FrequencyComplication>();
            _scoreCounter = gameObject.AddComponent<RegularScoreCounter>();
            _scoreCounter.Init(_atack);
            _scoreCounter.ScoreChanged += OnScoreChanged;
            _starSystem.SomePlanetRuined += OnSomePlanetRuined;
            _starSystem.SwitchStarted += OnSwitchStarted;
            _starSystem.SwitchEnded += OnSwitchEnded;
            StartCoroutine(GameStartCoroutine());
        }


        private void OnDestroy()
        {
            _scoreCounter.ScoreChanged -= OnScoreChanged;
            _starSystem.SomePlanetRuined -= OnSomePlanetRuined;
            _starSystem.SwitchStarted -= OnSwitchStarted;
            _starSystem.SwitchEnded -= OnSwitchEnded;
        }


        private IEnumerator GameStartCoroutine()
        {
            yield return new WaitForSeconds(_safeTime);
            _atack.Run();
        }


        private void OnSomePlanetRuined(Planet planet)
        {
            SystemagedonApp.HighscoresService.Send(Score, _mode);
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


        private void OnSwitchStarted(StarSystem current)
        {
            _atack.Clear();
            _atack.Stop();
        }


        private void OnSwitchEnded(StarSystem newSystem)
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
