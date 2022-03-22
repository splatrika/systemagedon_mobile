using UnityEngine;
using System.Collections;

namespace Systemagedon.App.Gameplay
{

    public class Star : MonoBehaviour
    {
        public void Init(float scale)
        {
            transform.localScale = Vector3.one * scale;
        }
    }

}
