using UnityEngine;
using System;

namespace Systemagedon.App.Movement
{
    public class OneAxisMovement : MonoBehaviour
    {
        public IOneAxisTransform Target { get => _target; }
        public float Velocity { get => _velocity; }


        [SerializeField] private GameObject _targetObject;
        [SerializeField] private float _velocity;


        private IOneAxisTransform _target;


        public void ApplyVelocity(float velocity)
        {
            _velocity = velocity;
        }


        private void Update()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Target must be assigned");
            }
            Target.SetPosition(Target.Position + _velocity * Time.deltaTime);
        }


        private void Start()
        {
            TrySetupTargetFrom(_targetObject, out _target);
        }


        private void OnValidate()
        {
            if (_targetObject && !TrySetupTargetFrom(_targetObject, out _target))
            {
                _targetObject = null;
                Debug.LogWarning("Target Object needs component that " +
                    "implements IOneAxisTransform");
            }
        }


        private bool TrySetupTargetFrom(in GameObject from, out IOneAxisTransform to)
        {
            to = from.GetComponent<IOneAxisTransform>();
            return to != null;
        }
    }
}

