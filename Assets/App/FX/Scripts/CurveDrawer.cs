using UnityEngine;
using System.Collections.Generic;
using Systemagedon.App.Movement;
using System.Linq;

namespace Systemagedon.App.FX
{

    public class CurveDrawer : MonoBehaviour
    {
        [SerializeField] private CurveTransform _target;
        [SerializeField] private LineRenderer _lineRenderer;


        private void OnEnable()
        {
            _target.CurveChanged += OnCurveChangded;
            _target.CurveOffsetChanged += OnCurveOffsetChanged;
            Redraw();
        }


        private void OnDisable()
        {
            _target.CurveChanged -= OnCurveChangded;
            _target.CurveOffsetChanged -= OnCurveOffsetChanged;
        }


        private void OnCurveChangded()
        {
            Redraw();
        }


        private void OnCurveOffsetChanged()
        {
            Redraw();
        }


        private void Redraw()
        {
            List<Vector3> points = new List<Vector3>();
            Vector3 offset = _target.GetCurveOffset();
            IEnumerable<CurveTransform.CurveSegment> segments =
                _target.BakedSegments;
            points.Add(segments.ElementAt(0).StartPoint + offset);
            foreach (CurveTransform.CurveSegment segment in segments)
            {
                points.Add(segment.EndPoint + offset);
            }
            _lineRenderer.positionCount = points.Count;
            _lineRenderer.SetPositions(points.ToArray());
        }
    }

}
