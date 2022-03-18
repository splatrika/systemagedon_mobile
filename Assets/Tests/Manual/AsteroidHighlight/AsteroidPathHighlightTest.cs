using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systemagedon.App.Movement;
using Systemagedon.App.Gameplay;
using Systemagedon.App.Gameplay.Drawing;


namespace Systemagedon.Tests.Manual
{

    public class AsteroidPathHighlightTest : MonoBehaviour
    {
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private Planet _targetPlanet;
        [SerializeField] private AsteroidPathHighlight _highlightPrefab;


        private void Start()
        {
            Asteroid asteroid = Instantiate(_asteroidPrefab);
            Bezier bezier = new Bezier()
            {
                PointA = Vector3.up * 10,
                PointB = Vector3.down * 10
            };
            asteroid.Init(_targetPlanet, bezier, 5);
            AsteroidPathHighlight highlight = Instantiate(_highlightPrefab);
            highlight.Init(asteroid);
        }
    }

}
