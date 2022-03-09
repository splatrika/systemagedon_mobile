using UnityEngine;
using System.Collections;
using Systemagedon.App.FX;

namespace Systemagedon.App.StarSystem
{

    public class AsteroidPathDrawer : MonoBehaviour
    {
        [SerializeField] private CurveDrawer _pathDrawer;
        [SerializeField] private Transform _crossMarkerPrefab;
        [SerializeField] private Asteroid _target;

        private Transform _crossMarker;


        private void OnEnable()
        {
            _crossMarker = Instantiate(_crossMarkerPrefab);
            _target.PathModified += OnAsteroidModified;
            ApplyCrossMarker();
        }


        private void OnDisable()
        {
            _target.PathModified -= OnAsteroidModified;
            Destroy(_crossMarker);
        }


        private void OnAsteroidModified()
        {
            ApplyCrossMarker();
        }


        private void ApplyCrossMarker()
        {
            _crossMarker.transform.position =
                _target.Path.CalculatePoint(_target.Path.Length / 2f);
        }
    }

}