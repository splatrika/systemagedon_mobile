using UnityEngine;
using System.Collections;
using System;

namespace Systemagedon.App.GameComplicaton
{

    public class FrequencyComplication : MonoBehaviour, IComplication
    {
        public event Action LevelUp;


        public float Frequency { get => _frequency; }
        public int Level { get => _level; }


        [SerializeField] private float _frequency;
        private int _level;


        private void OnEnable()
        {
            StartCoroutine(ComplicationCoroutine());
        }


        private IEnumerator ComplicationCoroutine()
        {
            while (enabled)
            {
                _level++;
                LevelUp?.Invoke();
                yield return new WaitForSeconds(_frequency);
            }
        }


    }

}
