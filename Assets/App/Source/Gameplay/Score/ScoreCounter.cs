using UnityEngine;
using System.Collections.Generic;
using System;

namespace Systemagedon.App.Gameplay
{

    public class ScoreCounter : MonoBehaviour, IScore
    {
        public event Action<int> ScoreChanged;


        public int Score { get; private set; }


        [SerializeField] private AsteroidsAttack _asteroidsAttack;
        private List<Asteroid> _registeredAsteroids = new List<Asteroid>();


        private void Start()
        {
            _asteroidsAttack.SomeDangerPassed += OnSomeDangerPassed;
        }


        private void OnDestroy()
        {
            _asteroidsAttack.SomeDangerPassed -= OnSomeDangerPassed;
        }


        private void OnSomeDangerPassed(Asteroid asteroid)
        {
            Score++;
            ScoreChanged.Invoke(Score);
        }
    }

}
