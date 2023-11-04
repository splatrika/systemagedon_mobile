using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Systemagedon.App.Services
{
    public class StarSystemSnapshot
    {
        public StarSystemSnapshot(IReadOnlyCollection<PlanetSnapshot> planets, StarSnapshot star)
        {
            Planets = planets.ToArray();
            Star = star;
        }

        public IReadOnlyCollection<PlanetSnapshot> Planets { get; }
        public StarSnapshot Star { get; }
    }
}
