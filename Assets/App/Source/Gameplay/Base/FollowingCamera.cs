using UnityEngine;
using System.Collections;

namespace Systemagedon.App
{

    public class FollowingCamera : MonoBehaviour
    {
        private Camera _camera;
        private Transform _target;
        private float _speed;
        private float _distance;


        public void Init(Camera camera, Transform target, float speed = 2,
            float distance = 1)
        {
            _camera = camera;
            _target = target;
            _speed = speed;
            _distance = distance;
        }


        //private void Update()
        //{
        //    Vector3 targetPosition = Vector3.
        //    _camera.transform.position +=
        //        (targetPosition - _camera.transform.position) / 2;
        //}
    }

}