using UnityEngine;
using System.Collections.Generic;
using Systemagedon.App.Movement;
using System.Linq;
using System;

namespace Systemagedon.App.Drawing
{

    /// <summary>
    /// Init from script required
    /// </summary>
    public class CurveDrawer : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private ICurvePath _target;
        private bool _inited = false;


        public void Init(ICurvePath target, LineRenderer lineRenderer,
            uint segmentsCount)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _lineRenderer = lineRenderer;
            _target = target;
            Draw(segmentsCount);
            _inited = true;
        }


        public void ChangeColor(Color color)
        {
            CheckInit();
            _lineRenderer.Fill(color);
        }


        private void Start()
        {
            CheckInit();
        }


        private void Draw(uint segmentsCount)
        {
            if (segmentsCount == 0)
            {
                throw new ArgumentException("segmentsCount can't equals zero");
            }
            Vector3[] points = new Vector3[segmentsCount];
            for (int i = 0; i < segmentsCount; i++)
            {
                float t = (float)i / segmentsCount;
                points[i] = _target.Path.CalculatePoint(t);
            }
            _lineRenderer.positionCount = (int)segmentsCount;
            _lineRenderer.SetPositions(points);
        }


        private void CheckInit()
        {
            if (!_inited)
            {
                throw new InvalidOperationException("Asteroid must inited " +
                    "from script!");
            }
        }



    }

}
