using UnityEngine;
using Systemagedon.App.Movement;

namespace Systemagedon.App.StarSystem
{
    [ExecuteInEditMode]
    public class OrbitTransform : MonoBehaviour, IOneAxisTransform
    {
        float IOneAxisTransform.Position { get => _anglePosition; }


        [SerializeField] private float _anglePosition;
        [SerializeField] private float _radius;


        Transform _transform;


        public void SetPosition(float value)
        {
            SetValueAndApplyPosition(ref _anglePosition, value);
        }


        public void SetRadius(float value)
        {
            SetValueAndApplyPosition(ref _radius, value);
        }


        private void SetValueAndApplyPosition<T>(ref T field, T value)
        {
            field = value;
            ApplyPosition(_anglePosition, _radius);
        }


        private void ApplyPosition(float angle, float radius)
        {
            Vector3 updatedPosition = new Vector3();
            updatedPosition.x = Mathf.Sin(angle);
            updatedPosition.z = Mathf.Cos(angle);
            updatedPosition *= radius;
            _transform.localPosition = updatedPosition;
        }


        private void Start()
        {
            ApplyPosition(_anglePosition, _radius);
        }


        private void OnEnable()
        {
            _transform = transform;
            _transform.hideFlags = HideFlags.NotEditable;
        }


        private void OnDisable()
        {
            _transform.hideFlags = HideFlags.None;
        }


    }
}