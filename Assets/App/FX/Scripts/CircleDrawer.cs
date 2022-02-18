using UnityEngine;
using Systemagedon.App;


namespace Systemagedon.App.FX
{

    public class CircleDrawer : MonoBehaviour
    {
        public IRounded Target { get => _target; }


        [SerializeField] private GameObject _targetObject;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private uint _pointsCount;


        private IRounded _target;


        private void Start()
        {
            OnValidate();
            ReDraw();
        }


        private void OnValidate()
        {
            if (_targetObject && !TrySetupTarget(_targetObject, out _target))
            {
                _targetObject = null;
                Debug.LogError("Target Object must have a component that implements IRounded");
            }
        }


        private bool TrySetupTarget(in GameObject from, out IRounded to)
        {
            to = from.GetComponent<IRounded>();
            return to != null;
        }


        private void ReDraw()
        {
            Vector3[] points = CalculatePoints(_pointsCount, _target.Radius);
            MovePoints(ref points, _target.Center); 
            _lineRenderer.positionCount = points.Length;
            _lineRenderer.SetPositions(points);
        }


        private Vector3[] CalculatePoints(uint count, float radius)
        {
            Vector3[] points = new Vector3[count];
            for (int i = 0; i < count; i++)
            {
                float angle = Mathf.PI * 2f * ((float)i / (float)count);
                points[i].x = Mathf.Sin(angle);
                points[i].z = Mathf.Cos(angle);
                points[i] *= radius;
            }
            return points;
        }


        private void MovePoints(ref Vector3[] points, in Vector3 offset)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] += offset;
            }
        }
    }

}
