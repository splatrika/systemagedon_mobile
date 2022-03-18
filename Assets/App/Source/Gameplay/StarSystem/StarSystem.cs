using UnityEngine;
using System;
using System.Collections.Generic;

namespace Systemagedon.App.Gameplay
{

    public class StarSystem : MonoBehaviour, IDashesProvider
    {
        public event Action<Planet> SomePlanetRuined;


        public IEnumerable<Planet> Planets { get => _planets; }
        public IEnumerable<IDash> Dashes { get => _planets; }


        [SerializeField] private Planet[] _planetsInspector;


        private Planet[] _planets;
        private bool _inited;


        public void Init(Planet[] planets)
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
            _inited = true;
        }


        private void Awake()
        {
            _planets = _planetsInspector;
        }


        private void OnDestroy()
        {
            foreach (Planet planet in _planets)
            {
                planet.Ruined -= OnPlanetRuined;
            }
        }


        private void OnPlanetRuined(Planet sender)
        {
            SomePlanetRuined?.Invoke(sender);
        }


        private void Start()
        {
            if (!_inited)
            {
                Init(_planetsInspector);
            }
        }
    }

}
