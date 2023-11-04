using System;

namespace Systemagedon.App.Services
{
    public class StarSnapshot
    {
        public StarSnapshot(int prefabIndex, float scale)
        {
            PrefabIndex = prefabIndex;
            Scale = scale;
        }

        public int PrefabIndex { get; }
        public float Scale { get; }
    }
}

