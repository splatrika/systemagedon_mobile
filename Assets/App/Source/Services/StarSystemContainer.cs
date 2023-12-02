using System;
using System.Collections.Generic;
using System.Linq;
using Systemagedon.App.Configuration;
using Systemagedon.App.Gameplay;
using UnityEngine;

namespace Systemagedon.App.Services
{
    public class StarSystemContainer : MonoBehaviour, IStarSystemContainer, ILegacyStarSystemProvider, IDashesProvider
    {
        [SerializeField]
        private StarSystemConfiguration _settings;

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

        public StarSystemSnapshot Read()
        {
            if (!_current) throw new InvalidOperationException("There is no star system");

            return new StarSystemSnapshot(
                _current.Planets
                    .Select(x => new PlanetSnapshot(
                        IndexOf(_settings.PlanetPrefabs, x.Prefab),
                        x.Radius,
                        x.Velocity, // todo maybe bugs while dash. Fix
                        x.Scale,
                        x.AnglePosition))
                    .ToArray(),
                new StarSnapshot(
                    IndexOf(_settings.StarPrefabs, _current.Star.Prefab),
                    _current.Star.Scale));
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

            var starPrefab = _settings.StarPrefabs.ElementAt(snapshot.Star.PrefabIndex);
            var genereatedStar = Star.InitFrom(starPrefab, snapshot.Star.Scale);

            var starSystem = new GameObject().AddComponent<Systemagedon.App.Gameplay.StarSystem>();
            starSystem.Init(generatedPlanets, genereatedStar);

            return starSystem;
        }

        private int IndexOf<T>(IReadOnlyCollection<T> collection, T element)
        {
            return collection.TakeWhile(x => !x.Equals(element)).Count();
        }

        private void OnDestroy()
        {
            TryDestroyCurrent();
        }
    }
}