namespace Systemagedon.App.Gameplay
{

    public interface IGameMode
    {
        public string GetStaticName();
        public void LoadAndPlay();
    }

}
