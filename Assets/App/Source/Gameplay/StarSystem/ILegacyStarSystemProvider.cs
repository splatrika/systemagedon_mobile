using System;
using System.Collections.Generic;

namespace Systemagedon.App.Gameplay
{

    public interface ILegacyStarSystemProvider
    {
        public event Action<Planet> SomePlanetRuined;
        public event Action<ILegacyStarSystemProvider> ModelUpdated;
        public IReadOnlyCollection<Planet> Planets { get; }
    }

}
