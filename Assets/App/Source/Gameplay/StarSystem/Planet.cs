using UnityEngine;
using System;
using Systemagedon.App.StarSystem;
using Systemagedon.App.Movement;

namespace Systemagedon.App.Gameplay
{

    public class Planet : MonoBehaviour, IDash, IRoundPath
    {
        public event Action Ruined;


        public float Radius { get => _radius; }
        public Vector3 Center { get => _orbit.Center; }


        [SerializeField] private float _radius;
        [SerializeField] private float _velocity;
        [SerializeField] private Dash.PropertiesFields _dashProperties;


        private OrbitTransform _orbit;
        private OneAxisMovement _movement;
        private Dash _dash;
        private bool _inited = false;


        public void Init(float radius, float velocity)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _radius = radius;
            _velocity = velocity;
            _orbit.SetRadius(_radius);
            _movement.Init(_orbit, _velocity);
            _dash.Init(_movement, _dashProperties);
            _inited = true;
        }


        public void Ruin()
        {
            Destroy(gameObject);
            Ruined?.Invoke();
        }


        public void ApplyDash()
        {
            _dash.ApplyDash();
        }


        private void Awake()
        {
            _orbit = gameObject.AddComponent<OrbitTransform>();
            _movement = gameObject.AddComponent<OneAxisMovement>();
            _dash = gameObject.AddComponent<Dash>();
        }


        private void Start()
        {
            if (!_inited)
            {
                Init(_radius, _velocity);
            }
        }


    }

}
