using System;

namespace Systemagedon.App.Services
{
	public class StarSystemSettings
	{
        public StarSystemSettings(float maxRadius, PlanetSettings planet, StarSettings star)
        {
            MaxRadius = maxRadius;
            Planet = planet;
            Star = star;
        }

        public float MaxRadius { get; }
		public PlanetSettings Planet { get; }
		public StarSettings Star { get; }
	}
}

