using UnityEngine;
using System.Collections;
using Systemagedon.App.Movement;
using System.Linq;

namespace Systemagedon.App.StarSystem
{

    public class AsteroidSpawner : FrequencySpawner<Asteroid>
    {
        [SerializeField] private float _topBorder;
        [SerializeField] private float _bottomBorder;
        [SerializeField] private RangeFloat _leverLength;
        [SerializeField] private RangeFloat _asteroidVelocity;
        [SerializeField] private StarSystem _starSystem;


        protected sealed override void SetupOnInstance(Asteroid instance)
        {
            GameObject asteroidOffset = new GameObject();
            Bezier randomCurve = new Bezier();

            randomCurve.PointA.y = _topBorder;
            randomCurve.PointB.y = _bottomBorder;
            MakeRandomLevers(ref randomCurve, _leverLength);
            instance.Transform.ChangeCurve(randomCurve);
            instance.Movement.SetVelocity(
                Random.Range(_asteroidVelocity.Min, _asteroidVelocity.Max));

            Planet targetPlanet = SelectRandomPlanet(_starSystem);
            Vector3 offsetForAsteroid =
                CalculateOffsetToPlanet(targetPlanet, instance);
            instance.transform.SetParent(asteroidOffset.transform);
            asteroidOffset.transform.position = offsetForAsteroid;
        }


        private void MakeRandomLevers(ref Bezier curve, RangeFloat length)
        {
            float leversAngle = Random.Range(0f, Mathf.PI * 2);
            float leversLength = Random.Range(length.Min, length.Min);
            curve.LerpA.y = _topBorder;
            curve.LerpB.y = _bottomBorder;
            curve.LerpA.x = Mathf.Sin(leversAngle) * leversLength;
            curve.LerpA.z = Mathf.Cos(leversAngle) * leversLength;
            curve.LerpB.x = curve.LerpA.x;
            curve.LerpB.z = curve.LerpA.z;
        }


        private Planet SelectRandomPlanet(StarSystem from)
        {
            int planetsCount = _starSystem.GetPlanets().Count();
            int randomIndex = Random.Range(0, planetsCount);
            return _starSystem.GetPlanets().ElementAt(randomIndex);
        }


        private Vector3 CalculateOffsetToPlanet(Planet target, Asteroid asteroid)
        {
            float middleOfAsteroidPath = asteroid.Transform.Length / 2f;
            Vector3 asteroidCrossPoint =
                asteroid.Transform.CalculatePoint(middleOfAsteroidPath);
            float secondsToCross =
                asteroid.Movement.CalculateSeconds(0, middleOfAsteroidPath);

            Vector3 planetCrossPoint =
                target.Movement.CalculatePoint(secondsToCross);
            Vector3 offsetForAsteroid = planetCrossPoint - asteroidCrossPoint;
            return offsetForAsteroid;
        }
    }

}
