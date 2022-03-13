using UnityEngine;
using System.Collections;
using System;

public class WorldTouchController : MonoBehaviour
{
    public event Action<GameObject> Touched;


    [SerializeField] private Camera _camera;
    [SerializeField] private float _rayCastDistance = 100;


    private void Update()
    {
        Vector3 touchPosition = new Vector3();
        bool touched = false;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            touched = true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
            touched = true;
        }
        if (touched)
        {
            RaycastHit raycastHit;
            Ray ray = _camera.ScreenPointToRay(touchPosition);
            bool touchedOnObject =
                Physics.Raycast(ray, out raycastHit, _rayCastDistance)
                && raycastHit.rigidbody;
            if (touchedOnObject)
            {
                Touched.Invoke(raycastHit.rigidbody.gameObject);
            }
        }
    }

}
