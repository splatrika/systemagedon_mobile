namespace Systemagedon.App.Services
{
    public class EmptyInitialStarSystemProvider : IInitialStarSystemProvider
    {
        public EmptyInitialStarSystemProvider(int initialPlanetsCount)
        {
            InitialPlanetsCount = initialPlanetsCount;
        }

        public int InitialPlanetsCount { get; }

        public bool HasValue() => false;
        public StarSystemSnapshot GetValue() => null;
    }
}
