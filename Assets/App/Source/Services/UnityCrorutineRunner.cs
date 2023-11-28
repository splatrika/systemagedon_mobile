using System.Collections;
using UnityEngine;

namespace Systemagedon.App.Services
{
    public class UnityCoroutineRunner : MonoBehaviour, IUnityCoroutineRunner
    {
        public static UnityCoroutineRunner Create(string name)
        {
            return new GameObject(name).AddComponent<UnityCoroutineRunner>();
        }

        public void Run(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}
