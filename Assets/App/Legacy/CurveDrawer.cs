using UnityEngine;
using System.Collections.Generic;
using Systemagedon.App.Movement;
using System.Linq;
using System;

namespace Systemagedon.App.FX
{

    public class CurveDrawer : MonoBehaviour
    {
        [SerializeField] private CurveTransform _target;
        [SerializeField] private LineRenderer _lineRenderer;


        public void Init(CurveTransform target, LineRenderer renderer)
        {
            if (_lineRenderer != null || _target != null)
            {
                throw new InvalidOperationException("Instance already initialized");
            }
            if (!target)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (!renderer)
            {
                throw new ArgumentNullException(nameof(renderer));
            }
            _lineRenderer = renderer;
            _target = target;
            SubscribeEvents();
            Redraw();
        }


        public void ChangeColor(Color color)
        {
            _lineRenderer.Fill(color);
        }


        private void OnEnable()
        {
            if (_target)
            {
                SubscribeEvents();
                Redraw();
            }
        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }


        private void Start()
        {
            Validate();
        }


        private void SubscribeEvents()
        {
            _target.CurveChanged += Redraw;
            _target.CurveOffsetChanged += Redraw;
        }


        private void UnsubscribeEvents()
        {
            _target.CurveChanged -= Redraw;
            _target.CurveOffsetChanged -= Redraw;
        }


        private void Validate()
        {
            if (!_target)
            {
                Debug.LogError("_target must be assigned from inspector or on init");
            }
            if (!_lineRenderer)
            {
                Debug.LogError("_lineRenderer must be assigned from inspector " +
                    "or on init");
            }
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
