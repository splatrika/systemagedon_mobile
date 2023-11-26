using System;
using System.Collections;
using Systemagedon.App.Gameplay.Drawing;
using UnityEngine;

namespace Systemagedon.App.Gameplay.TutorialStates
{

    [Serializable]
    public class TapHintState : ITutorialState
    {
        [SerializeField] private float _asteroidWait;
        [SerializeField] private float _pauseWait;
        [SerializeField] private AsteroidPathHighlight _asteroidHighlightPrefab;
        [SerializeField] private WorldTouchController _touchController;
        [SerializeField] private TapHint _tapHintPrefab;
        [SerializeField] private Canvas _canvas;


        public void OnFinish(ITutorialContext context)
        {
            
        }


        public void OnStart(ITutorialContext context)
        {
            context.Component.StartCoroutine(HintCoroutine(context));
        }


        private IEnumerator HintCoroutine(ITutorialContext context)
        {
            yield return new WaitForSeconds(_asteroidWait);
            Asteroid asteroid = context.AsteroidsGenerator.GenerateAndSpawn(
                context.AsteroidPrefab, context.ExamplePlanet);
            GameObject.Instantiate(_asteroidHighlightPrefab).Init(asteroid);
            yield return new WaitForSeconds(_pauseWait);
            context.StarSystem.Pause();
            asteroid.Pause();
            ExclusiveDashContoller dashContoller =
                context.Component.gameObject.AddComponent<ExclusiveDashContoller>();
            dashContoller.Init(context.ExamplePlanet, _touchController);
            bool touched = false;
            Action onTouched = () => touched = true;
            dashContoller.Touched += onTouched;
            TapHint hint = GameObject.Instantiate(_tapHintPrefab);
            hint.transform.SetParent(_canvas.transform, false);
            hint.Init(context.ExamplePlanet.transform, context.Camera);
            yield return new WaitUntil(() => touched);
            dashContoller.Touched -= onTouched;
            GameObject.Destroy(dashContoller);
            GameObject.Destroy(hint.gameObject);
            context.StarSystem.Resume();
            asteroid.Resume();
            context.ChangeState<TutorialFinishedState>();
        }
    }

}
