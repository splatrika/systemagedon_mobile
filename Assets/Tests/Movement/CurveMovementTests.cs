using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;
using Systemagedon.App.Movement;

namespace Systemagedon.Tests
{

    public class CurveMovementTests
    {

        [UnityTest]
        public IEnumerator Movement()
        {
            GameObject testingPrefab =
                TestsUtility.MakePrefabReady("Assets/Tests/Movement/Prefabs/CurveMovementTests.prefab");
            yield return null;
            CurveMovement movement = testingPrefab.GetComponent<CurveMovement>();
            Vector3 previousPosition = movement.transform.position;
            movement.SetPosition(movement.Length / 2);
            Vector3 position = movement.transform.position;
            Assert.IsTrue(previousPosition.x < position.x
                && previousPosition.y < position.y);
            movement.SetPosition(movement.Length);
            Assert.AreEqual(movement.Curve.PointB, movement.transform.position);
        }


        [Test]
        public void SetupFromScript()
        {
            CurveMovement movement = new GameObject().AddComponent<CurveMovement>();
            Bezier curve = new Bezier()
            {
                PointA = Vector3.zero,
                PointB = Vector3.up * 2
            };
            movement.ChangeCurve(curve);
            Assert.AreEqual(curve, movement.Curve);
            Assert.AreEqual(2, movement.Length);
            movement.SetPosition(2);
            Assert.AreEqual(Vector3.up * 2, movement.transform.position);
        }
    }

}

