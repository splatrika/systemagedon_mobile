using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Systemagedon.App.Services;

namespace Systemagedon.App.Gameplay
{
    public class StarSystemSwitcherConfiguration : MonoBehaviour
    {
        public StarSystemGeneratorLegacy Generator { get => _generatorLegacy; }

        [SerializeField] private StarSystemGeneratorLegacy _generatorLegacy;
        [SerializeField] private GameObject _complicationObject;
        [SerializeField] private GameObject _callbackObject;
        [SerializeField] private StarSystemContainer _starSystemContainer;
        [SerializeField] int _levelsToSwitch;
        [SerializeField] int _planetsAtStart;

        private IComplication _complication;
        private Complicator _complicator;
        private IAnimation _callback;
        private IStarSystemGenerator _starSystemGenerator;
        private int _planetsToSpawn;
        private int _level;
        private bool _callbackEnded = false;
        private bool _inited = false;

        public int GetPlanetsAtStart() => _planetsAtStart;

        public StarSystemSwitcherSettings ParseSettings()
        {
            if (_callback == null)
                _callback = _callbackObject.GetComponent<IAnimation>();

            return new StarSystemSwitcherSettings(
                _levelsToSwitch,
                _callback);
        }
    }
}
