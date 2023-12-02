using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systemagedon.App.Extensions;
using Systemagedon.App.Gameplay.TutorialStates;
using Systemagedon.App.Services;

namespace Systemagedon.App.Gameplay
{

    public class Tutorial : MonoBehaviour, ITutorialContext
    {
        public StarSystem StarSystem { get => _starSystem; }
        public Camera Camera { get => _camera; }
        public MonoBehaviour Component { get => this; }
        public Planet ExamplePlanet { get; set; }
        public AsteroidsGenerator AsteroidsGenerator { get => _asteroidsGenerator; }
        public Asteroid AsteroidPrefab { get => _asteroidPrefab; }


        [SerializeField] private Camera _camera;
        [SerializeField] private StarSystemConfiguration _starSystemSettings;
        [SerializeField] private StarSystemContainer _starSystemContainer;
        [SerializeField] private int _planets = 2;
        [SerializeField] private AsteroidsGenerator _asteroidsGenerator;
        [SerializeField] private Asteroid _asteroidPrefab;
        [Header("States")]
        [SerializeField] private IntroductionCinemaState _introductionCinema;
        [SerializeField] private TapHintState _tapHint;
        [SerializeField] private TutorialFinishedState _finished;


        private StarSystem _starSystem;
        private ITutorialState[] _states;
        private ITutorialState _currentState;


        private void Awake()
        {
            _states = new ITutorialState[]
            {
                _introductionCinema,
                _tapHint,
                _finished
            };
        }


        private void Start()
        {
            if (GlobalInstaller.StarSystemTransferService.IsSnapshotStored())
            {
                var snapshot = GlobalInstaller.StarSystemTransferService.TakeSnapshot();
                _starSystemContainer.Load(snapshot);
            }
            else
            {
                var generator = new StarSystemGenerator(_starSystemSettings.ParseSettings());
                _starSystemContainer.Load(generator.Generate(2));
            }
            _starSystem = FindObjectOfType<StarSystem>(); // todo refactor
            ExamplePlanet = _starSystem.Planets.SelectRandom();
            ChangeState<IntroductionCinemaState>();
        }


        private void OnDestroy()
        {
            if (_currentState != null)
            {
                _currentState.OnFinish(this);
            }
        }


        public void ChangeState<T>() where T : ITutorialState
        {
            if (_currentState != null)
            {
                _currentState.OnFinish(this);
            }
            _currentState = Array.Find(_states, state => state is T);
            _currentState.OnStart(this);
        }


        private void OnDrawGizmos()
        {
            if (_starSystemSettings)
            {
                _starSystemSettings.DrawGizmos();
            }
            if (_asteroidsGenerator)
            {
                _asteroidsGenerator.DrawGizmos();
            }
        }
    }

}