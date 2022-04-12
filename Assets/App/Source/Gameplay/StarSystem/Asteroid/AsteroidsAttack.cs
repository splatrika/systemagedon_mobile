using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Systemagedon.App.Movement;
using Systemagedon.App.Extensions;
using Random = UnityEngine.Random;

namespace Systemagedon.App.Gameplay
{

    public class AsteroidsAttack : FrequencySpawner<Asteroid>, IComplicatable
    {
        [Serializable]
        public struct SpawnProperties
        {
            public RangeFloat LeverLength;
            public RangeFloat AsteroidVelocity;
        }

        [Serializable]
        public struct ComplicationProperties
        {
            public RangeFloat AsteroidVelocity;
            public float SpawnPerSecond;
        }


        public event Action<Asteroid> SomeDangerPassed;
        public event Action<Asteroid> SomeDestroyed;


        [SerializeField] private GameObject _starSystemObject;
        [SerializeField] private AsteroidsGenerator _generator;
        [Header("Complication")]
        [SerializeField] private GameObject _complicationObject;
        [SerializeField] private ComplicationProperties _complicationProperties;


        private IStarSystemProvider _starSystem;
        private List<Asteroid> _alive = new List<Asteroid>();
        private Complicator _complicator;
        private IComplication _complication;
        private RangeFloat _additionalVelocity;


        public void Clear()
        {
            _alive.ForEach(asteroid => Destroy(asteroid.gameObject));
        }


        protected sealed override void SetupOnInstance(Asteroid instance)
        {
            Planet target = _starSystem.Planets.SelectRandom();
            _generator.RandomlySetup(instance, target, _additionalVelocity);
            _alive.Add(instance);
            instance.DangerPassed += OnAsteroidDangerPassed;
            instance.Destroyed += OnAsteroidDestroyed;
        }


        protected sealed override void Validate()
        {
            this.AssignInterfaceField(ref _complicationObject, ref _complication,
                nameof(_complicationObject));
            this.AssignInterfaceField(ref _starSystemObject, ref _starSystem,
                nameof(_starSystemObject));
        }


        protected sealed override void OnSpawnerDestroy()
        {
            _alive.ForEach((asteroid) => Destroy(asteroid.gameObject));
        }


        private void OnAsteroidDangerPassed(Asteroid sender)
        {
            SomeDangerPassed?.Invoke(sender);
        }


        private void OnAsteroidDestroyed(Asteroid sender)
        {
            SomeDestroyed?.Invoke(sender);
            sender.DangerPassed -= OnAsteroidDangerPassed;
            sender.Destroyed -= OnAsteroidDestroyed;
            _alive.Remove(sender);
        }


        private void Awake()
        {
            Validate();
            if (_complication == null)
            {
                throw new NullReferenceException(nameof(_complication));
            }
            if (_starSystem == null)
            {
                throw new NullReferenceException(nameof(_starSystem));
            }
            _complicator = gameObject.AddComponent<Complicator>();
            _complicator.Init(_complication, this);
        }


        private void OnDrawGizmos()
        {
            _generator.DrawGizmos();
        }


        void IComplicatable.RaiseDifficulty()
        {
            _additionalVelocity += _complicationProperties.AsteroidVelocity;
            RaiseSpawnPerSeconds(_complicationProperties.SpawnPerSecond);
        }
    }

}
