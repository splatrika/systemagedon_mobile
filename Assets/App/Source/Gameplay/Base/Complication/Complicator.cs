using UnityEngine;
using System;

namespace Systemagedon.App.Gameplay
{

    /// <summary>
    /// Init from script required
    /// </summary>
    public class Complicator : MonoBehaviour
    {
        private IComplication _complication;
        private IComplicatable _target;
        private bool _inited;


        public void Init(IComplication complication, IComplicatable target)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _complication = complication;
            _complication.LevelUp += OnLevelUp;
            _target = target;
            _inited = true;
        }


        private void OnLevelUp()
        {
            _target.RaiseDifficulty();
        }


        private void Start()
        {
            CheckInit();
        }


        private void OnDestroy()
        {
            _complication.LevelUp -= OnLevelUp;
        }


        private void CheckInit()
        {
            if (!_inited)
            {
                throw new InvalidOperationException("Asteroid must inited " +
                    "from script!");
            }
        }
    }

}
