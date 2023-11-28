using UnityEngine;

namespace Systemagedon.App.Services
{
    public class UnityCoroutineRunnerFactory : IUnityCoroutineRunnerFactory
    {
        public IUnityCoroutineRunner Create(string name)
        {
            return new GameObject(name + "CoroutineRunner").AddComponent<UnityCoroutineRunner>();
        }
    }
}
