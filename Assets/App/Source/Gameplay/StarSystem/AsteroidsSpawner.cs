using UnityEngine;
using System;
using System.Linq;
using Systemagedon.App.Movement;
using Random = UnityEngine.Random;

namespace Systemagedon.App.Gameplay
{

    public class AsteroidsSpawner : FrequencySpawner<Asteroid>, IComplicatable
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


        [SerializeField] private float _topBorder;
        [SerializeField] private float _bottomBorder;
        [SerializeField] private SpawnProperties _properties;
        [SerializeField] private StarSystem _starSystem;
        [Header("Complication")]
        [SerializeField] private GameObject _complicationObject;
        [SerializeField] private ComplicationProperties _complicationProperties;


        private Complicator _complicator;
        private IComplication _complication;
        private const string _invalidComplicationMessage = "Complication object" +
            " must have component that implements IComplication";


        protected sealed override void SetupOnInstance(Asteroid instance)
        {
            Planet target = SelectRandomPlanet(_starSystem);
            float speed = Random.Range(_properties.AsteroidVelocity.Min,
                _properties.AsteroidVelocity.Max);
            Bezier path = new Bezier();
            path.PointA.y = _topBorder;
            path.PointB.y = _bottomBorder;
            MakeRandomLevers(ref path, _properties.LeverLength);
            instance.Init(target, path, speed);
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
            _complication = _complicationObject.GetComponent<IComplication>();
            if (_complication == null)
            {
                Debug.LogError(_invalidComplicationMessage);
            }
            _complicator = gameObject.AddComponent<Complicator>();
            _complicator.Init(_complication, this);
        }


        protected sealed override void Validate()
        {
            bool invalidComplication = _complicationObject
                && _complicationObject.GetComponent<IComplication>() == null;
            if (invalidComplication)
            {
                Debug.LogError(_invalidComplicationMessage);
                _complicationObject = null;
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 0, 1, 0.4f);
            Gizmos.DrawCube(new Vector3(0, _topBorder, 0),
                new Vector3(10, 0.1f, 10));
            Gizmos.DrawCube(new Vector3(0, _bottomBorder, 0),
                new Vector3(10, 0.1f, 10));
        }


        private Planet SelectRandomPlanet(StarSystem from)
        {
            int planetsCount = _starSystem.Planets.Count();
            int randomIndex = Random.Range(0, planetsCount);
            return _starSystem.Planets.ElementAt(randomIndex);
        }


        void IComplicatable.RaiseDifficulty()
        {
            _properties.AsteroidVelocity +=
                _complicationProperties.AsteroidVelocity;
            RaiseSpawnPerSeconds(_complicationProperties.SpawnPerSecond);
        }
    }

}
