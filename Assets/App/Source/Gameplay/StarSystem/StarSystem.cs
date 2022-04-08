using UnityEngine;
using System;
using System.Collections.Generic;

namespace Systemagedon.App.Gameplay
{

    public class StarSystem : MonoBehaviour, IStarSystemProvider, IDashesProvider
    {
        public event Action<Planet> SomePlanetRuined;
        public event Action<IStarSystemProvider> ModelUpdated;


        public IEnumerable<Planet> Planets { get => GetPlanets(); }
        public IEnumerable<IDash> Dashes { get => GetPlanets(); }


        [SerializeField] private Planet[] _planetsInspector;
        [SerializeField] private Star _starInspector;


        private Planet[] _planets;
        private Star _star;
        private bool _inited;


        public void Init(Planet[] planets, Star star)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _planets = (Planet[])planets.Clone();
            foreach (Planet planet in _planets)
            {
                planet.Ruined += OnPlanetRuined;
            }
            _planetsInspector = planets;
            _star = star;
            _star.transform.position = transform.position;
            _inited = true;
        }


        public IEnumerable<Planet> GetPlanets()
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
            Planet alreadyAdded = Array.Find(_planets, (item) => item == planet);
            if (alreadyAdded != null)
            {
                throw new InvalidOperationException("This planet already added " +
                    "to star system");
            }
            Array.Resize(ref _planets, _planets.Length + 1);
            _planets[_planets.Length - 1] = planet;
            ModelUpdated?.Invoke(this);
        }


        private void Awake()
        {
            _planets = _planetsInspector;
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
