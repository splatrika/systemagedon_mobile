using UnityEngine;
using System;

namespace Systemagedon.App.FX
{

    /// <summary>
    /// Init from script requaired
    /// </summary>
    public class CircleDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private uint _segments;
        private IRoundPath _target;
        private bool _inited = false;


        public void Init(IRoundPath target)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _target = target;
            _inited = true;
        }


        private void Awake()
        {
            if (!_lineRenderer)
            {
                Debug.LogError("LineRenderer must be assigned");
            }
        }


        private void Start()
        {
            if (!_inited)
            {
                Debug.LogError("Instance must be inited from script");
            }
            Draw();
        }


        private void Draw()
        {
            Vector3[] points = CalculatePoints(_segments, _target.Radius);
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
