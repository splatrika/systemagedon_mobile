using System;
using Systemagedon.App.Gameplay;
using UnityEngine;

namespace Systemagedon.App.Services
{
	public class StarSystemGenerator : IStarSystemGenerator
	{
        private readonly StarSystemSettings _settings;

        public StarSystemGenerator(StarSystemSettings settings)
        {
            _settings = settings;
        }

        public StarSystemSnapshot Generate(int planets)
        {
            if (planets < 0)
                throw new InvalidOperationException("Planets count should be positive value");

            if (planets > CalculateMaxPlanets())
            {
                throw new InvalidOperationException("Planets can't be greater " +
                    "than max planets generator can generate with given config");
            }

            var generatedPlanets = new PlanetSnapshot[planets];
            var freeRadius = new RangeFloat
            {
                Min = _settings.Star.Size.Min + _settings.Planet.MinDistance + CalculateMinRadiusFor(planets - 1),
                Max = _settings.MaxRadius
            };

            for (int i = 0; i < planets; i++)
            {
                var selectedPrefab = UnityEngine.Random.Range(0, _settings.Planet.PrefabsCount); // todo use service
                var actualFreeRadius = freeRadius;
                actualFreeRadius.Min += _settings.Planet.Size.Min / 2f;
                actualFreeRadius.Max -= _settings.Planet.Size.Min / 2f;
                if (i > 0)
                {
                    actualFreeRadius.Max -= _settings.Planet.MinDistance;
                }
                var orbitRadius = actualFreeRadius.SelectRandom();

                var freeRadiusBefore = orbitRadius - freeRadius.Min;
                var freeRadiusAfter = freeRadius.Max - orbitRadius;
                var minFreeSpace = Mathf.Min(freeRadiusAfter, freeRadiusBefore);
                var actualValidScale = _settings.Planet.Size;
                if (actualValidScale.Max > minFreeSpace * 2)
                {
                    actualValidScale.Max = minFreeSpace * 2;
                }
                var scale = actualValidScale.SelectRandom();

                float velocity = _settings.Planet.Speed.SelectRandom();
                if (UnityEngine.Random.Range(0, 2) == 1) // todo use service
                {
                    velocity *= -1;
                }

                var anglePosition = UnityEngine.Random.Range(0, Mathf.PI * 2); // todo use service

                var planet = new PlanetSnapshot(selectedPrefab, orbitRadius, velocity, scale, anglePosition);
                generatedPlanets[i] = planet;
                freeRadius.Max = orbitRadius - scale / 2;
                freeRadius.Min -= CalculateMinRadiusFor(1);
            }

            var actualStarSize = _settings.Star.Size;
            if (actualStarSize.Max > freeRadius.Max)
            {
                actualStarSize.Max = freeRadius.Max;
            }
            var starScale = actualStarSize.SelectRandom();
            var selectedStarPrefab = UnityEngine.Random.Range(0, _settings.Star.PrafabsCount); // todo use service
            var generatedStar = new StarSnapshot(selectedStarPrefab, starScale);

            return new StarSystemSnapshot(generatedPlanets, generatedStar);
        }

        public int CalculateMaxPlanets()
        {
            float leftRadius = _settings.MaxRadius;
            leftRadius -= _settings.Star.Size.Min + _settings.Planet.MinDistance;
            float planetWithDistanse = _settings.Planet.Size.Min + _settings.Planet.MinDistance;
            return (int)(leftRadius / planetWithDistanse);
        }

        private float CalculateMinRadiusFor(int planetsCount)
        {
            return (_settings.Planet.Size.Min + _settings.Planet.MinDistance) * planetsCount;
        }
    }
}

