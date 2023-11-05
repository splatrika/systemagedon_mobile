using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Systemagedon.App.Services;

namespace Systemagedon.App.Gameplay
{

    public class StarSystemSwitcher : MonoBehaviour, IComplicatable
    {
        public event Action SwitchEnded;
        public event Action SwitchStarted;


        public StarSystemGeneratorLegacy Generator { get => _generatorLegacy; }


        [SerializeField] private StarSystemGeneratorLegacy _generatorLegacy;
        [SerializeField] private GameObject _complicationObject;
        [SerializeField] private GameObject _callbackObject;
        [SerializeField] private StarSystemContainer _starSystemContainer;
        [SerializeField] int _levelsToSwitch;
        [SerializeField] int _planetsAtStart;


        private IComplication _complication;
        private Complicator _complicator;
        private ISwitchCallback _callback;
        private IStarSystemGenerator _starSystemGenerator;
        private int _planetsToSpawn;
        private int _level;
        private bool _callbackEnded = false;
        private bool _inited = false;


        public void Init(StarSystem startsWith = null)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _starSystemGenerator = new StarSystemGenerator(_generatorLegacy.ParseSettings()); // todo pass settings 
            if (startsWith)
            {
                Debug.LogWarning("StartWith temporary unsupported");
            }
            else
            {
                OnSwitch();
            }
            _inited = true;
        }


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
            if (!_inited)
            {
                Init();
            }
        }


        private void OnDestroy()
        {
            Destroy(_complicator);
            if (_callback != null)
            {
                _callback.CallbackEnded -= OnCallbackEnded;
            }
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
            SwitchStarted?.Invoke();
            if (_callback != null)
            {
                _callback.Run();
                yield return new WaitUntil(() => _callbackEnded);
                _callbackEnded = false;
            }
            if (_planetsToSpawn != _starSystemGenerator.CalculateMaxPlanets())
            {
                _planetsToSpawn++;
            }
            OnSwitch();
            SwitchEnded?.Invoke();
        }


        private void OnSwitch()
        {
            var generated = _starSystemGenerator.Generate(_planetsToSpawn);
            _starSystemContainer.Load(generated);
        }

        private void OnCallbackEnded()
        {
            _callbackEnded = true;
        }


        private void OnDrawGizmos()
        {
            _generatorLegacy.DrawGizmos();
            if (_generatorLegacy.CalculateMaxPlanets() < 2)
            {
                Gizmos.DrawIcon(Vector3.zero, "console.erroricon");
            }
        }
    }

}
