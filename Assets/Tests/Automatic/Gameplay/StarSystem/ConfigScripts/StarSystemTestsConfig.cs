using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systemagedon.App.Gameplay;

namespace Systemagedon.Tests
{

    [CreateAssetMenu(menuName = "Systemagedon/Tests/StarSystemTests Config")]
    public class StarSystemTestsConfig : ScriptableObject
    {
        public Planet PlanetPrefab { get => _planetPrefab; }


        [SerializeField] private Planet _planetPrefab;
    }

}
