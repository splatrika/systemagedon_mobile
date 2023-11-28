using UnityEngine;
using System.Collections;
using System;
using Zenject;
using Systemagedon.App.Services;

namespace Systemagedon.App.Gameplay
{

    // todo move to RegularGameplay
    public class FrequencyComplication : MonoBehaviour, IComplication
    {
        public event Action LevelUp;


        [SerializeField] private float _frequency;
        [Inject] private IStarSystemSwitcher _starSystemSwitcher;


        private void Start()
        {
            StartCoroutine(ComplicationCoroutine());
        }


        private IEnumerator ComplicationCoroutine()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(_frequency);
                LevelUp?.Invoke();
                _starSystemSwitcher.RaiseDifficulty();
            }
        }
    }

}
