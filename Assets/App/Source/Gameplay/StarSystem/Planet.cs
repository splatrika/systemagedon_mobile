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


        public void Init(float orbitRadius, float velocity, float scale = 1,
            float anglePosition = 0)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _radius = orbitRadius;
            _velocity = velocity;
            _orbit.SetRadius(_radius);
            _orbit.SetPosition(anglePosition);
            _movement.Init(_orbit, _velocity);
            _dash.Init(_movement, _dashProperties);
            transform.localScale = Vector3.one * scale;
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
            return _movement.CalculatePoint(afterSeconds, false);
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
                Init(_radius, _velocity, transform.localScale.x);
            }
        }


        private void OnDestroy()
        {
            Destroy(_orbit);
            Destroy(_movement);
            Destroy(_dash);
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }

}
