﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Systemagedon.App.Gameplay
{

    public class StarSystemSwitcher : MonoBehaviour, IStarSystemProvider,
        IDashesProvider, IComplicatable
    {
        public event Action<StarSystem> SwitchEnded;
        public event Action<StarSystem> SwitchStarted;
        public event Action<Planet> SomePlanetRuined;
        public event Action<IStarSystemProvider> ModelUpdated;


        public StarSystemGenerator Generator { get => _generator; }
        public IEnumerable<Planet> Planets { get => _current?.Planets; }
        public IEnumerable<IDash> Dashes { get => _current?.Dashes; }


        [SerializeField] private StarSystemGenerator _generator;
        [SerializeField] private GameObject _complicationObject;
        [SerializeField] private GameObject _callbackObject;
        [SerializeField] int _levelsToSwitch;
        [SerializeField] int _planetsAtStart;


        private IComplication _complication;
        private Complicator _complicator;
        private ISwitchCallback _callback;
        private StarSystem _current;
        private int _planetsToSpawn;
        private int _level;
        private bool _callbackEnded = false;


        private void Start()
        {
            _complication = _complicationObject.GetComponent<IComplication>();
            if (_callbackObject)
            {
                _callback = _callbackObject.GetComponent<ISwitchCallback>();
            }
            _complicator = gameObject.AddComponent<Complicator>();
            _complicator.Init(_complication, this);
            _planetsToSpawn = _planetsAtStart;
            if (_callback != null)
            {
                _callback.CallbackEnded += OnCallbackEnded;
            }
            ChangeStarSystem();
        }


        private void OnDestroy()
        {
            Destroy(_complicator);
            if (_callback != null)
            {
                _callback.CallbackEnded -= OnCallbackEnded;
            }
            OnRemoveCurrent();
        }


        void IComplicatable.RaiseDifficulty()
        {
            _level++;
            if (_level % _levelsToSwitch == 0)
            {
                StartCoroutine(SwitchCoroutine());
            }
        }


        private IEnumerator SwitchCoroutine()
        {
            SwitchStarted?.Invoke(_current);
            if (_callback != null)
            {
                _callback.Run();
                yield return new WaitUntil(() => _callbackEnded);
                _callbackEnded = false;
            }
            if (_planetsToSpawn != _generator.CalculateMaxPlanets())
            {
                _planetsToSpawn++;
            }
            ChangeStarSystem();
            SwitchEnded?.Invoke(_current);
        }


        private void ChangeStarSystem()
        {
            OnRemoveCurrent();
            _current = _generator.GenerateAndSpawn(_planetsToSpawn);
            ModelUpdated?.Invoke(_current);
            _current.SomePlanetRuined += OnSomePlanetRuined;
        }


        private void OnRemoveCurrent()
        {
            if (_current)
            {
                Destroy(_current.gameObject);
                _current.SomePlanetRuined -= OnSomePlanetRuined;
            }
        }

        private void OnCallbackEnded()
        {
            _callbackEnded = true;
        }


        private void OnSomePlanetRuined(Planet planet)
        {
            SomePlanetRuined?.Invoke(planet);
        }


        private void OnDrawGizmos()
        {
            _generator.DrawGizmos();
            if (_generator.CalculateMaxPlanets() < 2)
            {
                Gizmos.DrawIcon(Vector3.zero, "console.erroricon");
            }
        }
    }

}
