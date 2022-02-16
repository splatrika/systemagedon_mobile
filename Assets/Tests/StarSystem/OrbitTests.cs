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
                PrefabUtility.LoadPrefabContents("Assets/Tests/StarSystem/Prefabs/OrbitTests.prefab");
            EditorSceneManager.MoveGameObjectToScene(testingPrefab,
                EditorSceneManager.GetActiveScene());
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
                PrefabUtility.LoadPrefabContents("Assets/Tests/StarSystem/Prefabs/OrbitTests.prefab");
            EditorSceneManager.MoveGameObjectToScene(testingPrefab,
                EditorSceneManager.GetActiveScene());
            OneAxisMovement movement = testingPrefab.GetComponent<OneAxisMovement>();
            Dash dash = testingPrefab.GetComponent<Dash>();
            float regularVelocity = movement.Velocity;
            dash.ApplyDash();
            Assert.AreEqual(regularVelocity + dash.Strength, movement.Velocity);
            yield return new WaitForSeconds(dash.Duration / 2);
            Assert.Greater(movement.Velocity, regularVelocity);
            Assert.Less(movement.Velocity, regularVelocity + dash.Strength);
            yield return new WaitForSeconds(dash.Duration / 2 + 0.5f);
            Assert.AreEqual(regularVelocity, movement.Velocity);
        }

    }

}