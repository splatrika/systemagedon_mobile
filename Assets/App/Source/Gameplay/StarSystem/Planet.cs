using UnityEngine;
using System;
using Systemagedon.App.StarSystem;
using Systemagedon.App.Movement;

namespace Systemagedon.App.Gameplay
{

    public class Planet : MonoBehaviour, IDash, IRoundPath, IMovable
    {
        public event Action<Planet> Ruined;


        public float Radius { get => _radius; }
        public Vector3 Center { get => _orbit.Center; }


        [SerializeField] private float _radius = 1;
        [SerializeField] private float _velocity = 1;
        [SerializeField] private Dash.PropertiesFields _dashProperties =
            Dash.PropertiesFields.Default;


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
            Start();
            Destroy(gameObject);
            Ruined?.Invoke(this);
        }


        public void ApplyDash()
        {
            Start();
            _dash.ApplyDash();
        }


        public Vector3 CalculatePoint(float afterSeconds)
        {
            Start();
            return _movement.CalculatePoint(afterSeconds);
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
