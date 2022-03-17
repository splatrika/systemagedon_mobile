using UnityEngine;
using System.Collections;
using System;
using Systemagedon.App.Drawing;

namespace Systemagedon.App.Gameplay.Drawing
{

    /// <summary>
    /// Init from script required
    /// </summary>
    public class AsteroidPathHighlight : MonoBehaviour
    {
        public event Action<AsteroidPathHighlight> Destroyed;


        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private uint _segments = 1;
        [SerializeField] private CrossMarker _crossMarkerPrefab;
        [Header("Colors")]
        [SerializeField] private Color _appear;
        [SerializeField] private Color _danger;
        [SerializeField] private Color _dangerPassed;
        [Header("Timing")]
        [SerializeField] private float _transitionsDuration;


        private Asteroid _asteroid;
        private CurveDrawer _drawer;
        private CrossMarker _crossMarker;
        private bool _inited = false;


        public void Init(Asteroid asteroid)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            if (!_lineRenderer || !_crossMarkerPrefab)
            {
                throw new Exception("AsteroidPathHighlight not fully setuped " +
                    "from inspector");
            }
            _asteroid = asteroid;
            _asteroid.DangerPassed += OnDangerPassed;
            _drawer = gameObject.AddComponent<CurveDrawer>();
            _drawer.Init(asteroid, _lineRenderer, _segments);
            _asteroid.DangerPassed += OnDangerPassed;
            _asteroid.Destroyed += OnAsteroidDestroyed;
            _crossMarker = Instantiate(_crossMarkerPrefab);
            _crossMarker.transform.position = _asteroid.GetCrossPoint();
            OnAsteroidAppear();
            _inited = true;
        }


        private void Start()
        {
            CheckInit();
        }


        private void OnDestroy()
        {
            _asteroid.DangerPassed -= OnDangerPassed;
            _asteroid.Destroyed -= OnAsteroidDestroyed;
            Destroy(_drawer);
            Destroy(_lineRenderer);
            Destroy(_crossMarker.gameObject);
            Destroyed?.Invoke(this);
        }


        private void OnAsteroidAppear()
        {
            RunPathColorTransition(_appear, _danger, _transitionsDuration);
            _crossMarker.Show();
        }


        private void OnDangerPassed(Asteroid sender)
        {
            RunPathColorTransition(_danger, _dangerPassed, _transitionsDuration);
            _crossMarker.Hide();
        }


        private void OnAsteroidDestroyed(Asteroid sender)
        {
            Destroy(this);
        }


        private void CheckInit()
        {
            if (!_inited)
            {
                throw new InvalidOperationException("Asteroid must inited " +
                    "from script!");
            }
        }


        private void RunPathColorTransition(Color from, Color to,
            float duration)
        {
            StartCoroutine(PathColorTransitionCoroutine(from, to, duration));
        }


        private IEnumerator PathColorTransitionCoroutine(Color from, Color to,
            float duration)
        {
            float timeLeft = duration;
            while (timeLeft >= 0)
            {
                Color current = Color.Lerp(to, from, timeLeft / duration);
                _drawer.ChangeColor(current);
                yield return null;
                timeLeft -= Time.deltaTime;
            }
        }


        private void OnValidate()
        {
            if (_segments == 0)
            {
                _segments = 1;
            }
        }
    }

}
