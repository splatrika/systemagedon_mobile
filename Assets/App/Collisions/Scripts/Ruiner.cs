using UnityEngine;
using System.Collections;

namespace Systemagedon.App.Collisions
{

    public class Ruiner : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            IRuinable hitted = collision.rigidbody?.GetComponentInChildren<IRuinable>();
            if (hitted != null)
            {
                hitted.Ruin();
            }
        }
    }

}
