using UnityEngine;
using System.Collections;
using System;
using Systemagedon.App.FX;

namespace Systemagedon.App.StarSystem
{
    class AsteroidPathHighlighter : MonoBehaviour
    {
        [SerializeField] private LegacyAsteroid _target;
        [SerializeField] private CrossMarker _crossMarkerPrefab;
        [SerializeField] private LineRenderer _pathRenderer;
        [SerializeField] private Color _appearance;
        [SerializeField] private Color _danger;
        [SerializeField] private Color _dangerHasPassed;
        [SerializeField] private float _transitionsDuration;


        private CurveDrawer _drawer;
        private CrossMarker _crossMarker;
        private Coroutine _highlightCoroutine;


        private void Start()
        {
            Validate();

            _drawer = gameObject.AddComponent<CurveDrawer>();
            _drawer.Init(_target.Path, _pathRenderer);

            _crossMarker = Instantiate(_crossMarkerPrefab);
            _crossMarker.Show();
            _crossMarker.transform.position = _target.CrossPoint;

            _target.PathModified += OnTargetPathModified;

            _highlightCoroutine = StartCoroutine(HighlightCoroutine());
        }


        private void OnDestroy()
        {
            _target.PathModified -= OnTargetPathModified;
            Destroy(_crossMarker.gameObject);
        }


        private void Validate()
        {
            if (!_pathRenderer)
            {
                throw new NullReferenceException("_pathRenderer must be assigned");
            }
            if (!_target)
            {
                throw new NullReferenceException("_target must be assigned");
            }
            if (!_crossMarkerPrefab)
            {
                throw new NullReferenceException("_crossMarkerPrefab must be assigned");
            }
        }


        private void OnTargetPathModified()
        {
            _crossMarker.transform.position = _target.CrossPoint;
            if (_highlightCoroutine != null)
            {
                StopCoroutine(_highlightCoroutine);
                _highlightCoroutine = StartCoroutine(HighlightCoroutine());
            }
        }


        private IEnumerator HighlightCoroutine()
        {
            RunPathColorTransition(_appearance, _danger, _transitionsDuration);
            yield return new WaitWhile(
                () => _target.Path.Position < _target.CrossPosition);
            RunPathColorTransition(_danger, _dangerHasPassed, _transitionsDuration);
            _crossMarker.Hide();
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

    }
}
