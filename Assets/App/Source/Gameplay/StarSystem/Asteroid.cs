using UnityEngine;
using System;
using Systemagedon.App.Movement;

namespace Systemagedon.App.Gameplay
{

    /// <summary>
    /// Init from script requaired
    /// </summary>
    public class Asteroid : MonoBehaviour, ICurvePath
    {
        public event Action<Asteroid> DangerPassed;
        public event Action<Asteroid> Destroyed;


        public Bezier Path { get => _pathTransform.Curve; }
        public Vector3 CrossPoint { get => GetCrossPoint(); }


        private CurveTransform _pathTransform;
        private float _speed;
        private Vector3 _crossPoint;
        private float _crossPosition { get => _pathTransform.Length / 2; }
        private bool _dangerPassed = false;
        private bool _inited = false;


        public void Init(Planet target, Bezier pathSource, float speed)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _speed = speed;
            _pathTransform.ChangeCurve(pathSource);
            Vector3 offsetToPlanet = CalculateOffsetToPlanet(target,
                _crossPosition, _speed);
            Bezier movedPath = _pathTransform.Curve;
            movedPath.MoveBy(offsetToPlanet);
            _pathTransform.ChangeCurve(movedPath);
            _crossPoint = _pathTransform.CalculatePoint(_crossPosition);
            _inited = true;
        }


        public float GetCrossPosition()
        {
            CheckInit();
            return _crossPosition;
        }


        public Vector3 GetCrossPoint()
        {
            CheckInit();
            return _crossPoint;
        }


        private void Awake()
        {
            _pathTransform = gameObject.AddComponent<CurveTransform>();
        }


        private void Start()
        {
            CheckInit();
        }


        private void CheckInit()
        {
            if (!_inited)
            {
                throw new InvalidOperationException("Asteroid must inited " +
                    "from script!");
            }
        }


        private void Update()
        {
            float newPosition = _pathTransform.Position + _speed * Time.deltaTime;
            if (!_dangerPassed && newPosition > _crossPosition)
            {
                _dangerPassed = true;
                DangerPassed?.Invoke(this);
            }
            if (newPosition > _pathTransform.Length)
            {
                Destroy(gameObject);
                Destroyed?.Invoke(this);
            }
            else
            {
                _pathTransform.SetPosition(newPosition);
            }
        }


        private void OnCollisionEnter(Collision collision)
        {
            Planet planet = collision.rigidbody.GetComponent<Planet>();
            if (planet)
            {
                planet.Ruin();
            }
        }


        private Vector3 CalculateOffsetToPlanet(Planet target, float crossPosition,
            float speed)
        {
            float middleOfAsteroidPath = crossPosition;
            Vector3 asteroidCrossPoint =
                _pathTransform.CalculatePoint(middleOfAsteroidPath);
            float secondsToCross = crossPosition / speed;

            Vector3 planetCrossPoint =
                target.CalculatePoint(secondsToCross);
            Vector3 offsetForAsteroid = planetCrossPoint - asteroidCrossPoint;
            return offsetForAsteroid;
        }
    }

}
