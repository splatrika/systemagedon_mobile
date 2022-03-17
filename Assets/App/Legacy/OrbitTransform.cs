using UnityEngine;
using Systemagedon.App.Movement;

namespace Systemagedon.App.StarSystem
{
    [ExecuteInEditMode]
    public class OrbitTransform : MonoBehaviour, IOneAxisTransform, IRoundPath
    {
        float IOneAxisTransform.Position { get => _anglePosition; }


        public float AnglePosition { get => _anglePosition; }
        public float Radius { get => _radius; }
        public Vector3 Center { get; private set; }


        [SerializeField] private float _anglePosition;
        [SerializeField] private float _radius;


        private Transform _transform;


        public void SetPosition(float value)
        {
            SetValueAndApplyPosition(ref _anglePosition, value);
        }


        public void SetRadius(float value)
        {
            SetValueAndApplyPosition(ref _radius, value);
        }



        public Vector3 CalculatePoint(float position)
        {
            Vector3 point = new Vector3();
            point.x = Mathf.Sin(position);
            point.z = Mathf.Cos(position);
            point *= _radius;
            return point;
        }


        private void SetValueAndApplyPosition<T>(ref T field, T value)
        {
            field = value;
            ApplyPosition(_anglePosition, _radius);
        }


        private void ApplyPosition(float angle, float radius)
        {
            _transform.localPosition = CalculatePoint(_anglePosition);
        }


        private void Start()
        {
            _transform = transform;
            ApplyPosition(_anglePosition, _radius);
            ActualizeCenter();
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


        private void ActualizeCenter()
        {
            if (!_transform.parent)
            {
                Center = Vector3.zero;
                return;
            }
            Center = _transform.parent.position;
        }
    }
}