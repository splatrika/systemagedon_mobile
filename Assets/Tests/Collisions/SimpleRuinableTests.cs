using System.Collections;
using Systemagedon.App.Collisions;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace Systemagedon.Tests
{

    public class SimpleRuinableTests
    {
        [UnityTest]
        public IEnumerator SimpleRuinableRuin()
        {
            GameObject testingPrefab =
                TestsUtility.MakePrefabReady("Assets/Tests/Collisions/Prefabs/SimpleRuinableTest.prefab");
            SimpleRuinable ruinable =
                testingPrefab.GetComponentInChildren<SimpleRuinable>();
            Ruiner ruiner = testingPrefab.GetComponentInChildren<Ruiner>();
            yield return null;
            if (!ruinable)
            {
                Assert.Fail("Ruinable must be not destroyed yet");
            }
            ruiner.transform.position = ruinable.transform.position;
            yield return null;
            if (ruinable)
            {
                Assert.Fail("Ruinable must be destroyed after collision with ruiner");
            }
        }
    }

}