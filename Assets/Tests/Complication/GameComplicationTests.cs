using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Systemagedon.App.GameComplicaton;
using Systemagedon.App.StarSystem;

namespace Systemagedon.Tests
{

    public class GameComplicationTests
    {
        [UnityTest]
        public IEnumerator AsteroidsComplication()
        {
            GameObject testPrefab = TestsUtility.MakePrefabReady
                ("Assets/Tests/Complication/Prefabs/AsteroidsComplicationTests.prefab");
            AsteroidSpawner asteroidSpawner =
                testPrefab.GetComponent<AsteroidSpawner>();
            AsteroidsComplicator asteroidsComplicator =
                testPrefab.GetComponent<AsteroidsComplicator>();
            FrequencyComplication frequencyComplication =
                testPrefab.GetComponent<FrequencyComplication>();

            float previousSpawnPerSecond = asteroidSpawner.SpawnPerSecond;

            yield return new WaitForSeconds(frequencyComplication.Frequency);

            float expectedSpawnPerSecond =
                previousSpawnPerSecond + asteroidsComplicator.Steps.SpawnPerSecond;
            float actualSpawnPerSecond = asteroidSpawner.SpawnPerSecond;
            Assert.AreEqual(expectedSpawnPerSecond, actualSpawnPerSecond);

        }
    }

}
