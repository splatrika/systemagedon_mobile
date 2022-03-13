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
            CurveTransform curveTransform = testingPrefab.GetComponent<CurveTransform>();
            Vector3 previousPosition = curveTransform.transform.position;
            curveTransform.SetPosition(curveTransform.Length / 2);
            Vector3 position = curveTransform.transform.position;
            Assert.IsTrue(previousPosition.x < position.x
                && previousPosition.y < position.y);
            curveTransform.SetPosition(curveTransform.Length);
            Assert.AreEqual(curveTransform.Curve.PointB, curveTransform.transform.position);
        }


        [Test]
        public void SetupFromScript()
        {
            CurveTransform curveTransform = new GameObject().AddComponent<CurveTransform>();
            Bezier curve = new Bezier()
            {
                PointA = Vector3.zero,
                PointB = Vector3.up * 2
            };
            curveTransform.ChangeCurve(curve);
            Assert.AreEqual(curve, curveTransform.Curve);
            Assert.AreEqual(2, curveTransform.Length);
            curveTransform.SetPosition(2);
            Assert.AreEqual(Vector3.up * 2, curveTransform.transform.position);
        }
    }

}

