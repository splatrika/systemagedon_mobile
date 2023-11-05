using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Systemagedon.App.Extensions;
using Systemagedon.App.Services;
using Random = UnityEngine.Random;

namespace Systemagedon.App.Gameplay
{

    [CreateAssetMenu(menuName = "Systemagedon/StarSystemGenerator Config")]
    public class StarSystemGeneratorLegacy : ScriptableObject
    {
        [SerializeField] private float _maxRadius;
        [Header("Planets")]
        [SerializeField] private Planet[] _planetPrefabs;
        [SerializeField] private RangeFloat _planetSize;
        [SerializeField] private RangeFloat _planetSpeed;
        [SerializeField] private float _minPlanetsDistance;
        [Header("Star")]
        [SerializeField] private Star[] _starPrefabs;
        [SerializeField] private RangeFloat _starSize;

        public IReadOnlyCollection<Planet> PlanetPrefabs => _planetPrefabs;
        public IReadOnlyCollection<Star> StarPrefabs => _starPrefabs;


        public StarSystemSettings ParseSettings()
        {
            return new StarSystemSettings(
                _maxRadius,
                new PlanetSettings(
                    _planetPrefabs.Length,
                    _planetSize,
                    _planetSpeed,
                    _minPlanetsDistance),
                new StarSettings(
                    _starPrefabs.Length,
                    _starSize));
        }


        public StarSystem GenerateAndSpawn(int planets)
        {
            var settings = ParseSettings();

            var generator = new StarSystemGenerator(settings);
            var generatedSnapshot = generator.Generate(planets);
            var generatedPlanets = generatedSnapshot.Planets
                .Select(p =>
                    Planet.InitFrom(
                        _planetPrefabs[p.PrefabIndex],
                        p.OrbitRadius,
                        p.Velocity,
                        p.Scale,
                        p.AnglePosition))
                .ToArray();

            var selectedStarPrefab = _starPrefabs[generatedSnapshot.Star.PrefabIndex];
            Star starInstance = Instantiate(selectedStarPrefab);
            starInstance.Init(generatedSnapshot.Star.Scale);

            StarSystem generated = new GameObject().AddComponent<StarSystem>();
            generated.Init(generatedPlanets, starInstance);
            return generated;
        }


        public int CalculateMaxPlanets()
        {
            var settings = ParseSettings();

            var generator = new StarSystemGenerator(settings);

            return generator.CalculateMaxPlanets();
        }


        public void DrawGizmos()
        {
            float firstPlanetOrbit = _maxRadius - _planetSize.Max / 2f;
            float secondPlanetOrbit = firstPlanetOrbit - _planetSize.Max / 2f
                - _minPlanetsDistance - _planetSize.Min / 2f;
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(Vector3.zero, _starSize.Min / 2f);
            Gizmos.color = Color.white;
            DrawOrbit(firstPlanetOrbit);
            DrawOrbit(secondPlanetOrbit);
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(Vector3.forward * firstPlanetOrbit, _planetSize.Max / 2f);
            Gizmos.DrawSphere(Vector3.forward * secondPlanetOrbit, _planetSize.Min / 2f);
        }


        private void DrawOrbit(float radius)
        {
            const int points = 20;
            for (int i = 0; i < points; i++)
            {
                float angle1 = (float)i / points * Mathf.PI * 2f;
                float angle2 = (float)(i + 1) / points * Mathf.PI * 2f;
                Vector3 point1 = new Vector3(Mathf.Sin(angle1), 0, Mathf.Cos(angle1)) * radius;
                Vector3 point2 = new Vector3(Mathf.Sin(angle2), 0, Mathf.Cos(angle2)) * radius;
                Gizmos.DrawLine(point1, point2);
            }
        }
    }

}
