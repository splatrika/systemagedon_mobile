using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Systemagedon.App.Gameplay;

namespace Systemagedon.Tests
{

    public class StarSystemTests
    {
        [UnityTest]
        public IEnumerator SomePlanetRuinedTests()
        {
            bool ruinedInvoked = false;

            Planet[] planets = new Planet[2];
            for (int i = 0; i < planets.Length; i++)
            {
                planets[i] = new GameObject().AddComponent<Planet>();
                planets[i].Init(i * 2, 1);
            }

            StarSystem starSystem = new GameObject().AddComponent<StarSystem>();
            starSystem.Init(planets);
            starSystem.SomePlanetRuined += (Planet planet) => ruinedInvoked = true;

            yield return null;
            Assert.IsFalse(ruinedInvoked, "SomePlanetRuined must din't invoke yet");


            planets[1].Ruin();
            Assert.IsTrue(ruinedInvoked, "SomePlanetRuined must be invoked after " +
                "planet has been ruined");
        }
    }

}
