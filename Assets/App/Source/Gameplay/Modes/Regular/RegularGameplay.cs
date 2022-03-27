using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

namespace Systemagedon.App.Gameplay
{

    [RequireComponent(typeof(AsteroidsAttack))]
    [RequireComponent(typeof(StarSystemSwitcher))]
    [RequireComponent(typeof(FrequencyComplication))]
    public class RegularGameplay : MonoBehaviour, ITransformLose
    {
        public event Action<Transform> Lose;


        [SerializeField] private float _safeTime;


        private AsteroidsAttack _atack;
        private StarSystemSwitcher _starSystem;
        private FrequencyComplication _complication;


        public void Restart()
        {
            new RegularMode().LoadAndPlay();
        }


        private void Start()
        {
            _atack = GetComponent<AsteroidsAttack>();
            _starSystem = GetComponent<StarSystemSwitcher>();
            _complication = GetComponent<FrequencyComplication>();
            _starSystem.SomePlanetRuined += OnSomePlanetRuined;
            _starSystem.SwitchStarted += OnSwitchStarted;
            _starSystem.SwitchEnded += OnSwitchEnded;
            StartCoroutine(GameStartCoroutine());
        }


        private void OnDestroy()
        {
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
            Lose?.Invoke(planet.transform);
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


        private IEnumerator OnSwitchEndedCoroutine()
        {
            yield return new WaitForSeconds(_safeTime);
            _atack.Run();
        }
    }

}
