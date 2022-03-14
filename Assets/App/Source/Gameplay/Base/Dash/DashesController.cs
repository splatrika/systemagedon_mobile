using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Systemagedon.App.Gameplay
{

    public class DashesController : MonoBehaviour
    {
        [SerializeField] private WorldTouchController _controller;
        [SerializeField] private GameObject _providerObject;


        private IDashesProvider _provider;
        private bool _inited = false;


        public void Init(WorldTouchController controller, IDashesProvider provider)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _controller = controller;
            _provider = provider;
            _inited = true;
        }


        private void Start()
        {
            OnValidate();
            _inited = true;
        }


        private void OnValidate()
        {
            _provider =
                _providerObject.GetComponent<IDashesProvider>();
            if (_provider == null)
            {
                Debug.LogError("Provider must have component that implements " +
                    "IDashesProvider");
                _providerObject = null;
            }
        }

        private void OnTouched(GameObject touched)
        {
            IDash touchedDash = touched.GetComponentInChildren<IDash>();
            IEnumerable<IDash> controlledDashes = _provider.Dashes;

            bool touchedIsDash = touchedDash != null;
            if (!touchedIsDash)
            {
                return;
            }
            bool dashCanControlled =
                controlledDashes.FirstOrDefault(dash => dash == touchedDash) != null;
            if (dashCanControlled)
            {
                touchedDash.ApplyDash();
            }
        }
    }

}
