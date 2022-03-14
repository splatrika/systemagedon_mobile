using UnityEngine;
using System;

namespace Systemagedon.App.Movement
{
    public class OneAxisMovement : MonoBehaviour
    {
        public IOneAxisTransform Target { get => _target; }
        public float Velocity { get => _velocity; }
        public float AdditionalVelocity { get => _additionalVelocity; }
        public float TotalVelocity { get => _velocity + _additionalVelocity; }


        [SerializeField] private GameObject _targetObject;
        [SerializeField] private float _velocity;
        [SerializeField] private float _additionalVelocity = 0;


        private IOneAxisTransform _target;
        private bool _inited = false;


        public void Init(IOneAxisTransform target, float velocity)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _target = target;
            _velocity = velocity;
            _inited = true;
        }


        public void ApplyVelocity(float velocity)
        {
            _velocity = velocity;
        }


        public void SetVelocity(float value)
        {
            _velocity = value;
        }


        public void SetAdditionalVelocity(float value)
        {
            _additionalVelocity = value;
        }


        public Vector3 CalculatePoint(float afterSeconds, float fromPosition)
        {
            return _target.CalculatePoint(fromPosition + TotalVelocity * afterSeconds);
        }


        public Vector3 CalculatePoint(float afterSeconds)
        {
            if (Target == null)
            {
                throw new NullReferenceException("Target must be assigned");
            }
            return CalculatePoint(afterSeconds, _target.Position);
        }


        public float CalculateSeconds(float fromPosition, float toPosition)
        {
            float distance = Mathf.Abs(toPosition - fromPosition);
            return distance / TotalVelocity;
        }


        private void Update()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Target must be assigned");
            }
            float directionModifier = (_velocity != 0)
                ? Mathf.Sign(_velocity)
                : 1;
            float completeVelocity = _velocity + _additionalVelocity * directionModifier;
            Target.SetPosition(Target.Position + completeVelocity * Time.deltaTime);
        }


        private void Awake()
        {
            OnValidate();
        }


        private void Start()
        {
            _inited = true;
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

