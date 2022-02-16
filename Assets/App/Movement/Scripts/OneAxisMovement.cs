using UnityEngine;
using System;

namespace Systemagedon.App.Movement
{
    public class OneAxisMovement : MonoBehaviour
    {
        public IOneAxisTransform Target { get; private set; }
        public float Velocity { get => _velocity; }


        [SerializeField] private GameObject _targetObject;
        [SerializeField] private float _velocity;


        public void ApplyVelocity(float velocity)
        {
            _velocity = velocity;
        }


        private void Update()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Target is not setted");
            }
            Target.SetPosition(Target.Position + _velocity * Time.deltaTime);
        }


        private void OnValidate()
        {
            Target = _targetObject.GetComponent<IOneAxisTransform>();
            if (Target == null)
            {
                _targetObject = null;
                Debug.LogWarning("Target Object needs component that " +
                    "implements IOneAxisTransform");
            }
        }
    }
}
