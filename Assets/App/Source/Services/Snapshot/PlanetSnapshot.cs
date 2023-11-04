using System;

namespace Systemagedon.App.Services
{
    public class PlanetSnapshot
    {
        public PlanetSnapshot(
            int prefabIndex,
            float orbitRadius,
            float velocity,
            float scale,
            float anglePosition)
        {
            PrefabIndex = prefabIndex;
            OrbitRadius = orbitRadius;
            Velocity = velocity;
            Scale = scale;
            AnglePosition = anglePosition;
        }

        public int PrefabIndex { get; }
        public float OrbitRadius { get; }
        public float Velocity { get; }
        public float Scale { get; }
        public float AnglePosition { get; }
    }
}

