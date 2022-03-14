using System.Collections.Generic;

namespace Systemagedon.App.Gameplay
{

    public interface IDashesProvider
    {
        public IEnumerable<IDash> Dashes { get; }
    }

}

