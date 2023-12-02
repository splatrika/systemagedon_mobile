using UnityEngine;
using System.Collections;

namespace Systemagedon.App.Gameplay
{

    public class Star : MonoBehaviour
    {
        public Star Prefab { get; private set; }
        public float Scale { get; private set; }

        public static Star InitFrom(Star prefab, float scale)
        {
            var instance = GameObject.Instantiate(prefab);
            instance.Prefab = prefab;
            instance.Scale = scale;
            instance.transform.localScale = Vector3.one * scale;
            return instance;
        }

        public void Init(float scale)
        {
            Scale = scale;
            transform.localScale = Vector3.one * scale;
        }
    }

}
