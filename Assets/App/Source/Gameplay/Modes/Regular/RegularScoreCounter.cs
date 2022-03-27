using UnityEngine;
using System.Collections.Generic;
using System;

namespace Systemagedon.App.Gameplay
{
    /// <summary>
    /// Init from script requiered
    /// </summary>
    public class RegularScoreCounter : MonoBehaviour, IScore
    {
        public event Action<int> ScoreChanged;


        public int Score { get; private set; }


        private AsteroidsAttack _asteroidsAttack;
        private List<Asteroid> _registeredAsteroids = new List<Asteroid>();
        private bool _inited = false;


        public void Init(AsteroidsAttack atack)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _asteroidsAttack = atack;
            _inited = true;
        }


        private void Start()
        {
            if (!_inited)
            {
                Debug.LogError("Init from script requiered");
            }
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
