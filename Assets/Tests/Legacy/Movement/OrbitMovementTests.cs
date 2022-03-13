using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Systemagedon.App.Movement;
using Systemagedon.App.StarSystem;

namespace Systemagedon.Tests
{

    public class OrbitMovementTests
    {
        [Test]
        public void CalculatePointTest()
        {
            GameObject prefabForTest =
                TestsUtility.MakePrefabReady("Assets/Tests/Movement/Prefabs/OrbitMovementTests.prefab");

            OneAxisMovement movement = prefabForTest.GetComponent<OneAxisMovement>();
            Vector3 calculatedPoint = movement.CalculatePoint(5, 0);
            Vector3 expectedPoint =
                movement.Target.CalculatePoint(movement.TotalVelocity * 5);
            Assert.AreEqual(calculatedPoint, expectedPoint, "Point calculation incorect");

            calculatedPoint = movement.CalculatePoint(5);
            expectedPoint = movement.CalculatePoint(5, movement.Target.Position);
            Assert.AreEqual(calculatedPoint, expectedPoint, "Point calculation incorect");
        }

    }

}