using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Systemagedon.App.Gameplay;

namespace Systemagedon.Tests
{

    public class PlanetRestoreTests
    {
        private const string _configPath =
                "Assets/Tests/Automatic/Gameplay/StarSystem/Configs/PlanetRestoreTests.asset";


        [Test]
        public void PlanetRestoreTestsWithEnumeratorPasses()
        {
            PlanetRestoreTestsConfig config =
                    AssetDatabase.LoadAssetAtPath<PlanetRestoreTestsConfig>(_configPath);

            float radius = 2;
            float velocity = 3;
            float scale = 4;
            float position = 5;

            Planet planet = Planet.InitFrom(config.PlanetPrefab, radius, velocity,
                scale, position);
            PlanetRuins ruins = null;
            planet.RuinedAdvanced += (sender, spawnedRuins) => { ruins = spawnedRuins; };

            planet.Ruin();
            Assert.IsNotNull(ruins);
            Planet restored = ruins.Restore();
            Assert.AreEqual(radius, restored.Radius);
            Assert.AreEqual(velocity, restored.Velocity);
            Assert.AreEqual(scale, restored.Scale);
            Assert.AreEqual(position, restored.AnglePosition);
            Assert.AreEqual(config.PlanetPrefab, restored.OriginalPrefab);


        }
    }

}
