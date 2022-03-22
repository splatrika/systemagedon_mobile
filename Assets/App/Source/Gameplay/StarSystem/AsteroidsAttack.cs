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


        [SerializeField] private float _topBorder;
        [SerializeField] private float _bottomBorder;
        [SerializeField] private SpawnProperties _properties;
        [SerializeField] private GameObject _starSystemObject;
        [Header("Complication")]
        [SerializeField] private GameObject _complicationObject;
        [SerializeField] private ComplicationProperties _complicationProperties;


        private IStarSystemProvider _starSystem;
        private List<Asteroid> _alive = new List<Asteroid>();
        private Complicator _complicator;
        private IComplication _complication;


        protected sealed override void SetupOnInstance(Asteroid instance)
        {
            Planet target = _starSystem.Planets.SelectRandom();
            float speed = Random.Range(_properties.AsteroidVelocity.Min,
                _properties.AsteroidVelocity.Max);
            Bezier path = new Bezier();
            path.PointA.y = _topBorder;
            path.PointB.y = _bottomBorder;
            MakeRandomLevers(ref path, _properties.LeverLength);
            instance.Init(target, path, speed);
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
        }


        private void MakeRandomLevers(ref Bezier curve, RangeFloat length)
        {
            float leversAngle = Random.Range(0f, Mathf.PI * 2);
            float leversLength = Random.Range(length.Min, length.Min);
            curve.LerpA.y = _topBorder;
            curve.LerpB.y = _bottomBorder;
            curve.LerpA.x = Mathf.Sin(leversAngle) * leversLength;
            curve.LerpA.z = Mathf.Cos(leversAngle) * leversLength;
            curve.LerpB.x = curve.LerpA.x;
            curve.LerpB.z = curve.LerpA.z;
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
            Gizmos.color = new Color(0, 0, 1, 0.4f);
            Gizmos.DrawCube(new Vector3(0, _topBorder, 0),
                new Vector3(10, 0.1f, 10));
            Gizmos.DrawCube(new Vector3(0, _bottomBorder, 0),
                new Vector3(10, 0.1f, 10));
        }


        void IComplicatable.RaiseDifficulty()
        {
            _properties.AsteroidVelocity +=
                _complicationProperties.AsteroidVelocity;
            RaiseSpawnPerSeconds(_complicationProperties.SpawnPerSecond);
        }
    }

}
