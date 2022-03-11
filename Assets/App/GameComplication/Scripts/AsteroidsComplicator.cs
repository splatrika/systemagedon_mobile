using UnityEngine;
using System;
using Systemagedon.App.StarSystem;

namespace Systemagedon.App.GameComplicaton
{

    public class AsteroidsComplicator : Complicator
    {
        [Serializable]
        public struct RiseSteps
        {
            public float SpawnPerSecond;
            public RangeFloat Velocity;
        }


        public RiseSteps Steps { get => _riseSteps; }


        [SerializeField] private AsteroidSpawner _spawner;
        [SerializeField] private RiseSteps _riseSteps;


        protected override void OnComplicate(int level)
        {
            _spawner.RaiseAsteroidVelocity(_riseSteps.Velocity);
            _spawner.RaiseSpawnPerSeconds(_riseSteps.SpawnPerSecond);

        }
    }

}
