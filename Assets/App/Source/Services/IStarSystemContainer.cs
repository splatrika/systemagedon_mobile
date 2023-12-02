namespace Systemagedon.App.Services
{
    public interface IStarSystemContainer
    {
        public void Load(StarSystemSnapshot snapshot);
        public StarSystemSnapshot Read();
    }
}
