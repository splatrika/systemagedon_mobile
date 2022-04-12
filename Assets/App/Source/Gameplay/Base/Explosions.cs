using System;
using UnityEngine;

namespace Systemagedon.App.Gameplay
{

    public static class Explosions
    {
        public static event Action<Transform> OnExplosion;


        public static void Notify(Transform sender)
        {
            OnExplosion?.Invoke(sender);
        }
    }

}
