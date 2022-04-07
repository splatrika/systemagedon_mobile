using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Systemagedon.App.Gameplay
{

    public class RegularLoseVisualizer : MonoBehaviour
    {
        [SerializeField] private RegularLoseUI _uiPrefab;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Camera _camera;
        [SerializeField] private RegularGameplay _gameplay;
        [Header("Look at ruined planet")]
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private float _cameraFOV;
        [SerializeField] private float _transition;


        private void Awake()
        {
            _gameplay.Lose += OnLose;
        }


        private void OnDestroy()
        {
            _gameplay.Lose -= OnLose;
        }


        private void OnLose(RegularLoseContext context)
        {
            StartCoroutine(VisualizationCoroutine(context));
        }


        private IEnumerator VisualizationCoroutine(RegularLoseContext context)
        {
            Vector3 _target = context.Ruined.transform.position + _cameraOffset;
            Vector3 _lookPosition = _camera.transform.position - _cameraOffset;
            _camera.transform.DOMove(_target, _transition);
            _camera.transform.DOLookAt(_lookPosition, _transition);
            _camera.DOFieldOfView(_cameraFOV, _transition);
            yield return new WaitForSeconds(_transition);
            print("UI");
            RegularLoseUI ui = Instantiate(_uiPrefab);
            ui.transform.SetParent(_canvas.transform, false);
            ui.Init(context);
        }
    }

}
