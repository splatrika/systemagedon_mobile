using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systemagedon.App.Gameplay;

namespace Systemagedon.Tests
{

    [CreateAssetMenu(menuName = "Systemagedon/Tests/PlanetRestoreTests Config")]
    public class PlanetRestoreTestsConfig : ScriptableObject
    {
        public Planet PlanetPrefab { get => _planetPrefab; }


        [SerializeField] private Planet _planetPrefab;
    }

}
