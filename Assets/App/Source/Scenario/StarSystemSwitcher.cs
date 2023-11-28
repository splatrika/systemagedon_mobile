using System;
using System.Collections;
using System.Collections.Generic;
using Systemagedon.App.Gameplay;
using Systemagedon.App.Services;
using UnityEngine;

namespace Systemagedon.App.Scenario
{
    public class StarSystemSwitcher : IStarSystemSwitcher, ISubscenarioInitialization
    {
        public event Action SwitchEnded;
        public event Action SwitchStarted;

        private readonly IStarSystemContainer _starSystemContainer;
        private readonly IInitialStarSystemProvider _initialStarSystemProvider;
        private readonly IStarSystemGenerator _starSystemGenerator;
        private readonly IUnityCoroutineRunner _coroutineRunner;
        private readonly StarSystemSwitcherSettings _settings;

        private int _level;
        private int _currentPlanetsCount;

        public StarSystemSwitcher(
            IStarSystemContainer starSystemContainer,
            IInitialStarSystemProvider initialStarSystemProvider,
            IStarSystemGenerator starSystemGenerator,
            IUnityCoroutineRunnerFactory coroutineRunnerFactory,
            StarSystemSwitcherSettings settings)
        {
            _starSystemContainer = starSystemContainer;
            _initialStarSystemProvider = initialStarSystemProvider;
            _starSystemGenerator = starSystemGenerator;
            _coroutineRunner = coroutineRunnerFactory.Create(nameof(StarSystemSwitcher));
            _settings = settings;
        }

        // todo add ScenarionStarter or something: call subscenarioStart then scenarioStart
        public void OnSubscenarioStart()
        {
            var initialStarSystem = _initialStarSystemProvider.HasValue()
                ? _initialStarSystemProvider.GetValue()
                : _starSystemGenerator.Generate(_initialStarSystemProvider.InitialPlanetsCount);

            _starSystemContainer.Load(initialStarSystem);
            _currentPlanetsCount = initialStarSystem.Planets.Count;
        }

        public void RaiseDifficulty()
        {
            _level++;
            if (_level % _settings.LevelsToSwitch == 0)
            {
                _coroutineRunner.Run(SwitchCoroutine());
            }
        }

        private IEnumerator SwitchCoroutine()
        {
            SwitchStarted?.Invoke();

            yield return _settings.SwitchAnimation.RunAndWaitUntilEnd();
     
            var newStarSystem = _starSystemGenerator.Generate(++_currentPlanetsCount);
            _starSystemContainer.Load(newStarSystem);

            SwitchEnded?.Invoke();
        }
    }
}
