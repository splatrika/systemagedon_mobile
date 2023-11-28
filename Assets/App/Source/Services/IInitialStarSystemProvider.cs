namespace Systemagedon.App.Services
{
    public interface IInitialStarSystemProvider
    {
        public int InitialPlanetsCount { get; }

        public bool HasValue();
        public StarSystemSnapshot GetValue();
    }
}
