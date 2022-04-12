using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systemagedon.App.Gameplay
{

    [RequireComponent(typeof(RectTransform))]
    public class TapHint : MonoBehaviour
    {

        public void Init(Transform target, Camera camera)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.position = camera.WorldToScreenPoint(target.position);
        }

    }

}