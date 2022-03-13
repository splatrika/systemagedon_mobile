﻿using UnityEngine;
using System;
using Systemagedon.App.Collisions;
using Systemagedon.App.Movement;

namespace Systemagedon.App.StarSystem
{

    public class Asteroid : MonoBehaviour
    {
        public event Action PathModified;
        public event Action Destroyed;


        public Ruiner Ruiner { get => _ruiner; }
        public CurveTransform Path { get => _path; }
        public AsteroidMovement Movement { get => _movement; }
        public float CrossPosition { get => _crossPositon; }
        public Vector3 CrossPoint { get => _crossPoint; }


        [SerializeField] private Ruiner _ruiner;
        [SerializeField] private CurveTransform _path;
        [SerializeField] private AsteroidMovement _movement;


        private float _crossPositon;
        private Vector3 _crossPoint;
        private bool _crossed = false;


        private void Start()
        {
            Validate();
        }


        private void OnEnable()
        {
            Path.CurveChanged += OnPathModified;
            Path.CurveOffsetChanged += OnPathModified;
            ActualizeCrossData();
        }


        private void OnDisable()
        {
            Path.CurveChanged -= OnPathModified;
            Path.CurveOffsetChanged -= OnPathModified;
        }


        private void OnDestroy()
        {
            Destroyed?.Invoke();
        }


        private void Validate()
        {
            bool movementIsNotValid = _movement.Target != this;
            if (movementIsNotValid)
            {
                _movement = null;
                Debug.LogError("Target of movement must be this asteroid");
            }
        }


        private void ActualizeCrossData()
        {
            _crossPositon = _path.Length / 2;
            _crossPoint = _path.CalculatePoint(_crossPositon);
        }


        private void OnPathModified()
        {
            ActualizeCrossData();
            PathModified?.Invoke();
        }
    }

}