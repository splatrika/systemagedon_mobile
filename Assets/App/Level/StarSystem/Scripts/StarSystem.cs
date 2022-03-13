using UnityEngine;
using System.Collections;
using Systemagedon.App.Movement;
using System;
using System.Collections.Generic;

namespace Systemagedon.App.StarSystem
{
    public class StarSystem : MonoBehaviour, IDashesProvider
    {
        public event Action DashesListUpdated;


        [SerializeField] private Planet[] _planets;


        private Dash[] _planetDashes;


        public IEnumerable<Planet> GetPlanets()
        {
            return _planets;
        }


        public IEnumerable<Dash> GetDashes()
        {
            return _planetDashes;
        }


        private void Start()
        {
            _planetDashes = FindPlanetDashes();
            DashesListUpdated?.Invoke();
        }


        private Dash[] FindPlanetDashes()
        {
            Dash[] finded = new Dash[_planets.Length];
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