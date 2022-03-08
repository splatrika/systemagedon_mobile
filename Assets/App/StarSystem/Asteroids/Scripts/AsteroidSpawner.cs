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


        protected override void SetupOnInstance(Asteroid instance)
        {
            GameObject asteroidRoot = new GameObject();
            Bezier randomCurve = new Bezier();

            randomCurve.PointA.y = _topBorder;
            randomCurve.PointB.y = _bottomBorder;

            float leversAngle = Random.Range(0f, Mathf.PI * 2);
            float leversLength = Random.Range(_leverLength.Min, _leverLength.Min);
            randomCurve.LerpA.y = _topBorder;
            randomCurve.LerpB.y = _bottomBorder;
            randomCurve.LerpA.x = Mathf.Sin(leversAngle) * leversLength;
            randomCurve.LerpA.z = Mathf.Cos(leversAngle) * leversLength;
            randomCurve.LerpB.x = randomCurve.LerpA.x;
            randomCurve.LerpB.z = randomCurve.LerpA.z;

            instance.Transform.ChangeCurve(randomCurve);
            instance.Movement.SetVelocity(
                Random.Range(_asteroidVelocity.Min, _asteroidVelocity.Max));

            int planetsCount = _starSystem.GetPlanets().Count();
            int randomIndex = Random.Range(0, planetsCount);
            Planet targetPlanet =
                _starSystem.GetPlanets().ElementAt(randomIndex);

            //A point where asteroid collides star system plane
            float middleOfAsteroidPath = instance.Transform.Length / 2f;
            Vector3 asteroidCrossPoint =
                instance.Transform.CalculatePoint(middleOfAsteroidPath);
            float secondsToCross =
                instance.Movement.CalculateSeconds(0, middleOfAsteroidPath);

            Vector3 planetCrossPoint =
                targetPlanet.Movement.CalculatePoint(secondsToCross);
            Vector3 offsetForAsteroid = planetCrossPoint - asteroidCrossPoint;
            offsetForAsteroid.y = 0;
            instance.transform.SetParent(asteroidRoot.transform);
            asteroidRoot.transform.position = offsetForAsteroid;
        }
    }

}
