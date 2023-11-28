namespace Systemagedon.App.Services
{
    public interface IUnityCoroutineRunnerFactory
    {
        public IUnityCoroutineRunner Create(string name);
    }
}
