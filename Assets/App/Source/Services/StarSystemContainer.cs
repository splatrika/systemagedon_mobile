using System;
using System.Collections.Generic;
using System.Linq;
using Systemagedon.App.Gameplay;
using UnityEngine;

namespace Systemagedon.App.Services
{
    public class StarSystemContainer : MonoBehaviour, IStarSystemContainer, ILegacyStarSystemProvider, IDashesProvider
    {
        [SerializeField]
        private StarSystemGeneratorLegacy _settings;

        private Systemagedon.App.Gameplay.StarSystem _current;

        public IReadOnlyCollection<Planet> Planets => _current?.Planets;
        public IReadOnlyCollection<IDash> Dashes => _current?.Dashes;

        public event Action<Planet> SomePlanetRuined;
        public event Action<ILegacyStarSystemProvider> ModelUpdated;

        public void Load(StarSystemSnapshot snapshot)
        {
            TryDestroyCurrent();
            SpawnNew(snapshot);
            ModelUpdated?.Invoke(this);
        }

        private void TryDestroyCurrent()
        {
            if (_current != null)
            {
                _current.SomePlanetRuined -= OnPlanetRuined;
                Destroy(_current);
            }
        }

        private void SpawnNew(StarSystemSnapshot snapshot)
        {
            _current = Spawn(snapshot);
            _current.SomePlanetRuined += OnPlanetRuined;
        }

        private void OnPlanetRuined(Planet planet)
        {
            SomePlanetRuined?.Invoke(planet);
        }

        private Systemagedon.App.Gameplay.StarSystem Spawn(StarSystemSnapshot snapshot)
        {
            var generatedPlanets = snapshot.Planets
                .Select(p =>
                    Planet.InitFrom(
                        _settings.PlanetPrefabs.ElementAt(p.PrefabIndex),
                        p.OrbitRadius,
                        p.Velocity,
                        p.Scale,
                        p.AnglePosition))
                .ToArray();

            var genereatedStar = Instantiate(_settings.StarPrefabs.ElementAt(snapshot.Star.PrefabIndex));
            genereatedStar.Init(snapshot.Star.Scale);

            var starSystem = new GameObject().AddComponent<Systemagedon.App.Gameplay.StarSystem>();
            starSystem.Init(generatedPlanets, genereatedStar);

            return starSystem;
        }

        private void OnDestroy()
        {
            TryDestroyCurrent();
        }
    }
}