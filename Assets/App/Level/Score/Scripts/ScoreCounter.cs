using UnityEngine;
using System.Collections.Generic;
using System;
using Systemagedon.App.StarSystem;

namespace Systemagedon.App.Score
{

    public class ScoreCounter : MonoBehaviour, IScore
    {

        public event Action Updated;


        public int Score { get; private set; }


        private class AsteroidRegistration
        {
            public Asteroid Asteroid;
            public Action DestroyListener;
        }


        [SerializeField] private AsteroidSpawner _asteroidSpawner;
        private List<AsteroidRegistration> _registered = new List<AsteroidRegistration>();



        private void Start()
        {
            _asteroidSpawner.Spawned += OnAsteroidSpawned;
        }


        private void OnDestroy()
        {
            _asteroidSpawner.Spawned -= OnAsteroidSpawned;
            while (_registered.Count > 0)
            {
                RemoveAsteroidRegistration(_registered[0]);
            }
        }


        private void OnAsteroidSpawned(Asteroid asteroid)
        {
            AsteroidRegistration registration = new AsteroidRegistration();
            registration.Asteroid = asteroid;
            registration.DestroyListener = () => OnAsteroidDestroyed(registration);
            asteroid.Destroyed += registration.DestroyListener;
        }


        private void OnAsteroidDestroyed(AsteroidRegistration context)
        {
            Score++;
            Updated?.Invoke();
            RemoveAsteroidRegistration(context);
        }


        private void RemoveAsteroidRegistration(AsteroidRegistration registration)
        {
            print("Remove");
            registration.Asteroid.Destroyed -= registration.DestroyListener;
            _registered.Remove(registration);
        }
    }

}
