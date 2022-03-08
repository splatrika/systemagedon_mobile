using UnityEngine;
using System.Collections;

namespace Systemagedon.App.Movement
{
    public class Dash : MonoBehaviour
    {
        public OneAxisMovement Target { get => _target; }
        public float Strength { get => _strength; }
        public float Duration { get => _duration; }


        [SerializeField] private OneAxisMovement _target;
        [SerializeField] private float _strength;
        [SerializeField] private float _duration;


        private Coroutine _previousDash;


        public void ApplyDash()
        {
            if (_previousDash != null)
            {
                StopCoroutine(_previousDash);
            }
            _previousDash = StartCoroutine(DashCoroutine(_duration));
        }


        public void SetStrength(float value)
        {
            _strength = value;
        }


        public void SetDuration(float value)
        {
            _duration = value;
        }


        private IEnumerator DashCoroutine(float duration)
        {
            float _timeLeft = duration;
            while (_timeLeft > 0)
            {
                float factor = _timeLeft / duration;
                float _additionalVelocity = _strength * factor;
                _target.SetAdditionalVelocity(_additionalVelocity);
                yield return null;
                _timeLeft -= Time.deltaTime;
            }
            _target.SetAdditionalVelocity(0);
        }
    }
}
