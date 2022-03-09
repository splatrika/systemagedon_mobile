using UnityEngine;
using System.Collections;
using System;
using Systemagedon.App.Collisions;
using Systemagedon.App.Movement;

namespace Systemagedon.App.StarSystem
{

    public class Asteroid : MonoBehaviour
    {
        public event Action PathModified;


        public Ruiner Ruiner { get => _ruiner; }
        public CurveTransform Path { get => _path; }
        public OneAxisMovement Movement { get => _movement; }


        [SerializeField] private Ruiner _ruiner;
        [SerializeField] private CurveTransform _path;
        [SerializeField] private OneAxisMovement _movement;


        private void Start()
        {
            Validate();
        }


        private void OnEnable()
        {
            Path.CurveChanged += CallModified;
            Path.CurveOffsetChanged += CallModified;
        }


        private void OnDisable()
        {
            Path.CurveChanged -= CallModified;
            Path.CurveOffsetChanged -= CallModified;
        }


        private void Validate()
        {
            bool movementIsNotValid = _movement.Target != _path;
            if (movementIsNotValid)
            {
                _movement = null;
                Debug.LogError("Target of movement of this asteroid " +
                    "must be transform that assigned to this asteroid");
            }
        }


        private void CallModified()
        {
            PathModified?.Invoke();
        }
    }

}
