using UnityEngine;
using System.Collections;
using System;

namespace Systemagedon.App.Movement
{

    [ExecuteInEditMode]
    public class CurveMovement : MonoBehaviour, IOneAxisTransform
    {
        [Serializable]
        private struct CurveSegment
        {
            public Vector3 StartPoint { get; set; }
            public Vector3 EndPoint { get; set; }
            public float StartPosition { get; set; }
            public float EndPosition { get; set; }
        }


        public Bezier Curve { get => _curve; }
        public float Position { get => _position; }
        public float Length { get => _length; }


        [SerializeField] private Bezier _curve;
        [SerializeField] private float _position;


        private CurveSegment[] _segments;
        private const int c_SegemntsCount = 20;


        private float _length;
        private Transform _transform;


        public void ChangeCurve(Bezier curve)
        {
            _curve = curve;
            BakeCurve();
            ApplyTransform();
        }


        public void SetPosition(float value)
        {
            _position = value;
            ApplyTransform();
        }


        public Vector3 CalculatePoint(float position)
        {
            if (_position > _length)
            {
                throw new ArgumentOutOfRangeException("Position out of curve length range");
            }
            CurveSegment segment = Array.Find(_segments, segment =>
            {
                return segment.StartPosition <= _position
                    && segment.EndPosition >= _position;
            });
            float localT = (_position - segment.StartPosition) /
                (segment.EndPosition - segment.StartPosition);
            if (float.IsNaN(localT))
            {
                localT = 0;
            }
            return Vector3.Lerp(segment.StartPoint, segment.EndPoint, localT);
        }


        private void OnEnable()
        {
            _transform = transform;
            BakeCurve();
            ApplyTransform();
        }


        private void OnValidate()
        {
            BakeCurve();
        }


        private void ApplyTransform()
        {
            _transform.localPosition = CalculatePoint(_position);
        }


        private void BakeCurve()
        {
            float totalLength = 0;
            _segments = new CurveSegment[c_SegemntsCount];
            for (int i = 0; i < c_SegemntsCount; i++)
            {
                CurveSegment segment = new CurveSegment();
                float startT = (float)i / (c_SegemntsCount);
                float endT = (i + 1f) / (c_SegemntsCount);
                segment.StartPoint = _curve.CalculatePoint(startT);
                segment.EndPoint = _curve.CalculatePoint(endT);
                segment.StartPosition = totalLength;
                totalLength += Vector3.Distance(segment.StartPoint, segment.EndPoint);
                segment.EndPosition = totalLength;
                _segments[i] = segment;
            }
            _length = totalLength;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Vector3 parentPosition = (transform.parent)
                ? transform.parent.position
                : Vector3.zero;
            foreach (CurveSegment segment in _segments)
            {
                Vector3 firstPoint = segment.StartPoint;
                Vector3 secondPoint = segment.EndPoint;
                Gizmos.DrawLine(firstPoint + parentPosition,
                    secondPoint + parentPosition);
            }
        }
    }


    [Serializable]
    public struct Bezier
    {
        public Vector3 PointA;
        public Vector3 LerpA;
        public Vector3 PointB;
        public Vector3 LerpB;


        public Vector3 CalculatePoint(float t)
        {
            Vector3 subPointA = Vector3.Lerp(PointA, LerpA, t);
            Vector3 subPointB = Vector3.Lerp(LerpA, LerpB, t);
            Vector3 subPointC = Vector3.Lerp(LerpB, PointB, t);

            Vector3 finishPointA = Vector3.Lerp(subPointA, subPointB, t);
            Vector3 finishPointB = Vector3.Lerp(subPointB, subPointC, t);
            return Vector3.Lerp(finishPointA, finishPointB, t);
        }
    }

}
