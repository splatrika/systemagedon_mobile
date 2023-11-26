using UnityEngine;
using System;
using System.Collections.Generic;

namespace Systemagedon.App.Gameplay
{

    public class StarSystem : MonoBehaviour, ILegacyStarSystemProvider, IDashesProvider,
        IPausable
    {
        public event Action<Planet> SomePlanetRuined;
        public event Action<ILegacyStarSystemProvider> ModelUpdated;


        public IReadOnlyCollection<Planet> Planets { get => GetPlanets(); }
        public IReadOnlyCollection<IDash> Dashes { get => GetPlanets(); }


        [SerializeField] private Planet[] _planetsInspector;
        [SerializeField] private Star _starInspector;


        private List<Planet> _planets;
        private Star _star;
        private bool _paused;
        private bool _inited;


        public void Init(Planet[] planets, Star star)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _planets = new List<Planet>(planets);
            foreach (Planet planet in _planets)
            {
                OnPlanetAdd(planet);
            }
            _planetsInspector = planets;
            _star = star;
            _star.transform.position = transform.position;
            _star.transform.SetParent(transform);
            _inited = true;
        }


        public IReadOnlyCollection<Planet> GetPlanets()
        {
            Start();
            return _planets;
        }


        public void AddPlanet(Planet planet)
        {
            if (!planet)
            {
                throw new NullReferenceException(nameof(planet));
            }
            if (_paused)
            {
                planet.Pause();
            }
            OnPlanetAdd(planet);
            Planet alreadyAdded = _planets.Find((item) => item == planet);
            if (alreadyAdded != null)
            {
                throw new InvalidOperationException("This planet already added " +
                    "to star system");
            }
            _planets.Add(planet);
            ModelUpdated?.Invoke(this);
        }


        public void Pause()
        {
            _paused = true;
            _planets.ForEach(planet => planet.Pause());
        }


        public void Resume()
        {
            _paused = false;
            _planets.ForEach(planet => planet.Resume());
        }


        private void Awake()
        {
            if (_planetsInspector != null)
                _planets = new List<Planet>(_planetsInspector);
        }


        private void OnPlanetAdd(Planet planet)
        {
            planet.Ruined += OnPlanetRuined;
            planet.transform.SetParent(transform);
        }


        private void OnDestroy()
        {
            foreach (Planet planet in _planets)
            { //TODO fix missing asset
                planet.Ruined -= OnPlanetRuined;
                Destroy(planet.gameObject);
            }
            Destroy(_star.gameObject);
        }


        private void OnPlanetRuined(Planet sender)
        {
            _planets.Remove(sender);
            sender.Ruined -= OnPlanetRuined;
            SomePlanetRuined?.Invoke(sender);
        }


        private void Start()
        {
            if (!_inited)
            {
                Init(_planetsInspector, _starInspector);
                ModelUpdated?.Invoke(this);
            }
        }
    }

}
