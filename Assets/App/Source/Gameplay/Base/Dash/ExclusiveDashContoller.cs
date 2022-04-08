using UnityEngine;
using System.Collections;
using System;

namespace Systemagedon.App.Gameplay
{
    /// <summary>
    /// Init from script required
    /// </summary>
    public class ExclusiveDashContoller : MonoBehaviour
    {
        private IDash _target;
        private WorldTouchController _controller;
        private bool _inited = false;


        public void Init(IDash target, WorldTouchController controller)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _target = target;
            _controller = controller;
            _controller.Touched += OnTouched;
            _inited = true;
        }


        private void Start()
        {
            if (!_inited)
            {
                Debug.LogError("Init from script required");
            }
        }


        private void OnDestroy()
        {
            _controller.Touched -= OnTouched;
        }


        private void OnTouched(GameObject touched)
        {
            IDash touchedDash = touched.GetComponent<IDash>();
            if (touchedDash == _target)
            {
                touchedDash.ApplyDash();
            }
        }
    }

}
