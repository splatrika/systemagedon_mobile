using UnityEngine;
using System.Collections;
using Systemagedon.App.Movement;
using System;
using System.Collections.Generic;

namespace Systemagedon.App.StarSystem
{
    public class StarSystem : MonoBehaviour, IDashesProviderLegacy
    {
        public event Action DashesListUpdated;


        [SerializeField] private Planet[] _planets;


        private LegacyDash[] _planetDashes;


        public IEnumerable<Planet> GetPlanets()
        {
            return _planets;
        }


        public IEnumerable<LegacyDash> GetDashes()
        {
            return _planetDashes;
        }


        private void Start()
        {
            _planetDashes = FindPlanetDashes();
            DashesListUpdated?.Invoke();
        }


        private LegacyDash[] FindPlanetDashes()
        {
            LegacyDash[] finded = new LegacyDash[_planets.Length];
            int i = 0;
            foreach (Planet planet in _planets)
            {
                finded[i] = planet.Dash;
                i++;
            }
            return finded;
        }
    }
}