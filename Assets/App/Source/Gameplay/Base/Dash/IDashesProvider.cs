using System.Collections.Generic;

namespace Systemagedon.App.Gameplay.Dash
{

    public interface IDashesProvider
    {
        public IEnumerable<IDash> Dashes { get; }
    }

}

