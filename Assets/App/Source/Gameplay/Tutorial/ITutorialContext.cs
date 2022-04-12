using System;
using UnityEngine;

namespace Systemagedon.App.Gameplay
{

    public interface ITutorialContext
    {
        public StarSystem StarSystem { get; }
        public Camera Camera { get; }
        public MonoBehaviour Component { get; }
        public Planet ExamplePlanet { get; set; }
        public AsteroidsGenerator AsteroidsGenerator { get; }
        public Asteroid AsteroidPrefab { get; }


        public void ChangeState<T>() where T : ITutorialState;
    }

}