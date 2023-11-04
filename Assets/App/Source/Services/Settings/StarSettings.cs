namespace Systemagedon.App.Services
{
	public class StarSettings
	{
        public StarSettings(int prafabsCount, RangeFloat size)
        {
            PrafabsCount = prafabsCount;
            Size = size;
        }

        public int PrafabsCount { get; }
        public RangeFloat Size { get; }
    }
}

