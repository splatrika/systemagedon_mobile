using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Systemagedon.App.Gameplay.Drawing;

namespace Systemagedon.App.Gameplay.TutorialStates
{

    [Serializable]
    public class IntroductionCinemaState : ITutorialState
    {
        [SerializeField] private float _asteroidWait;
        [Header("AsteroidFollowing")]
        [SerializeField] private float _followWait;
        [SerializeField] private Vector3 _asteroidCameraOffset;
        [SerializeField] private Vector3 _asteroidCameraRotation;
        [Header("LookAtPlanet")]
        [SerializeField] private Vector3 _planetCameraOffset;
        [SerializeField] private Vector3 _planetCameraRotation;
        [SerializeField] private float _transition;
        [SerializeField] private AsteroidPathHighlight _asteroidHighlightPrefab;
        [Header("Rewind")]
        [SerializeField] private float _rewindDelay;
        [SerializeField] private float _rewindDuration;
        [SerializeField] private RewindHUD _rewindHUDPrefab;
        [SerializeField] private Canvas _canvas;
        [Header("Sound")]
        [SerializeField] private AudioSource _optionalBackground;
        [SerializeField] private float _rewindStrength;


        private Vector3 _originalCameraPosition;
        private Quaternion _originalCameraRotation;
        private bool _planetAlive { get => !_ruins; }
        private PlanetRuins _ruins;
        private ITutorialContext _context;


        public void OnStart(ITutorialContext context)
        {
            _context = context;
            _originalCameraPosition = context.Camera.transform.position;
            _originalCameraRotation = context.Camera.transform.rotation;
            if (!context.ExamplePlanet)
            {
                throw new InvalidOperationException("Example planet has not selected");
            }
            context.ExamplePlanet.RuinedAdvanced += OnPlanetRuined;
            context.Component.StartCoroutine(CinemaCoroutine(context));
        }


        public void OnFinish(ITutorialContext context)
        {
            _ruins = null;
            _context = null;
        }


        private IEnumerator CinemaCoroutine(ITutorialContext context)
        {
            yield return new WaitForSeconds(_asteroidWait);
            Asteroid asteroid = context.AsteroidsGenerator.GenerateAndSpawn(
                context.AsteroidPrefab, context.ExamplePlanet);
            GameObject.Instantiate(_asteroidHighlightPrefab).Init(asteroid);
            yield return new WaitForSeconds(_followWait);
            context.Camera.transform.rotation = Quaternion.Euler(_asteroidCameraRotation);
            while (_planetAlive)
            {
                yield return null;
                context.Camera.transform.position = asteroid.transform.position
                    + _asteroidCameraOffset;
            }
            Vector3 lookAtPlanet = _ruins.transform.position + _planetCameraOffset;
            context.Camera.transform.DOMove(lookAtPlanet, _transition);
            context.Camera.transform.DORotate(_planetCameraRotation,
            _transition);
            GameObject.Destroy(asteroid.gameObject);
            if (_optionalBackground)
            {
                _optionalBackground.pitch = 0;
            }
            yield return new WaitForSeconds(_rewindDelay);
            if (_optionalBackground)
            {
                _optionalBackground.pitch = -1 * _rewindStrength;
            }
            RewindHUD rewindHUD = GameObject.Instantiate(_rewindHUDPrefab);
            rewindHUD.transform.SetParent(_canvas.transform, false);
            Planet restored = _ruins.Restore();
            context.StarSystem.AddPlanet(restored);
            context.ExamplePlanet = restored;
            context.Camera.transform.DOMove(_originalCameraPosition, _rewindDuration);
            context.Camera.transform.DORotate(_originalCameraRotation.eulerAngles,
                _rewindDuration);
            yield return new WaitForSeconds(_rewindDuration);
            if (_optionalBackground)
            {
                _optionalBackground.pitch = 1;
            }
            GameObject.Destroy(rewindHUD.gameObject);
            context.ChangeState<TapHintState>();
        }


        private void OnPlanetRuined(Planet sender, PlanetRuins ruins)
        {
            _ruins = ruins;
            _context.ExamplePlanet.RuinedAdvanced -= OnPlanetRuined;
        }
    }

}
