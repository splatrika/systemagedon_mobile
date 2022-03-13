using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Systemagedon.App.Movement
{
    public class LegacyDashController : MonoBehaviour
    {
        [SerializeField] private WorldTouchController _controller;
        [SerializeField] private GameObject _providerObject;


        private IDashesProviderLegacy _provider;
        private IEnumerable<LegacyDash> _controlledDashes;


        private void Start()
        {
            OnDashesListUpdated();
        }


        private void OnValidate()
        {
            _provider = _providerObject.GetComponent<IDashesProviderLegacy>();
            if (_provider == null)
            {
                Debug.LogError("Provider must have component that implements IDashesProvider");
                _providerObject = null;
            }
        }


        private void OnEnable()
        {
            OnValidate();
            _provider.DashesListUpdated += OnDashesListUpdated;
            _controller.Touched += OnTouched;
        }


        private void OnDisable()
        {
            _provider.DashesListUpdated -= OnDashesListUpdated;
            _controller.Touched -= OnTouched;
        }


        private void OnDashesListUpdated()
        {
            _controlledDashes = _provider.GetDashes();
        }


        private void OnTouched(GameObject touched)
        {
            if (!enabled)
            {
                return;
            }
            LegacyDash touchedDash = touched.GetComponentInChildren<LegacyDash>();
            bool dashCanControlled = touchedDash
                && _controlledDashes.FirstOrDefault(dash => dash == touchedDash);
            if (dashCanControlled)
            {
                touchedDash.ApplyDash();
            }
        }
    }
}
