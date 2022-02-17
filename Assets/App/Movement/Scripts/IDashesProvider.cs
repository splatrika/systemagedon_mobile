using System;
using System.Collections.Generic;


namespace Systemagedon.App.Movement
{
    public interface IDashesProvider
    {
        public event Action DashesListUpdated;


        public IEnumerable<Dash> GetDashes();
    }
}
