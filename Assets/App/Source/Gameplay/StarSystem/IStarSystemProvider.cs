using System;
using System.Collections.Generic;

namespace Systemagedon.App.Gameplay
{

    public interface IStarSystemProvider
    {
        public event Action<Planet> SomePlanetRuined;
        public event Action<IStarSystemProvider> ModelUpdated;
        public IEnumerable<Planet> Planets { get; }
    }

}
