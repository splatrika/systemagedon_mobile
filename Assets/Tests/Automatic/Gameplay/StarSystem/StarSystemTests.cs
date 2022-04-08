using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Systemagedon.App.Gameplay;
using UnityEditor;

namespace Systemagedon.Tests
{

    public class StarSystemTests
    {
        private const string _configPath =
                "Assets/Tests/Automatic/Gameplay/StarSystem/Configs/StarSystemTests.asset";

        [UnityTest]
        public IEnumerator SomePlanetRuinedTests()
        {
            StarSystemTestsConfig config =
                       AssetDatabase.LoadAssetAtPath<StarSystemTestsConfig>(_configPath);

            bool ruinedInvoked = false;

            Planet[] planets = new Planet[2];
            for (int i = 0; i < planets.Length; i++)
            {
                planets[i] = Planet.InitFrom(config.PlanetPrefab, i * 2, 1);
            }

            StarSystem starSystem = new GameObject().AddComponent<StarSystem>();
            starSystem.Init(planets, new GameObject().AddComponent<Star>());
            starSystem.SomePlanetRuined += (Planet planet) => ruinedInvoked = true;

            yield return null;
            Assert.IsFalse(ruinedInvoked, "SomePlanetRuined must din't invoke yet");


            planets[1].Ruin();
            Assert.IsTrue(ruinedInvoked, "SomePlanetRuined must be invoked after " +
                "planet has been ruined");
        }
    }

}
