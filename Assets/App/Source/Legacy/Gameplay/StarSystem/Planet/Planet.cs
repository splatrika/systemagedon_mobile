using UnityEngine;
using System;
using Systemagedon.App.StarSystem;
using Systemagedon.App.Movement;

namespace Systemagedon.App.Gameplay
{
    /// <summary>
    /// Only init from prefab allowed. Use Planet.InitFrom
    /// </summary>
    public class Planet : MonoBehaviour, IDash, IRoundPath, IMovable, IPausable
    {
        public event Action<Planet> Ruined;
        public event Action<Planet, PlanetRuins> RuinedAdvanced;


        public float Radius { get => _radius; }
        public Vector3 Center { get => _orbit.Center; }
        public Planet OriginalPrefab { get => _originalPrefab; }
        public float Velocity { get => _velocity; }
        public float Scale { get => transform.localScale.x; }
        public float AnglePosition { get => _orbit.AnglePosition; }


        [SerializeField] private float _radius = 1;
        [SerializeField] private float _velocity = 1;
        [SerializeField] private Dash.PropertiesFields _dashProperties =
            Dash.PropertiesFields.Default;
        [SerializeField] private PlanetRuins _ruinsPrefab;


        private OrbitTransform _orbit;
        private OneAxisMovement _movement;
        private Dash _dash;
        private Planet _originalPrefab;
        private bool _inited = false;


        public static Planet InitFrom(Planet prefab, float orbitRadius, float velocity, float scale = 1,
            float anglePosition = 0)
        {
            Planet instance = Instantiate(prefab);
            instance._originalPrefab = prefab;
            instance._radius = orbitRadius;
            instance._velocity = velocity;
            instance._orbit.SetRadius(instance._radius);
            instance._orbit.SetPosition(anglePosition);
            instance._movement.Init(instance._orbit, instance._velocity);
            instance._dash.Init(instance._movement, instance._dashProperties);
            instance.transform.localScale = Vector3.one * scale;
            instance._inited = true;
            return instance;
        }


        public void Ruin()
        {
            Start();
            Destroy(gameObject);
            Explosions.Notify(transform);
            Ruined?.Invoke(this);
            PlanetRuins ruins = PlanetRuins.InitFrom(_ruinsPrefab, this);
            RuinedAdvanced?.Invoke(this, ruins);
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


        public void Pause()
        {
            _movement.Pause();
        }


        public void Resume()
        {
            _movement.Resume();
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
                Debug.LogError("Only init from prefab allowed. Use Planet.InitFrom");
            }
            if (!_ruinsPrefab)
            {
                throw new NullReferenceException(nameof(_ruinsPrefab));
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
