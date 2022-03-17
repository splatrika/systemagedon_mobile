using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using Systemagedon.App.Gameplay;
using Systemagedon.App.Movement;

namespace Systemagedon.Tests
{

    public class AsteroidTests
    {
        private const string _configPath =
            "Assets/Tests/Gameplay/StarSystem/Configs/AsteroidTests.asset";


        [UnityTest]
        public IEnumerator AsteroidDangerPassedEvent()
        {
            AsteroidTestsConfig config =
                AssetDatabase.LoadAssetAtPath<AsteroidTestsConfig>(_configPath);

            if (!config.PlanetPrefab || !config.AsteroidPrefab)
            {
                Debug.LogError("Config has not fully setuped");
            }

            Planet planet = Object.Instantiate(config.PlanetPrefab);
            Asteroid asteroid = Object.Instantiate(config.AsteroidPrefab);

            planet.Init(2, 1);

            Bezier path = new Bezier
            {
                PointA = Vector3.up,
                PointB = Vector3.down
            };
            float pathLength = 2;
            float asteroidSpeed = 0.5f;
            float inaccuracy = 0.1f;
            asteroid.Init(planet, path, asteroidSpeed);

            bool dangerPassed = false;
            asteroid.DangerPassed += (sender) => dangerPassed = true;
            yield return null;
            Assert.IsFalse(dangerPassed, "Danger shouldn't passed yet");

            yield return new WaitForSeconds(asteroid.GetCrossPosition() / asteroidSpeed
                + inaccuracy);
            Assert.IsTrue(dangerPassed, "Danger should already passed");
        }
    }

}
