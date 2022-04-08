using UnityEngine;
using System;
using Systemagedon.App.Extensions;
using Random = UnityEngine.Random;

namespace Systemagedon.App.Gameplay
{

    [CreateAssetMenu(menuName = "Systemagedon/StarSystemGenerator Config")]
    public class StarSystemGenerator : ScriptableObject
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


        public StarSystem GenerateAndSpawn(int planets)
        {
            if (planets < 0)
            {
                throw new ArgumentOutOfRangeException("planets");
            }
            if (planets > CalculateMaxPlanets())
            {
                throw new ArgumentOutOfRangeException("Planets can't be greater " +
                    "than max planets generator can generate with given config");
            }
            Planet[] generatedPlanets = new Planet[planets];
            RangeFloat freeRadius = new RangeFloat
            {
                Min = _starSize.Min + _minPlanetsDistance + CalculateMinRadiusFor(planets - 1),
                Max = _maxRadius
            };
            for (int i = 0; i < planets; i++)
            {
                Planet selectedPrefab = _planetPrefabs.SelectRandom();
                //Planet instance = Planet.InitFrom(selectedPrefab;
                RangeFloat actualFreeRadius = freeRadius;
                actualFreeRadius.Min += _planetSize.Min / 2f;
                actualFreeRadius.Max -= _planetSize.Min / 2f;
                if (i > 0)
                {
                    actualFreeRadius.Max -= _minPlanetsDistance;
                }
                float orbitRadius = actualFreeRadius.SelectRandom();

                float freeRadiusBefore = orbitRadius - freeRadius.Min;
                float freeRadiusAfter = freeRadius.Max - orbitRadius;
                float minFreeSpace = Mathf.Min(freeRadiusAfter, freeRadiusBefore);
                RangeFloat actualValidScale = _planetSize;
                if (actualValidScale.Max > minFreeSpace * 2)
                {
                    actualValidScale.Max = minFreeSpace * 2;
                }
                float scale = actualValidScale.SelectRandom();

                float velocity = _planetSpeed.SelectRandom();
                if (Random.Range(0, 2) == 1)
                {
                    velocity *= -1;
                }

                float anglePosition = Random.Range(0, Mathf.PI * 2);

                Planet instance = Planet.InitFrom(selectedPrefab, orbitRadius,
                    velocity, scale, anglePosition);
                generatedPlanets[i] = instance;
                freeRadius.Max = orbitRadius - scale / 2;
                freeRadius.Min -= CalculateMinRadiusFor(1);
            }

            RangeFloat actualStarSize = _starSize;
            if (actualStarSize.Max > freeRadius.Max)
            {
                actualStarSize.Max = freeRadius.Max;
            }
            float starScale = actualStarSize.SelectRandom();
            Star selectedStarPrefab = _starPrefabs.SelectRandom();
            Star starInstance = Instantiate(selectedStarPrefab);
            starInstance.Init(starScale);

            StarSystem generated = new GameObject().AddComponent<StarSystem>();
            generated.Init(generatedPlanets, starInstance);
            return generated;
        }


        public int CalculateMaxPlanets()
        {
            float leftRadius = _maxRadius;
            leftRadius -= _starSize.Min + _minPlanetsDistance;
            float planetWithDistanse = _planetSize.Min + _minPlanetsDistance;
            return (int)(leftRadius / planetWithDistanse);
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


        private float CalculateMinRadiusFor(int planets)
        {
            return (_planetSize.Min + _minPlanetsDistance) * planets;
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
