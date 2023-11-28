using System.Collections;

namespace Systemagedon.App.Services
{
    public interface IUnityCoroutineRunner
    {
        public void Run(IEnumerator coroutine);
    }
}
