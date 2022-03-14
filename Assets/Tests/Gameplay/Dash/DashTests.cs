using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Systemagedon.App.Gameplay;
using Systemagedon.App.Movement;

namespace Systemagedon.Tests
{

    public class DashTests
    {
        [UnityTest]
        public IEnumerator DashFromInit()
        {
            GameObject orbitMovementPrefab =
                TestsUtility.MakePrefabReady("Assets/Tests/Gameplay/Dash/Prefabs/OrbitMovement.prefab");
            OneAxisMovement movement =
                orbitMovementPrefab.GetComponent<OneAxisMovement>();

            Dash dash = new GameObject().AddComponent<Dash>();
            Dash.PropertiesFields properties = new Dash.PropertiesFields()
            {
                Strength = 5,
                Duration = 2
            };
            dash.Init(movement, properties);

            yield return null;
            Assert.AreEqual(0, movement.AdditionalVelocity,
                "Dash must be not started yet");

            dash.ApplyDash();
            Assert.AreEqual(dash.Properties.Strength, movement.AdditionalVelocity,
                "Dash started right now. Invalid additional velocity");
            float previousAdditionalVelocity = movement.AdditionalVelocity;

            yield return new WaitForSeconds(dash.Properties.Duration / 2f);
            Assert.Less(movement.AdditionalVelocity, previousAdditionalVelocity,
                "Additional velocity must be less after time");

            yield return new WaitForSeconds(dash.Properties.Duration / 2f + 1);
            Assert.AreEqual(0, movement.AdditionalVelocity,
                "Dash finished. Invalid additional velocity");
        }
    }

}