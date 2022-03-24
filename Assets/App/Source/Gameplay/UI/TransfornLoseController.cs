using UnityEngine;
using System;
using Systemagedon.App.Gameplay;
using Systemagedon.App.Extensions;

namespace Systemagedon.App.UI
{

    public class TransfornLoseController : MonoBehaviour
    {
        [SerializeField] private GameObject _targetObject;
        [SerializeField] private Camera _camera;
        [SerializeField] private TransformLoseView _viewPrefab;


        private ITransformLose _target;


        private void Awake()
        {
            OnValidate();
            if (_target == null)
            {
                throw new NullReferenceException(nameof(_target));
            }
            _target.Lose += OnLose;
        }


        private void OnDestroy()
        {
            _target.Lose -= OnLose;
        }


        private void OnLose(Transform transform)
        {
            TransformLoseView view = Instantiate(_viewPrefab);
            view.Init(_target);
        }


        private void OnValidate()
        {
            this.AssignInterfaceField(ref _targetObject, ref _target,
                nameof(_targetObject));
        }
    }

}