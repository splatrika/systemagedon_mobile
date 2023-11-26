using UnityEngine;
using System.Collections;
using Systemagedon.App.Movement;
using System;

namespace Systemagedon.App.Gameplay
{

    public class Dash : MonoBehaviour, IDash
    {
        public OneAxisMovement Target { get => _target; }
        public PropertiesFields Properties { get => _properties; }


        [Serializable]
        public struct PropertiesFields
        {
            public float Strength;
            public float Duration;

            public static PropertiesFields Default
            { get => new PropertiesFields()
                {
                    Strength = 1,
                    Duration = 1
                };
            }
        }


        [SerializeField] private OneAxisMovement _target;
        [SerializeField] private PropertiesFields _properties =
            PropertiesFields.Default;


        private Coroutine _previousDash;
        private bool _inited;


        public void Init(OneAxisMovement target, PropertiesFields properties)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _target = target;
            _properties = properties;
        }


        public void ApplyDash()
        {
            if (_previousDash != null)
            {
                StopCoroutine(_previousDash);
            }
            _previousDash = StartCoroutine(DashCoroutine(_properties.Duration,
                _properties.Strength));
        }


        private void Start()
        {
            if (!_target)
            {
                Debug.LogError("Dash has not assigned target");
            }
            if (_properties.Strength <= 0)
            {
                Debug.LogError("Strength can't be equals or less zero");
            }
            if (_properties.Duration <= 0)
            {
                Debug.LogError("Duration can't be equals or less zero");
            }
            _inited = true;
        }


        private IEnumerator DashCoroutine(float duration, float strength)
        {
            float _timeLeft = duration;
            while (_timeLeft > 0)
            {
                float factor = _timeLeft / duration;
                float _additionalVelocity = strength * factor;
                _target.SetAdditionalVelocity(_additionalVelocity);
                yield return null;
                _timeLeft -= Time.deltaTime;
            }
            _target.SetAdditionalVelocity(0);
        }
    }

}