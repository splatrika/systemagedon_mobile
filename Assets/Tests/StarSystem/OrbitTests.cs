using UnityEngine;
using Systemagedon.App.StarSystem;
using Systemagedon.App.Movement;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

namespace Systemagedon.Tests
{

    public class OrbitTests
    {
        [UnityTest]
        public IEnumerator OrbitMovement()
        {
            GameObject testingPrefab =
                TestsUtility.MakePrefabReady("Assets/Tests/StarSystem/Prefabs/OrbitTests.prefab");
            OneAxisMovement movement = testingPrefab.GetComponent<OneAxisMovement>();
            IOneAxisTransform transform = testingPrefab.GetComponent<OrbitTransform>();
            yield return new WaitForSeconds(1);
            Assert.Greater(transform.Position, movement.Velocity - 1);
            transform.SetPosition(0);
            movement.ApplyVelocity(100);
            yield return new WaitForSeconds(1);
            Assert.Greater(transform.Position, movement.Velocity - 1);
        }


        [UnityTest]
        public IEnumerator OrbitDash()
        {
            GameObject testingPrefab =
                TestsUtility.MakePrefabReady("Assets/Tests/StarSystem/Prefabs/OrbitTests.prefab");
            OneAxisMovement movement = testingPrefab.GetComponent<OneAxisMovement>();
            Dash dash = testingPrefab.GetComponent<Dash>();
            float regularVelocity = movement.Velocity;
            dash.ApplyDash();
            Assert.AreEqual(regularVelocity + dash.Strength, movement.TotalVelocity);
            yield return new WaitForSeconds(dash.Duration / 2);
            Assert.Greater(movement.TotalVelocity, regularVelocity);
            Assert.Less(movement.TotalVelocity, regularVelocity + dash.Strength);
            yield return new WaitForSeconds(dash.Duration / 2 + 0.5f);
            Assert.AreEqual(regularVelocity, movement.TotalVelocity);
        }


        [UnityTest]
        public IEnumerator OrbitCenter()
        {
            GameObject testingPrefab =
                TestsUtility.MakePrefabReady("Assets/Tests/StarSystem/Prefabs/OrbitTests.prefab");
            GameObject root = new GameObject();
            OrbitTransform orbit = testingPrefab.GetComponent<OrbitTransform>();
            testingPrefab.transform.parent = root.transform;
            root.transform.position = Vector3.back;
            yield return null;
            Assert.AreEqual(root.transform.position, orbit.Center,
                "Center of orbit must be equals position of orbit's parent");
        }

    }

}