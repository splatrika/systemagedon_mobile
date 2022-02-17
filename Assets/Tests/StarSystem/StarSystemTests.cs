using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;
using Systemagedon.App.Movement;
using Systemagedon.App.StarSystem;

namespace Systemagedon.Tests
{

    public class StarSystemTests
    {
        [UnityTest]
        public IEnumerator StarSystemGetDashes()
        {
            GameObject testingPrefab =
                    PrefabUtility.LoadPrefabContents("Assets/Tests/StarSystem/Prefabs/StarSystemTests.prefab");
            EditorSceneManager.MoveGameObjectToScene(testingPrefab,
                EditorSceneManager.GetActiveScene());
            bool listUpdatedRaised = false;
            StarSystem starSystem = testingPrefab.GetComponent<StarSystem>();
            IDashesProvider dashesProvider = (IDashesProvider)starSystem;
            dashesProvider.DashesListUpdated += () =>
            {
                listUpdatedRaised = true;
            };
            yield return null;
            Assert.IsTrue(listUpdatedRaised, "List updated must be invoked after Start");
            List<Dash> dashes = new List<Dash>(starSystem.GetDashes());
            int i = 0;
            foreach (Planet planet in starSystem.GetPlanets())
            {
                Assert.AreEqual(planet.Dash, dashes[i], "Returned dashes from star system must be dashes of its planets");
                i++;
            }
        }
    }

}
