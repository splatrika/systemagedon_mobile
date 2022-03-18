using UnityEngine;
using System.Collections.Generic;
using System;

namespace Systemagedon.App.Gameplay
{

    public class ScoreCounter : MonoBehaviour, IScore
    {
        public event Action<int> ScoreChanged;


        public int Score { get; private set; }


        [SerializeField] private AsteroidsSpawner _asteroidsSpawner;
        private List<Asteroid> _registeredAsteroids = new List<Asteroid>();


        private void Start()
        {
            _asteroidsSpawner.Spawned += OnAsteroidSpawned;
        }


        private void OnDestroy()
        {
            _asteroidsSpawner.Spawned -= OnAsteroidSpawned;
            while (_registeredAsteroids.Count > 0)
            {
                UnregisterAsteroid(_registeredAsteroids[0]);
            }
        }


        private void OnAsteroidSpawned(Asteroid asteroid)
        {
            asteroid.DangerPassed += OnDangerPassed;
            asteroid.Destroyed += OnAsteroidDestroyed;
            _registeredAsteroids.Add(asteroid);
        }


        private void OnDangerPassed(Asteroid sender)
        {
            Score++;
            ScoreChanged?.Invoke(Score);
        }


        private void OnAsteroidDestroyed(Asteroid sender)
        {
            UnregisterAsteroid(sender);
        }


        private void UnregisterAsteroid(Asteroid asteroid)
        {
            if (!_registeredAsteroids.Contains(asteroid))
            {
                throw new InvalidOperationException("Asteroid not registered");
            }
            asteroid.DangerPassed -= OnDangerPassed;
            asteroid.Destroyed -= OnAsteroidDestroyed;
            _registeredAsteroids.Remove(asteroid);
        }
    }

}
