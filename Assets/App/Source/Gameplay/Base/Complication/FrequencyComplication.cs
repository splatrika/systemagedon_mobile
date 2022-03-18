using UnityEngine;
using System.Collections;
using System;

namespace Systemagedon.App.Gameplay
{

    public class FrequencyComplication : MonoBehaviour, IComplication
    {
        public event Action LevelUp;


        [SerializeField] private float _frequency;


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
            }
        }
    }

}
