using System;
using System.Collections.Generic;
using Systemagedon.App.Gameplay;
using Systemagedon.App.Scenario;
using Systemagedon.App.Services;
using UnityEngine;
using Zenject;

public class RegularGameplayStartup : MonoInstaller
{
    [SerializeField]
    private RegularGameplay _regularGameplay;

    [SerializeField]
    private StarSystemContainer _starSystemContainer;

    [SerializeField]
    private StarSystemGeneratorLegacy _starSystemConfiguration;

    [SerializeField]
    private StarSystemSwitcherConfiguration _starSystemSwitcherConfiguration;

    public override void InstallBindings()
    {
        Container
            .Bind<IUnityCoroutineRunnerFactory>()
            .To<UnityCoroutineRunnerFactory>()
            .AsSingle();

        Container
            .Bind<IStarSystemContainer>()
            .FromInstance(_starSystemContainer);

        Container
            .Bind<StarSystemSettings>()
            .FromInstance(_starSystemConfiguration.ParseSettings());

        Container
            .Bind<IInitialStarSystemProvider>()
            .FromInstance(new EmptyInitialStarSystemProvider(
                _starSystemSwitcherConfiguration.GetPlanetsAtStart()
            ));

        Container
            .Bind<IStarSystemGenerator>()
            .To<StarSystemGenerator>()
            .AsSingle();

        Container
            .Bind<StarSystemSwitcherSettings>()
            .FromInstance(_starSystemSwitcherConfiguration.ParseSettings());

        Container
            .Bind(typeof(IStarSystemSwitcher), typeof(ISubscenarioInitialization))
            .To<StarSystemSwitcher>()
            .AsSingle();
    }

    public override void Start()
    {
        _regularGameplay.Init(Container.Resolve<IStarSystemSwitcher>());

        var subscenarios = Container.Resolve<IEnumerable<ISubscenarioInitialization>>();
        foreach (var subscenario in subscenarios)
        {
            subscenario.OnSubscenarioStart();
        }

        // todo add scenarios init
    }

    private void OnDrawGizmos()
    {
        _starSystemConfiguration.DrawGizmos();
    }
}