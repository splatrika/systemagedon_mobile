using UnityEngine;
using System.Collections;
using Systemagedon.App.Movement;

namespace Systemagedon.App.StarSystem
{

    public class AsteroidMovement : MonoBehaviour
    {
        public Asteroid Target { get => _target; }


        [SerializeField] private Asteroid _target;
        [SerializeField] private float _velocity;


        private CurveTransform _path;


        public void SetVelocity(float value)
        {
            _velocity = value;
        }


        public float CalculateSeconds(float fromPosition, float toPosition)
        {
            float distance = Mathf.Abs(toPosition - fromPosition);
            return distance / _velocity;
        }


        private void Start()
        {
            _path = _target.Path;
        }


        private void Update()
        {
            float newPosition = _path.Position + _velocity * Time.deltaTime;
            if (newPosition > _path.Length)
            {
                Destroy(_target.gameObject);
                Destroy(this);
            }
            else
            {
                _path.SetPosition(newPosition);
            }
        }

    }

}