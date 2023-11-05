using System.Collections.Generic;

namespace Systemagedon.App.Gameplay
{

    public interface IDashesProvider
    {
        public IReadOnlyCollection<IDash> Dashes { get; }
    }

}

