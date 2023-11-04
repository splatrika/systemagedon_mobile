namespace Systemagedon.App.Services
{
	public class PlanetSettings
	{
        public PlanetSettings(int prefabsCount, RangeFloat size, RangeFloat speed, float minDistance)
        {
            PrefabsCount = prefabsCount;
            Size = size;
            Speed = speed;
            MinDistance = minDistance;
        }

        public int PrefabsCount { get; }
        public RangeFloat Size { get; }
        public RangeFloat Speed { get; }
        public float MinDistance { get; }
    }
}

