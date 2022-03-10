using UnityEngine;
using System;
using Systemagedon.App.FX;

namespace Systemagedon.App.StarSystem
{

    public class AsteroidPathDrawer : MonoBehaviour
    {
        //public Asteroid Target { get => _target; }
        //public CurveDrawer PathDrawer { get => _pathDrawer; }

        
        //[SerializeField] private Transform _crossMarkerPrefab;
        //[SerializeField] private Asteroid _target;
        //[SerializeField] private LineRenderer _lineRenderer;


        //private Transform _crossMarker;
        //private CurveDrawer _pathDrawer;


        //public void HideCrossMarker()
        //{
        //    if (!_crossMarker)
        //    {
        //        throw new InvalidOperationException("Drawer must be enabled");
        //    }
        //    _crossMarker.GetComponentInChildren<Renderer>().enabled = false;
        //}


        //private void OnEnable()
        //{
        //    if (!_lineRenderer)
        //    {
        //        _pathDrawer = gameObject.AddComponent<CurveDrawer>();
        //        _pathDrawer.Init(_lineRenderer);
        //    }
        //    _crossMarker = Instantiate(_crossMarkerPrefab);
        //    _target.PathModified += OnAsteroidModified;
        //    ApplyCrossMarker();
        //}


        //private void OnDisable()
        //{
        //    _target.PathModified -= OnAsteroidModified;
        //    Destroy(_crossMarker);
        //}


        //private void OnAsteroidModified()
        //{
        //    ApplyCrossMarker();
        //}


        //private void ApplyCrossMarker()
        //{
        //    _crossMarker.transform.position =
        //        _target.Path.CalculatePoint(_target.Path.Length / 2f);
        //}
    }

}