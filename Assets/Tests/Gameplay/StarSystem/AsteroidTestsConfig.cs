using System;
using UnityEngine;
using Systemagedon.App.Gameplay;

namespace Systemagedon.Tests
{

    [CreateAssetMenu(menuName = "Systemagedon/Tests/AsteroidTestsConfig")]
    public class AsteroidTestsConfig : ScriptableObject
    {
        public Asteroid AsteroidPrefab { get => _asteroidPrefab; }
        public Planet PlanetPrefab { get => _planetPrefab; }


        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private Planet _planetPrefab;
    }

}