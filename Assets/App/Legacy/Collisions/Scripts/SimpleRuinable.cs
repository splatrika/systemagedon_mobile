using UnityEngine;
using System.Collections;

namespace Systemagedon.App.Collisions
{

    public class SimpleRuinable : MonoBehaviour, IRuinable
    {
        public void Ruin()
        {
            Destroy(gameObject);
        }
    }

}
